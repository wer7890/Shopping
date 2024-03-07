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
        /// 登入
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
                    //string sql = "SELECT f_pwd FROM t_userInfo WHERE f_userName=@userName COLLATE SQL_Latin1_General_CP1_CS_AS";

                    using (SqlCommand cmd = new SqlCommand("getPwd", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;   //設定CommandType屬性為預存程序
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userName", userName));
                        string result = cmd.ExecuteScalar().ToString();

                        if (result.ToString() == pwd)
                        {
                            // 認證成功，設定 Session
                            if (SetSessionId(userName))
                            {
                                HttpContext.Current.Session["userName"] = userName;
                                return true;
                            }
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

        /// <summary>
        /// 把SessionId寫進資料庫
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool SetSessionId(string userName) 
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //string sql = "UPDATE t_userInfo SET f_sessionId=@sessionId WHERE f_userName=@userName";

                    using (SqlCommand cmd = new SqlCommand("setSessionId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@sessionId", HttpContext.Current.Session.SessionID.ToString()));
                        cmd.Parameters.Add(new SqlParameter("@userName", userName));

                        return cmd.ExecuteNonQuery() != 0;
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