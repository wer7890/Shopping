using System;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.SessionState;

namespace ShoppingWeb
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //註冊WebAPI路由
            RouteTable.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{folder}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { folder = "Controller" } 
            );
        }


        /// <summary>
        /// 檢查當前請求的URL是否以~/api開頭
        /// </summary>
        /// <returns></returns>
        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/api");
        }

        void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }
    }
}