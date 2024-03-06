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

namespace ShoppingWeb.Ajax
{
    public partial class IndexHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 取得管理員身分
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string CheckUserPermission()
        {
            string strRoles = null;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                string sessionUserName = HttpContext.Current.Session["UserName"] as string;


                using (SqlConnection con = new SqlConnection(connectionString))
                {


                    string sql = "SELECT f_userName, f_roles FROM t_userInfo WHERE f_userName=@name";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@name", sessionUserName));

                        using (SqlDataAdapter sqlData = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sqlData.Fill(dt);
                            DataRow dr = dt.Rows[0];
                            strRoles = dr["f_roles"].ToString();
                        }
                    }

                }

                return strRoles;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                return strRoles;
            }
        }


        /// <summary>
        /// 刪除Session["userName"]
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static bool DeleteSession()
        {
            HttpContext.Current.Session["userName"] = null;
            return true;
        }
    }
}