using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
                    string sql = "SELECT * FROM t_userInfo2 WHERE f_userId=@id";
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
            int id = Convert.ToInt32(labUserId.Text);
            string pwd = txbPwd.Text;
            string userName = txbUserName.Text;
            int roles = Convert.ToInt32(ddlRoles.SelectedValue);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "UPDATE t_userInfo2 SET f_userName=@userName, f_pwd=@pwd, f_roles=@roles WHERE f_userId=@id";
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
}