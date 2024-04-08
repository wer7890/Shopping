using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

namespace ShoppingWeb.Ajax
{
    public partial class MemberHandler : System.Web.UI.Page
    {
        private const int PERMITTED_USER_ROLES = 2;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 一開始顯示所有會員資訊
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static object GetAllMemberData()
        {
            bool loginResult = Utility.CheckDuplicateLogin();
            if (!loginResult)
            {
                return "重複登入";
            }
            else
            {
                // 連接資料庫，獲取使用者資料
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("getAllMemberData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        // 將資料轉換為 JSON 格式返回
                        return Utility.ConvertDataTableToJson(dt);
                    }
                }
            }

        }

        /// <summary>
        /// 是否啟用
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string ToggleProductStatus(string memberId)
        {
            bool loginResult = Utility.CheckDuplicateLogin();
            if (!loginResult)
            {
                return "重複登入";
            }
            else
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("toggleMemberStatus", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.Add(new SqlParameter("@memberId", memberId));

                            int rowsAffected = (int)cmd.ExecuteScalar();

                            return (rowsAffected > 0) ? "更改成功" : "更改失敗";

                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                    return "錯誤";
                }
            }
        }

        /// <summary>
        /// 更改會員等級
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        [WebMethod]
        public static string ToggleMemberLevel(string memberId, string level)
        {
            bool loginResult = Utility.CheckDuplicateLogin();
            if (!loginResult)
            {
                return "重複登入";
            }
            else
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("toggleMemberLevel", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.Add(new SqlParameter("@memberId", memberId));
                            cmd.Parameters.Add(new SqlParameter("@level", level));

                            int rowsAffected = (int)cmd.ExecuteScalar();

                            return (rowsAffected > 0) ? "更改成功" : "更改失敗";

                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                    return "錯誤";
                }
            }
        }

        /// <summary>
        /// 新增會員資料
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string RandomMember()
        {

            if (!Utility.CheckDuplicateLogin())
            {
                return "重複登入";
            }

            if (!Utility.CheckRoles(PERMITTED_USER_ROLES))
            {
                return "權限不足";
            }

            //int numberOfMembersToAdd = 10;
            string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AddMember", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    string username = GenerateRandomUsername();
                    string email = GenerateRandomEmail();
                    string password = GenerateRandomPassword();

                    cmd.Parameters.Add(new SqlParameter("@Username", username));


                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "成功";
                    }
                    else
                    {
                        return "失敗";
                    }
                }
            }
        }

        // 生成隨機的會員資料方法示例
        static string GenerateRandomUsername()
        {
            return "RandomUsername" + Guid.NewGuid().ToString().Substring(0, 8);
        }

        static string GenerateRandomEmail()
        {
            return "random" + Guid.NewGuid().ToString().Substring(0, 8) + "@example.com";
        }

        static string GenerateRandomPassword()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}