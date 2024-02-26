using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace ShoppingWeb.Web
{
    public partial class SearchUser : System.Web.UI.Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridViewBinding();
        }

        /// <summary>
        /// 用來更新及綁定GridView
        /// </summary>
        protected void GridViewBinding()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT f_userId, f_pwd, f_userName, f_roles, f_permissions FROM t_userInfo2";

                using (SqlDataAdapter sqlData = new SqlDataAdapter(sql, con))
                {
                    DataSet dt = new DataSet();
                    sqlData.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }

        /// <summary>
        /// 用於修改功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string id = GridView1.Rows[e.NewEditIndex].Cells[0].Text;
            Response.Redirect("UpDataUser.aspx?id=" + id);
        }

        /// <summary>
        /// 用於刪除功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridView1.Rows[e.RowIndex].Cells[0].Text;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM t_userInfo2 WHERE f_userId=@id";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.Parameters.Add(new SqlParameter("@Id", id));

                    int r = cmd.ExecuteNonQuery();

                    if (r > 0)
                    {
                        Response.Write("<script>alert('刪除成功')</script>");
                        GridViewBinding();
                    }
                    else
                    {
                        Response.Write("<script>alert('刪除失敗')</script>");
                    }
                }
            }
        }
    }
}