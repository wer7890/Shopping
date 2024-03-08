using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ShoppingWeb.Ajax
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 確認是否有重複登入
        /// </summary>
        /// <returns></returns>
        public bool AnyoneLongin()
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

                            // 統一 SessionID 的存取方式
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