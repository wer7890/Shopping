using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingWeb.Ajax
{
    public partial class LoginHandler : System.Web.UI.Page
    {
     
        /// <summary>
        /// 登入，如果成功就把sessionId寫入資料庫，並且把userId存到Session["userId"]
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [WebMethod]
        public static string LoginUser(string account, string pwd)
        {

            if (!LoginSpecialChar(account, pwd))
            {
                return "帳號和密碼不能含有非英文和數字且長度應在6到16之間";
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getPwdAndEditSessionId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@account", account));
                        cmd.Parameters.Add(new SqlParameter("@pwd", BasePage.GetSHA256HashFromString(pwd)));
                        cmd.Parameters.Add(new SqlParameter("@sessionId", HttpContext.Current.Session.SessionID.ToString()));
                        cmd.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int));
                        cmd.Parameters["@userId"].Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(new SqlParameter("@roles", SqlDbType.Int));
                        cmd.Parameters["@roles"].Direction = ParameterDirection.Output;

                        object result = cmd.ExecuteScalar();

                        if (result != null && result.ToString() == "1")
                        {
                            HttpContext.Current.Session["userId"] = cmd.Parameters["@userId"].Value.ToString();
                            HttpContext.Current.Session["roles"] = cmd.Parameters["@roles"].Value;
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

        /// <summary>
        /// 判斷輸入值
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool LoginSpecialChar(string account, string pwd)
        {
            bool cheackAccount = Regex.IsMatch(account, @"^[A-Za-z0-9]{6,16}$");
            bool cheackPwd = Regex.IsMatch(pwd, @"^[A-Za-z0-9]{6,16}$");

            return (cheackAccount && cheackPwd);
        }
    }
}