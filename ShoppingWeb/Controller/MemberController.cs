using Newtonsoft.Json.Linq;
using NLog;
using ShoppingWeb.Ajax;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace ShoppingWeb.Controller
{
    [RoutePrefix("/api/Controller/member")]
    public class MemberController : Base
    {
        /// <summary>
        /// 會員系統所要求的權限
        /// </summary>
        private const int PERMITTED_USER_ROLES = 2;

        /// <summary>
        /// 一開始顯示所有會員資訊
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAllMemberData")]
        public object GetAllMemberData([FromBody] JObject obj)
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
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getAllMemberData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", obj["pageNumber"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@pageSize", obj["pageSize"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@beforePagesTotal", obj["beforePagesTotal"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                        cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;
                        SqlDataReader reader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
                        int totalPages = (int)Math.Ceiling((double)totalCount / (int)obj["pageSize"]);  // 計算總頁數，Math.Ceiling向上進位取整數

                        var result = new
                        {
                            Data = ConvertDataTableToJson(dt),
                            TotalPages = totalPages
                        };

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 是否啟用
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ToggleProductStatus")]
        public int ToggleProductStatus([FromBody] JObject obj)
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
                        cmd.Parameters.Add(new SqlParameter("@memberId", obj["memberId"].ToString()));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return (rowsAffected > 0) ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure;

                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 更改會員等級
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ToggleMemberLevel")]
        public int ToggleMemberLevel([FromBody] JObject obj)
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
                        cmd.Parameters.Add(new SqlParameter("@memberId", obj["memberId"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@level", obj["level"].ToString()));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return (rowsAffected > 0) ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure;

                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 新增會員資料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AddMember")]
        public int AddMember([FromBody] JObject obj)
        {

            if (!CheckDuplicateLogin())
            {
                return (int)UserStatus.DuplicateLogin;
            }

            if (!CheckRoles(PERMITTED_USER_ROLES))
            {
                return (int)UserStatus.AccessDenied;
            }

            if (!AddMemberSpecialChar(obj["account"].ToString(), obj["pwd"].ToString(), obj["name"].ToString(), obj["birthday"].ToString(), obj["phone"].ToString(), obj["email"].ToString(), obj["address"].ToString()))
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
                        cmd.Parameters.Add(new SqlParameter("@account", obj["account"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@pwd", obj["pwd"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@name", obj["name"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@birthday", obj["birthday"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@phone", obj["phone"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@email", obj["email"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@address", obj["address"].ToString()));

                        int result = (int)cmd.ExecuteScalar();

                        return (result == 1) ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 判斷輸入值
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [NonAction]
        public bool AddMemberSpecialChar(string account, string pwd, string name, string birthday, string phone, string email, string address)
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