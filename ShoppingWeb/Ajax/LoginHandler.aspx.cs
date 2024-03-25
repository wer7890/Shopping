using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
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
        /// 登入，如果成功就把sessionId寫入資料庫，並且把userId存到Session["userId"]
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [WebMethod]
        public static string LoginUser(string userName, string pwd)
        {
            if (SpecialChar(userName, pwd))
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("loginUser", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.Add(new SqlParameter("@userName", userName));
                            cmd.Parameters.Add(new SqlParameter("@pwd",AddUserHandler.GetSHA256HashFromString(pwd)));
                            cmd.Parameters.Add(new SqlParameter("@sessionId", HttpContext.Current.Session.SessionID.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int));
                            cmd.Parameters["@userId"].Direction = ParameterDirection.Output;

                            object result = cmd.ExecuteScalar();

                            if (result != null && result.ToString() == "1")
                            {
                                HttpContext.Current.Session["userId"] = cmd.Parameters["@userId"].Value.ToString();
                                return "登入成功";
                            }

                            return "帳號密碼錯誤";
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                    return "登入失敗";
                }
            }
            else
            {
                return "名稱和密碼不能含有非英文和數字且長度應在6到16之間";
            }
        }

        
        /// <summary>
        /// 判斷輸入值
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool SpecialChar(string userName, string pwd)
        {
            bool cheackUserName = Regex.IsMatch(userName, @"^[A-Za-z0-9]{6,16}$");
            bool cheackPwd = Regex.IsMatch(pwd, @"^[A-Za-z0-9]{6,16}$");

            if (cheackUserName && cheackPwd)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}