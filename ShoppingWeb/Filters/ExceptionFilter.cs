using System;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using NLog;

namespace ShoppingWeb.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Error("後端filter" + context.Exception.Message + "帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);

            context.Response = context.Request.CreateResponse((int)DatabaseOperationResult.Error);
        }
    }
}