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

namespace ShoppingWeb.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text.Trim(); //Trim()刪除空白字元
            string pwd = txbPassword.Text.Trim();
            labLogin.Text = "";

            if (CheckLength(userName, pwd))  //檢查用戶是否輸入帳號密碼，以節省性能
            {
                if (IsLogin(userName, pwd))
                {
                    labLogin.Text = "登入成功";
                    Session["userName"] = userName;
                    Response.Redirect("AddUser.aspx");
                }
                else
                {
                    labLogin.Text = "用戶名或密碼錯誤";
                }
            }
           

        }

        /// <summary>
        /// 檢查資料庫中是否有資料
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool IsLogin(string name, string pwd)
        {
            bool loginSuccessful = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //讀取資料庫的sql語法
                string sql = "SELECT f_userName, f_pwd FROM t_userInfo WHERE f_userName=@name";

                using (SqlCommand cmd = new SqlCommand(sql, con))  //資料庫連接對象
                {
                    con.Open();

                    //給sql語句中的變量進行附值
                    SqlParameter parameter1 = new SqlParameter("@name", name);
                    SqlParameter parameter2 = new SqlParameter("@pwd", pwd);

                    //把parameter變量對象附值給cmd對象
                    cmd.Parameters.Add(parameter1);
                    cmd.Parameters.Add(parameter2);

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

        /// <summary>
        /// 檢查輸入框是否為空
        /// </summary>
        /// <returns></returns>
        public bool CheckLength(string userName, string pwd)
        {
            bool checkLengthResult = true;

            if (userName.Length == 0 | pwd.Length == 0)
            {
                labLogin.Text = "用戶名跟密碼不能為空";
                return false;
            }

            if (!IsSpecialChar(userName, pwd))
            {
                labLogin.Text = "用戶名跟密碼不可包含特殊字元";
                return false;
            }

            return checkLengthResult;
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

    }
}