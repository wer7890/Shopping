using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

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

                bool loginSuccessful = false;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //讀取資料庫的sql語法
                    string sql = "SELECT f_userName, f_pwd FROM t_userInfo WHERE f_userName=@name COLLATE SQL_Latin1_General_CP1_CS_AS";

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
                                    //在認證帳號密碼後，先將SessionID儲存
                                    //HttpContext.Current.Session.Add("ID", HttpContext.Current.Session.SessionID);
                                    HttpContext.Current.Session["userName"] = userName;
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



        
    }
}