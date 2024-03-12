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
        public static string GetUserPermission()
        {
            string strRoles = null;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //string sql = "SELECT f_roles FROM t_userInfo WHERE f_userName=@userName";
                    using (SqlCommand cmd = new SqlCommand("getRoles", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userName", HttpContext.Current.Session["userName"]));

                        string result = cmd.ExecuteScalar().ToString();

                        if (result != null)
                        {
                            strRoles = result.ToString();
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
                    //string sql = "SELECT f_sessionId FROM t_userInfo WHERE f_userName=@userName";
                    using (SqlCommand cmd = new SqlCommand("getSessionId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userName", HttpContext.Current.Session["userName"]));

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            HttpContext.Current.Session["dbID"] = result.ToString();

                            string currentSessionID = HttpContext.Current.Session.SessionID;
                            string dbSessionID = HttpContext.Current.Session["dbID"].ToString();

                            if (dbSessionID != currentSessionID)
                            {
                                HttpContext.Current.Session["userName"] = null;
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
