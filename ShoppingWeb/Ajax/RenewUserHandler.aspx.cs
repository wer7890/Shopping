using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;


namespace ShoppingWeb.Ajax
{
    public partial class RenewUserHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        public static string publicUserName = "";

        /// <summary>
        /// 設定跳轉道編輯帳號頁面時，input裡面的預設值
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static object GetUserDataForEdit()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                string sessionUserId = HttpContext.Current.Session["userID"] as string;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //string sql = "SELECT f_userId, f_userName, f_pwd, f_roles FROM t_userInfo WHERE f_userId=@id";
                    using (SqlCommand cmd = new SqlCommand("getUserData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userId", sessionUserId));

                        using (SqlDataAdapter sqlData = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sqlData.Fill(dt);

                            // 構建包含數據的匿名對象
                            var userObject = new
                            {
                                UserId = dt.Rows[0]["f_userId"],
                                UserName = dt.Rows[0]["f_userName"],
                                Password = dt.Rows[0]["f_pwd"],
                                Roles = dt.Rows[0]["f_roles"],
                            };
                            publicUserName = dt.Rows[0]["f_userName"].ToString() ;


                            return userObject;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                return ex;
            }

        }


        /// <summary>
        /// 更改帳號
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [WebMethod]
        public static string EditUser(string userId, string userName, string pwd, string roles)
        {
            if (!CheckUpdataUserName(userName))
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                    string sessionUserId = HttpContext.Current.Session["userID"] as string;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        //string sql = "UPDATE t_userInfo SET f_userName=@userName, f_pwd=@pwd, f_roles=@roles WHERE f_userId=@id";
                        using (SqlCommand cmd = new SqlCommand("updataUser", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();

                            cmd.Parameters.Add(new SqlParameter("@userId", sessionUserId));
                            cmd.Parameters.Add(new SqlParameter("@pwd", pwd));
                            cmd.Parameters.Add(new SqlParameter("@userName", userName));
                            cmd.Parameters.Add(new SqlParameter("@roles", roles));

                            int rowsAffected = (int)cmd.ExecuteScalar();

                            return (rowsAffected > 0) ? "修改成功" : "修改失敗";
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                    return "修改失敗";
                }
            }
            else
            {
                return "管理員名稱重複";
            } 

        }

        /// <summary>
        /// 判斷修改的ID是否重複
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CheckUpdataUserName(string name)
        {
            if (publicUserName != name)
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        //string sql = "SELECT COUNT(*) FROM t_userInfo where f_userName=@userName";
                        using (SqlCommand cmd = new SqlCommand("getUserNameCount", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.Add(new SqlParameter("@userName", name));

                            int count = (int)cmd.ExecuteScalar();

                            return count > 0;

                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
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