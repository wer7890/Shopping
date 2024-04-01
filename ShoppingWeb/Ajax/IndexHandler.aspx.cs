using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;

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
        public static object GetUserPermission()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("getUserAccountRoles", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userId", HttpContext.Current.Session["userId"]));
                        SqlDataAdapter da = new SqlDataAdapter();
                        DataTable dt = new DataTable();
                        da.SelectCommand = cmd;
                        da.Fill(dt);

                        var result = new
                        {
                            Account = dt.Rows[0]["f_account"].ToString(),
                            Roles = dt.Rows[0]["f_roles"].ToString(),
                        };

                        return result;
                    }

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                return "發生內部錯誤: " + ex.Message;
            }
        }

        /// <summary>
        /// 刪除Session["userId"]
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static bool DeleteSession()
        {
            HttpContext.Current.Session["userId"] = null;
            return true;
        }

        /// <summary>
        /// 確認是否有重複登入
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static bool AnyoneLongin()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("getSessionId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userId", HttpContext.Current.Session["userId"]));

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            HttpContext.Current.Session["dbID"] = result.ToString();

                            string currentSessionID = HttpContext.Current.Session.SessionID;
                            string dbSessionID = HttpContext.Current.Session["dbID"].ToString();

                            if (dbSessionID != currentSessionID)
                            {
                                HttpContext.Current.Session["userId"] = null;
                                return false;
                            }

                            return true;
                        }
                        else
                        {
                            return false;
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
