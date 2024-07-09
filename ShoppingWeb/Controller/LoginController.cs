using ShoppingWeb.Filters;
using ShoppingWeb.Repository;
using ShoppingWeb.Response;
using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace ShoppingWeb.Controller
{
    [RoutePrefix("/api/Controller/login")]
    [ValidationFilter]
    public class LoginController : ApiController
    {
        private ILoginRepository _loginRepo;

        private ILoginRepository LoginRepo
        {
            get
            {
                if (this._loginRepo == null)
                {
                    this._loginRepo = new LoginRepository();
                }

                return this._loginRepo;
            }
        }

        /// <summary>
        /// 登入，如果成功就把sessionId寫入資料庫，並且創建userInfo物件把userId和roles存到userInfo物件中，再存到Session["userInfo"]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("LoginUser")]
        public BaseResponse LoginUser([FromBody] LoginUserDto dto)
        {
            try
            {
                if (!Regex.IsMatch(dto.Account, @"^[A-Za-z0-9]{6,16}$") || !Regex.IsMatch(dto.Pwd, @"^[A-Za-z0-9]{6,16}$"))
                {
                    return new BaseResponse
                    {
                        Status = ActionResult.InputError
                    };
                }

                (Exception exc, int? result) = this.LoginRepo.LoginUser(dto);

                if (exc != null)
                {
                    throw exc;
                }

                return new BaseResponse
                {
                    Status = (result == 1) ? ActionResult.Success : ActionResult.Failure
                };
            }
            catch (Exception ex)
            {
                this.LoginRepo.SetNLog(ex);
                return new BaseResponse
                {
                    Status = ActionResult.Error
                };
            }
        }

       
        /// <summary>
        /// 取得管理員身分
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUserPermission")]
        public GetUserPermissionResponse GetUserPermission()
        {
            return new GetUserPermissionResponse
            {
                Account = ((UserInfo)HttpContext.Current.Session["userInfo"]).Account,
                Roles = ((UserInfo)HttpContext.Current.Session["userInfo"]).Roles,
                Status = ActionResult.Success
            };
        }

        /// <summary>
        /// 刪除Session["userId"]
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteSession")]
        public void DeleteSession()
        {
            HttpContext.Current.Session["userInfo"] = null;
        }
    }
}