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
    public partial class MainMaster : System.Web.UI.MasterPage
    {

        string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["userName"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                if (!IsPostBack)  //頁面加載第一次時
                {
                    string sql = "SELECT f_userName, f_roles FROM t_userInfo WHERE f_userName=@name";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@name", Session["userName"]));

                        using (SqlDataAdapter sqlData = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sqlData.Fill(dt);
                            DataRow dr = dt.Rows[0];
                            string s = "";
                            switch (dr["f_roles"].ToString())
                            {
                                case "0":
                                    s = "root";
                                    break;

                                case "1":
                                    s = "超級管理員";
                                    break;

                                case "2":
                                    s = "會員管理員";
                                    break;

                                case "3":
                                    s = "商品管理員";
                                    break;

                                default:
                                    break;
                            }
                            labUserRoles.Text = "身分: " + s;
                        }
                    }
                }

            }
        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            Session["userName"] = null;
            Response.Redirect("Login.aspx");
        }


    }
}