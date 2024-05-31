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
    public class LoginFilter: AuthorizationFilterAttribute
    {
        public LoginFilter()
        {
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

                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                // 判斷同一隻帳號是否有重複登入
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getSessionId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userId", ((UserInfo)HttpContext.Current.Session["userInfo"]).UserId));

                        object dbResult = cmd.ExecuteScalar();

                        if (dbResult != null)
                        {
                            string currentSessionID = HttpContext.Current.Session.SessionID;

                            if (dbResult.ToString() != currentSessionID)
                            {
                                HttpContext.Current.Session["userInfo"] = null;
                                actionContext.Response = actionContext.Request.CreateResponse((int)UserStatus.DuplicateLogin);
                            }

                        }
                    }
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