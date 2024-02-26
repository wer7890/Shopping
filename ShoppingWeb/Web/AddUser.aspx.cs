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
            string userId = txbUserId.Text.Trim();
            string pwd = txbPwd.Text.Trim();
            string userName = txbUserName.Text.Trim();
            string roles = ddlRoles.SelectedValue;
            string permissions = ddlPermissions.Text.Trim();

            if (IsCheck())
            {

                if (!IsCheckUserId(userId))
                {

                    if (IsAddUser(userId, pwd, userName, roles, permissions))
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
                    labAddUser.Text = "管理員ID重複";
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
        public bool IsAddUser(string id, string pwd, string name, string roles, string permissions)
        {
            bool b = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO t_userInfo2 VALUES(@Id, @pwd, @name, @roles, @permissions)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();

                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    cmd.Parameters.Add(new SqlParameter("@pwd", pwd));
                    cmd.Parameters.Add(new SqlParameter("@name", name));
                    cmd.Parameters.Add(new SqlParameter("@roles", roles));
                    cmd.Parameters.Add(new SqlParameter("@permissions", permissions));

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
        public bool IsCheckUserId(string id)
        {
            bool b = false;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT * from t_userInfo2 where f_userId=@Id";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.Parameters.Add(new SqlParameter("@Id", id));

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

            if (txbUserId.Text.Length == 0 | txbPwd.Text.Length == 0 | txbUserName.Text.Length == 0 | ddlRoles.SelectedValue.Length == 0 | ddlPermissions.Text.Length ==0)
            {
                b = false;
            }

            return b;
        }

    }
}