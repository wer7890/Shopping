using NLog;
using System;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ShoppingWeb.Filter
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
                //類上或方法上有標記[AllowAnonymous]有就return
                if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())  
                {
                    return;
                }

                //跳過有標記[SkipFilter(參數)]的該參數的Filter
                if (actionContext.ActionDescriptor.GetCustomAttributes<SkipFilter>().Any(a => a.FilterName == "RolesFilter"))
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