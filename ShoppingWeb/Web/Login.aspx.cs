using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data;
using System.Web.Services;

namespace ShoppingWeb.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static bool LoginUser(string userName, string pwd)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                bool loginSuccessful = false;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //讀取資料庫的sql語法
                    string sql = "SELECT f_userName, f_pwd FROM t_userInfo WHERE f_userName=@name";

                    using (SqlCommand cmd = new SqlCommand(sql, con))  //資料庫連接對象
                    {
                        con.Open();

                        //給sql語句中的變量進行附值
                        SqlParameter parameter1 = new SqlParameter("@name", userName);

                        //把parameter變量對象附值給cmd對象
                        cmd.Parameters.Add(parameter1);

                        using (SqlDataAdapter sqlData = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sqlData.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];
                                string storedPwd = dr["f_pwd"].ToString();

                                if (storedPwd == pwd)
                                {
                                    setSession(HttpContext.Current.Handler as Page, userName);
                                    loginSuccessful = true;
                                }
                                else
                                {
                                    loginSuccessful = false;
                                }

                            }
                            else
                            {
                                loginSuccessful = false;
                            }
                        }

                    }
                }
                return loginSuccessful;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                return false;
            }
        }




        /// <summary>
        /// 判斷是否有非法字元
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool IsSpecialChar(string userName, string pwd)
        {
            bool regUserName = Regex.IsMatch(userName, @"^[A-Za-z0-9]+$");
            bool regPwd = Regex.IsMatch(pwd, @"^[A-Za-z0-9]+$");

            if (regUserName & regPwd)
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// 設定Session
        /// </summary>
        /// <param name="page"></param>
        /// <param name="userName"></param>
        public static void setSession(Page page, string userName)
        {
            page.Session["userName"] = userName;
        }

    }
}