using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace ShoppingWeb
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{folder}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { folder = "Controller" } // 约束 folder 只能是 "Ajax"
            );
        }
    }
}