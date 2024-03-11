using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;


namespace ShoppingWeb.Ajax
{
    public partial class LoginHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 登入，如果成功就把sessionId寫入資料庫
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool LoginUser(string userName, string pwd)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("LoginUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userName", userName));
                        cmd.Parameters.Add(new SqlParameter("@pwd", pwd));
                        cmd.Parameters.Add(new SqlParameter("@sessionId", HttpContext.Current.Session.SessionID.ToString()));

                        object result = cmd.ExecuteScalar();

                        if (result != null && result.ToString() == "1")
                        {
                            HttpContext.Current.Session["userName"] = userName;
                            return true;
                        }

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                return false;
            }
        }

    }
}