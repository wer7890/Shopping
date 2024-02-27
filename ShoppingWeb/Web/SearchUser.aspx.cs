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
            if (CheckBrowse())
            {

                if (!IsPostBack)
                {
                    GridViewBinding();
                }
                
            }
            else
            {
                Response.Redirect("AddUser.aspx");
            }

        }

        /// <summary>
        /// 用來更新及綁定GridView
        /// </summary>
        protected void GridViewBinding()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT f_userId, f_userName, f_pwd, f_roles FROM t_userInfo2 WHERE f_roles>0";

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
            int roles = Convert.ToInt32(GridView1.Rows[e.NewEditIndex].Cells[3].Text);
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
            int roles = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[3].Text);

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

        /// <summary>
        /// 判斷權限是否可以查詢，修改，刪除管理員
        /// </summary>
        /// <returns></returns>
        public bool CheckRoles(int roles)
        {
            bool b = false;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT f_userName, f_roles FROM t_userInfo2 WHERE f_userName=@name and f_roles<=@roles";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.Parameters.Add(new SqlParameter("@name", Session["userName"]));
                    cmd.Parameters.Add(new SqlParameter("@roles", roles));

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
        /// 是否有權限瀏覽此網頁
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool CheckBrowse()
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

        
        /// <summary>
        /// 當GridView的標題SortExpression會觸發的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            // 獲取排序字段和排序方向
            string sortExpression = e.SortExpression;
            string sortDirection = GetSortDirection(sortExpression);

            // 在這裡添加排序邏輯，重新绑定數據
            GridViewBinding(sortExpression, sortDirection);
        }

        /// <summary>
        /// 獲取排序方向
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private string GetSortDirection(string column)
        {
            // 默認升序
            string sortDirection = "ASC";

            // 检查是否之前已经排序
            string currentSortColumn = ViewState["SortColumn"] as string;
            if (currentSortColumn != null && currentSortColumn == column)
            {
                // 如果是之前已经排序的列，則切换排序方向
                string currentSortDirection = ViewState["SortDirection"] as string;
                if (currentSortDirection != null && currentSortDirection == "ASC")
                {
                    sortDirection = "DESC";
                }
            }

            // 保存當前排序列和方向
            ViewState["SortColumn"] = column;
            ViewState["SortDirection"] = sortDirection;

            return sortDirection;
        }

        /// <summary>
        /// 數據绑定方法，根据排序字段和排序方向进行排序
        /// </summary>
        /// <param name="sortExpression"></param>
        /// <param name="sortDirection"></param>
        private void GridViewBinding(string sortExpression, string sortDirection)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT f_userId, f_userName, f_pwd, f_roles FROM t_userInfo2 WHERE f_roles>0";

                // 添加排序邏輯
                if (!string.IsNullOrEmpty(sortExpression))
                {
                    sql += " ORDER BY " + sortExpression + " " + sortDirection;
                }

                using (SqlDataAdapter sqlData = new SqlDataAdapter(sql, con))
                {
                    DataSet dt = new DataSet();
                    sqlData.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }
    }
}