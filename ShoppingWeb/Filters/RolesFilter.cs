using NLog;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ShoppingWeb.Controller
{
    public class RolesFilter : AuthorizationFilterAttribute
    {
        private readonly int _roles;

        public RolesFilter(int roles)
        {
            _roles = roles;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count > 0)  //類上有無標記[AllowAnonymous]有就return
                {
                    return;
                }
                if (actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count > 0)  //方法上有無標記[AllowAnonymous]有就return
                {
                    return;
                }

                if (!(((UserInfo)HttpContext.Current.Session["userInfo"]).Roles == 1 || ((UserInfo)HttpContext.Current.Session["userInfo"]).Roles == _roles))
                {
                    actionContext.Response = actionContext.Request.CreateResponse((int)UserStatus.AccessDenied);
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex);
                actionContext.Response = actionContext.Request.CreateResponse((int)DatabaseOperationResult.Error);
            }
        }
    }
}