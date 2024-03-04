using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingWeb.Web
{
    public partial class UpDataUser : System.Web.UI.Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                if (!IsPostBack)  //頁面加載第一次時
                {

                    if (Session["userName"] == null)
                    {
                        Response.Redirect("Login.aspx");
                    }

                    string sql = "SELECT f_userId, f_userName, f_pwd, f_roles FROM t_userInfo WHERE f_userId=@id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Id", id));

                        using (SqlDataAdapter sqlData = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sqlData.Fill(dt);
                            DataRow dr = dt.Rows[0];
                            labUserId.Text = id;
                            txbUserName.Text = dr["f_userName"].ToString();
                            txbPwd.Text = dr["f_pwd"].ToString();
                            ListItem item = ddlRoles.Items.FindByValue(dr["f_roles"].ToString());

                            if (item != null)
                            {
                                item.Selected = true;
                            }

                        }
                    }
                }

            }
        }

        protected void txbAddUser_Click(object sender, EventArgs e)
        {
            int id = int.Parse(labUserId.Text);
            string pwd = txbPwd.Text;
            string userName = txbUserName.Text;
            int roles = int.Parse(ddlRoles.SelectedValue);

            if (CheckLength(userName, pwd))
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "UPDATE t_userInfo SET f_userName=@userName, f_pwd=@pwd, f_roles=@roles WHERE f_userId=@id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        con.Open();

                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        cmd.Parameters.Add(new SqlParameter("@pwd", pwd));
                        cmd.Parameters.Add(new SqlParameter("@userName", userName));
                        cmd.Parameters.Add(new SqlParameter("@roles", roles));

                        int r = cmd.ExecuteNonQuery();

                        if (r > 0)
                        {
                            Response.Redirect("SearchUser.aspx");
                        }
                        else
                        {
                            Response.Write("<script>alert('修改失敗')</script>");
                        }
                    }
                }
            }
            
        }

        /// <summary>
        /// 檢查輸入框是否為空，以及帳號密碼長度
        /// </summary>
        /// <returns></returns>
        public bool CheckLength(string userName, string pwd)
        {
            bool lengthResult = true;

            if (userName.Length == 0 | pwd.Length == 0 | ddlRoles.SelectedValue.Length == 0)
            {
                labAddUser.Text = "資料不能為空";
                return false;
            }

            if (userName.Length < 6 | pwd.Length < 6)
            {
                labAddUser.Text = "用戶名跟密碼長度不能小於6";
                return false;
            }

            if (userName.Length > 16 | pwd.Length > 16)
            {
                labAddUser.Text = "用戶名跟密碼長度不能大於16";
                return false;
            }

            if (!IsSpecialChar(userName, pwd))
            {
                labAddUser.Text = "用戶名跟密碼不可包含特殊字元";
                return false;
            }

            return lengthResult;
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