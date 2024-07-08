using ShoppingWeb.Repository;
using ShoppingWeb.Response;
using System;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ShoppingWeb.Filters
{
    public class LoginFilter : AuthorizationFilterAttribute
    {
        private IBaseRepository _baseRepo;

        private IBaseRepository BaseRepo
        {
            get
            {
                if (this._baseRepo == null)
                {
                    this._baseRepo = new BaseRepository();
                }

                return this._baseRepo;
            }
        }
        
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (HttpContext.Current.Session["userInfo"] == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(new BaseResponse
                {
                    Status = ActionResult.DuplicateLogin
                });
            }

            //類上或方法上有標記[AllowAnonymous]有就return
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }

            var loginResult = IsRepeatLogin();

            switch (loginResult.Status)
            {
                case ActionResult.DuplicateLogin:
                    HttpContext.Current.Session["userInfo"] = null;
                    actionContext.Response = actionContext.Request.CreateResponse(new BaseResponse
                    {
                        Status = ActionResult.DuplicateLogin
                    });
                    break;
                case ActionResult.Error:
                    actionContext.Response = actionContext.Request.CreateResponse(new BaseResponse
                    {
                        Status = ActionResult.Error
                    });
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 判斷是否有重複登入
        /// </summary>
        /// <returns></returns>
        public BaseResponse IsRepeatLogin()
        {
            try
            {
                (Exception exc, int? result) = this.BaseRepo.RepeatLogin();

                if (exc != null)
                {
                    throw exc;
                }

                if (result == 0)
                {
                    return new BaseResponse
                    {
                        Status = ActionResult.DuplicateLogin
                    };
                }

                return new BaseResponse
                {
                    Status = ActionResult.LoginCorrect
                };
            }
            catch (Exception ex)
            {
                this.BaseRepo.SetNLog(ex);
                return new BaseResponse
                {
                    Status = ActionResult.Error
                };
            }
        }
    }
}