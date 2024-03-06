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

namespace ShoppingWeb.Web
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)  //頁面加載第一次時
            {
                if (Session["userName"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                
            }
            if (!AnyoneLongin())
            {
                Session["userName"] = null;
                Response.Write("<script>alert('重複登入，已被登出'); location.href='./Login.aspx'; </script>");
            }

        }


        public bool AnyoneLongin()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "SELECT f_sessionId FROM t_userInfo WHERE f_userName=@userName";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userName", Session["userName"]));
                        using (SqlDataAdapter sqlData = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sqlData.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];
                                Session["dbID"] = dr["f_sessionId"].ToString();

                                if (Session["dbID"].ToString() == Session.SessionID)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }

                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                return false;
            }
        }

    }
}

