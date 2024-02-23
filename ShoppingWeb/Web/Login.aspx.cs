using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

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

            if (IsCheck())  //檢查用戶是否輸入帳號密碼，以節省性能
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
            else
            {
                labLogin.Text = "用戶名跟密碼不能為空";
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
            bool b = false;
            using (SqlConnection con = new SqlConnection(connectionString))  
            {
                //讀取資料庫的sql語法
                string sql = "SELECT * from t_userInfo where f_userName=@name and f_pwd=@pwd";

                using (SqlCommand cmd = new SqlCommand(sql, con))  //資料庫連接對象
                {
                    con.Open();

                    //給sql語句中的變量進行附值
                    SqlParameter parameter1 = new SqlParameter("@name", name);
                    SqlParameter parameter2 = new SqlParameter("@pwd", pwd);

                    //把parameter變量對象附值給cmd對象
                    cmd.Parameters.Add(parameter1);
                    cmd.Parameters.Add(parameter2);

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        b = true;
                    }
                    else
                    {
                        b = false;
                    }

                }
            }
            return b;
        }

        /// <summary>
        /// 檢查輸入框是否為空
        /// </summary>
        /// <returns></returns>
        public bool IsCheck()
        {
            bool b = true;

            if (txbUserName.Text.Length == 0)
            {
                b = false;
            }
            if (txbPassword.Text.Length == 0)
            {
                b = false;
            }

            return b;
        }

        
    }
}