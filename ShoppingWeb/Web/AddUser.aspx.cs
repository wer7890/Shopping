using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

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

            if (IsCheck())
            {

                if (CheckRoles())
                {

                    if (!IsCheckUserName(userName))
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
                else
                {
                    Response.Write("<script>alert('你沒有這個權限')</script>");
                }

            }
            else
            {
                labAddUser.Text = "資料不能為空";
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
            bool b = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO t_userInfo2 VALUES(@name, @pwd, @roles)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();

                    cmd.Parameters.Add(new SqlParameter("@name", name));
                    cmd.Parameters.Add(new SqlParameter("@pwd", pwd));
                    cmd.Parameters.Add(new SqlParameter("@roles", roles));

                    int r = cmd.ExecuteNonQuery();

                    if (r > 0)
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
        /// 判斷新增的ID是否重複
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsCheckUserName(string name)
        {
            bool b = false;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM t_userInfo2 where f_userName=@name";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.Parameters.Add(new SqlParameter("@name", name));

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

            if (txbUserName.Text.Length == 0 | txbPwd.Text.Length == 0 | ddlRoles.SelectedValue.Length == 0)
            {
                b = false;
            }

            return b;
        }

        /// <summary>
        /// 判斷權限是否可以添加管理員
        /// </summary>
        /// <returns></returns>
        public bool CheckRoles()
        {
            bool b = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT f_userName, f_roles FROM t_userInfo2 WHERE f_userName=@name and f_roles<2";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.Parameters.Add(new SqlParameter("@name", Session["userName"]));

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
    }
}