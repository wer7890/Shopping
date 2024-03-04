using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ShoppingWeb.Web
{
    public partial class SearchUser1 : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)  //頁面加載第一次時
            {
                if (Session["userName"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
                else
                {

                    // 建立 SQL 查詢
                    string sql = "SELECT f_userId, f_userName, f_pwd, f_roles FROM t_userInfo WHERE f_roles > 0";

                    // 使用 SqlConnection 連接資料庫
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        // 打開資料庫連接
                        con.Open();

                        // 使用 SqlCommand 执行 SQL 查询
                        using (SqlCommand command = new SqlCommand(sql, con))
                        {
                            // 使用 SqlDataReader 讀取資料
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                // 動態構建 HTML 表格
                                string tableRows = "";

                                while (reader.Read())
                                {
                                    // 編輯和刪除的按鈕，您可以根據需要設計連結或其他操作
                                    string editButton = $"<button class='btn btn-primary' onclick='editUser({reader["f_userId"]})'>編輯</button>";
                                    string deleteButton = $"<button class='btn btn-danger' onclick='deleteUser({reader["f_userId"]})'>刪除</button>";

                                    tableRows += "<tr>" +
                                        $"<td>{reader["f_userId"]}</td>" +
                                        $"<td>{reader["f_userName"]}</td>" +
                                        $"<td>{reader["f_pwd"]}</td>" +
                                        $"<td>{reader["f_roles"]}</td>" +
                                        $"<td>{editButton}</td>" +
                                        $"<td>{deleteButton}</td>" +
                                        "</tr>";
                                }

                                // 將動態生成的表格行添加到 Literal 控制項
                                tableBodyLiteral.Text = tableRows;
                            }
                        }
                    }
                }
            }
        }

        [WebMethod]
        public static bool deleteUser(string userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM t_userInfo WHERE f_userId=@id";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.Parameters.Add(new SqlParameter("@Id", userId));
                    int r = cmd.ExecuteNonQuery();
                    if (r > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            
        }

        

    }
}