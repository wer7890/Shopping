using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ShoppingWeb.Web
{
    public partial class AddUser : System.Web.UI.Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txbAddUser_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text.Trim();
            string pwd = txbPwd.Text.Trim();
            string roles = ddlRoles.SelectedValue;
            labAddUser.Text = "";


            if (CheckRoles())  //檢查權限
            {

                if (CheckLength(userName, pwd))  //檢查長度及空白和特殊字元
                {

                    if (!IsCheckUserName(userName))  //檢查名稱是否重複
                    {

                        if (IsAddUser(userName, pwd, roles))
                        {
                            Response.Write("<script>alert('新增成功')</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('新增失敗')</script>");
                        }

                    }
                    else
                    {
                        labAddUser.Text = "管理員名稱重複";
                    }
                }

            }
            else 
            {
                Response.Write("<script>alert('你沒有這個權限')</script>");
            }
           
        }

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="pwd"></param>
        /// <param name="name"></param>
        /// <param name="roles"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public bool IsAddUser(string name, string pwd, string roles)
        {
            bool addResult = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO t_userInfo VALUES(@name, @pwd, @roles)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();

                    cmd.Parameters.Add(new SqlParameter("@name", name));
                    cmd.Parameters.Add(new SqlParameter("@pwd", pwd));
                    cmd.Parameters.Add(new SqlParameter("@roles", roles));

                    int r = cmd.ExecuteNonQuery();

                    if (r > 0)
                    {
                        addResult = true;
                    }
                    else
                    {
                        addResult = false;
                    }
                }
            }

            return addResult;
        }

        /// <summary>
        /// 判斷新增的ID是否重複
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsCheckUserName(string name)
        {
            bool userNameExists = false;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT f_userName FROM t_userInfo where f_userName=@name";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.Parameters.Add(new SqlParameter("@name", name));

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        userNameExists = true;
                    }
                    else
                    {
                        userNameExists = false;
                    }

                }
            }

            return userNameExists;
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

        /// <summary>
        /// 判斷權限是否可以添加管理員
        /// </summary>
        /// <returns></returns>
        public bool CheckRoles()
        {
            bool hasPermission = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT f_userName, f_roles FROM t_userInfo WHERE f_userName=@name and f_roles<2";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.Parameters.Add(new SqlParameter("@name", Session["userName"]));

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        hasPermission = true;
                    }
                    else
                    {
                        hasPermission = false;
                    }

                }
            }

            return hasPermission;
        }


        
    }
}