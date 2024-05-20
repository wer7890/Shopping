using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
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
        public static object GetAllMemberData(int pageNumber, int pageSize)
        {
            if (!CheckDuplicateLogin())
            {
                return (int)UserStatus.DuplicateLogin;
            }

            if (!CheckRoles(PERMITTED_USER_ROLES))
            {
                return (int)UserStatus.AccessDenied;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pro_sw_getAllMemberData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.Add(new SqlParameter("@pageNumber", pageNumber));
                    cmd.Parameters.Add(new SqlParameter("@pageSize", pageSize));
                    cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                    cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
                    int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);  // 計算總頁數，Math.Ceiling向上進位取整數

                    var result = new
                    {
                        Data = ConvertDataTableToJson(dt),
                        TotalPages = totalPages
                    };

                    return result;
                }
            }

        }

        /// <summary>
        /// 是否啟用
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [WebMethod]
        public static int ToggleProductStatus(string memberId)
        {

            if (!CheckDuplicateLogin())
            {
                return (int)UserStatus.DuplicateLogin;
            }

            if (!CheckRoles(PERMITTED_USER_ROLES))
            {
                return (int)UserStatus.AccessDenied;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editMemberStatus", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@memberId", memberId));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return (rowsAffected > 0) ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure;

                    }
                }
            }
            catch (Exception ex)
            {
                Logger3 logger = new Logger3();
                logger.LogException(ex);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 更改會員等級
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        [WebMethod]
        public static int ToggleMemberLevel(string memberId, string level)
        {

            if (!CheckDuplicateLogin())
            {
                return (int)UserStatus.DuplicateLogin;
            }

            if (!CheckRoles(PERMITTED_USER_ROLES))
            {
                return (int)UserStatus.AccessDenied;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editMemberLevel", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@memberId", memberId));
                        cmd.Parameters.Add(new SqlParameter("@level", level));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return (rowsAffected > 0) ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure;

                    }
                }
            }
            catch (Exception ex)
            {
                Logger3 logger = new Logger3();
                logger.LogException(ex);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 新增會員資料
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static int AddMember(string account, string pwd, string name, string birthday, string phone, string email, string address)
        {

            if (!CheckDuplicateLogin())
            {
                return (int)UserStatus.DuplicateLogin;
            }

            if (!CheckRoles(PERMITTED_USER_ROLES))
            {
                return (int)UserStatus.AccessDenied;
            }

            if (!AddMemberSpecialChar(account, pwd, name, birthday, phone, email, address))
            {
                return (int)UserStatus.InputError;
            }

            try
            {
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

                        int result = (int)cmd.ExecuteScalar();

                        return (result == 1) ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger3 logger = new Logger3();
                logger.LogException(ex);
                return (int)DatabaseOperationResult.Error;
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