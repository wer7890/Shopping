using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;


namespace ShoppingWeb.Ajax
{
    public partial class AddUserHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 註冊新管理員
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [WebMethod]
        public static string RegisterNewUser(string userName, string pwd, string roles)
        {
            try
            {

                if (DoesUserNameExist(userName))
                {
                    return "管理員名稱重複";
                }
                else if (AddUserData(userName, pwd, roles))
                {
                    return "新增成功";
                }
                else
                {
                    return "新增失敗";
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                return "發生內部錯誤: " + ex.Message;
            }
        }

        /// <summary>
        /// 判斷使用者名稱是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DoesUserNameExist(string name)
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


        /// <summary>
        /// 新增管理員資料
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static bool AddUserData(string name, string pwd, string roles)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //string sql = "INSERT INTO t_userInfo(f_userName, f_pwd, f_roles, f_sessionId) VALUES(@userName, @pwd, @roles, NULL)";
                    using (SqlCommand cmd = new SqlCommand("insertUserInfo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userName", name));
                        cmd.Parameters.Add(new SqlParameter("@pwd", pwd));
                        cmd.Parameters.Add(new SqlParameter("@roles", roles));

                        int r = (int)cmd.ExecuteScalar();

                        if (r > 0)
                        {
                            return true;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("Insert failed. Rows affected: " + r);
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