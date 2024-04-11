using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;

namespace ShoppingWeb.Ajax
{
    public partial class MemberHandler : BasePage
    {
        private const int PERMITTED_USER_ROLES = 2;

        public MemberHandler()
        {
            
        }


        /// <summary>
        /// 一開始顯示所有會員資訊
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static object GetAllMemberData()
        {

            if (!Utility.CheckRoles(PERMITTED_USER_ROLES))
            {
                return "權限不足";
            }

            string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pro_sw_getAllMemberData", con))
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

        /// <summary>
        /// 是否啟用
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string ToggleProductStatus(string memberId)
        {

            if (!Utility.CheckRoles(PERMITTED_USER_ROLES))
            {
                return "權限不足";
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editMemberStatus", con))
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

        /// <summary>
        /// 更改會員等級
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        [WebMethod]
        public static string ToggleMemberLevel(string memberId, string level)
        {
            if (!Utility.CheckRoles(PERMITTED_USER_ROLES))
            {
                return "權限不足";
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editMemberLevel", con))
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

        /// <summary>
        /// 新增會員資料
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string AddMember(string account, string pwd, string name, string birthday, string phone, string email, string address)
        {

            if (!Utility.CheckRoles(PERMITTED_USER_ROLES))
            {
                return "權限不足";
            }

            if (!AddMemberSpecialChar(account, pwd, name, birthday, phone, email, address))
            {
                return "輸入值錯誤";
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_addMemberData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@account", account));
                        cmd.Parameters.Add(new SqlParameter("@pwd", pwd));
                        cmd.Parameters.Add(new SqlParameter("@name", name));
                        cmd.Parameters.Add(new SqlParameter("@birthday", birthday));
                        cmd.Parameters.Add(new SqlParameter("@phone", phone));
                        cmd.Parameters.Add(new SqlParameter("@email", email));
                        cmd.Parameters.Add(new SqlParameter("@address", address));

                        string result = cmd.ExecuteScalar().ToString();

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
        /// 判斷輸入值
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool AddMemberSpecialChar(string account, string pwd, string name, string birthday, string phone, string email, string address)
        {
            string s = birthday;
            bool cheackAccount = Regex.IsMatch(account, @"^[A-Za-z0-9]{6,16}$");
            bool cheackPwd = Regex.IsMatch(pwd, @"^[A-Za-z0-9]{6,16}$");
            bool cheackName = Regex.IsMatch(name, @"^[\u4E00-\u9FFF]{1,15}$");
            bool cheackBirthday = Regex.IsMatch(birthday, @"^[0-9-]{8,10}$");
            bool cheackPhone = Regex.IsMatch(phone, @"^[0-9]{10}$");
            bool cheackEmail = Regex.IsMatch(email, @"^[a-zA-Z0-9_.+-]{1,25}@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
            bool cheackAddress = Regex.IsMatch(address, @"^[\u4E00-\u9FFF0-9A-Za-z]{2,50}$");

            return (cheackAccount && cheackPwd && cheackName && cheackBirthday && cheackPhone && cheackEmail && cheackAddress);
        }
    }
}