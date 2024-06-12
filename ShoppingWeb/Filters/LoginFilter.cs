using NLog;
using ShoppingWeb.Response;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                //類上或方法上有標記[AllowAnonymous]有就return
                if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())  
                {
                    return;
                }

                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                // 判斷同一隻帳號是否有重複登入
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getSessionId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userId", ((UserInfo)HttpContext.Current.Session["userInfo"]).UserId));
                        cmd.Parameters.Add(new SqlParameter("@sessionId", HttpContext.Current.Session.SessionID.ToString()));

                        int result = (int)cmd.ExecuteScalar();

                        if (result == 0)
                        {
                            HttpContext.Current.Session["userInfo"] = null;
                            actionContext.Response = actionContext.Request.CreateResponse(new BaseResponse
                            {
                                Status = ActionResult.DuplicateLogin
                            });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex);
                actionContext.Response = actionContext.Request.CreateResponse(new BaseResponse
                {
                    Status = ActionResult.Error
                });
            }
        }
    }
}