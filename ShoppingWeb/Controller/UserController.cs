using Newtonsoft.Json.Linq;
using NLog;
using ShoppingWeb.Filters;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace ShoppingWeb.Controller
{
    [RoutePrefix("/api/Controller/user")]
    [RolesFilter((int)Roles.User)]
    public class UserController : BaseController
    {
        /// <summary>
        /// 刪除管理員
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RemoveUserInfo")]
        public int RemoveUserInfo([FromBody] JObject obj)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_delUserData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userId", obj["userId"].ToString()));

                        int r = (int)cmd.ExecuteScalar();

                        return (r > 0) ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                string fileName = ex.StackTrace.Substring(ex.StackTrace.IndexOf("位置"));
                logger.Error("後端錯誤時間: " + DateTime.Now + " 訊息: " + ex.Message + fileName + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 設定Session["selectUserId"]
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetSessionSelectUserId")]
        public int SetSessionSelectUserId([FromBody] JObject obj)
        {
            HttpContext.Current.Session["selectUserId"] = obj["userId"].ToString();  //存儲資料到 Session 變數
            return (int)DatabaseOperationResult.Success;
        }

        /// <summary>
        /// 顯示所有管理員，依照分頁
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetSessionSelectUserId")]
        public object GetAllUserData([FromBody] JObject obj)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getAllUserData", con))
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
                string fileName = ex.StackTrace.Substring(ex.StackTrace.IndexOf("位置"));
                logger.Error("後端錯誤時間: " + DateTime.Now + " 訊息: " + ex.Message + fileName + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 更改管理員身分
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ToggleUserRoles")]
        public int ToggleUserRoles([FromBody] JObject obj)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editRoles", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userId", obj["userId"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@roles", obj["roles"].ToString()));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return (rowsAffected > 0) ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure;

                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                string fileName = ex.StackTrace.Substring(ex.StackTrace.IndexOf("位置"));
                logger.Error("後端錯誤時間: " + DateTime.Now + " 訊息: " + ex.Message + fileName + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return (int)DatabaseOperationResult.Error;
            }
        }


        /// <summary>
        /// 新增管理員，會先判斷使用者名稱是否存在
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ToggleUserRoles")]
        public int RegisterNewUser([FromBody] JObject obj)
        {

            if (!AddUserSpecialChar(obj["account"].ToString(), obj["pwd"].ToString(), obj["roles"].ToString()))
            {
                return (int)UserStatus.InputError;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_addUserData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@account", obj["account"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@pwd", GetSHA256HashFromString(obj["pwd"].ToString())));
                        cmd.Parameters.Add(new SqlParameter("@roles", obj["roles"].ToString()));

                        int result = (int)cmd.ExecuteScalar();

                        return (result == 1) ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                string fileName = ex.StackTrace.Substring(ex.StackTrace.IndexOf("位置"));
                logger.Error("後端錯誤時間: " + DateTime.Now + " 訊息: " + ex.Message + fileName + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 判斷輸入值
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [NonAction]
        public bool AddUserSpecialChar(string account, string pwd, string roles)
        {
            bool cheackAccount = Regex.IsMatch(account, @"^[A-Za-z0-9]{6,16}$");
            bool cheackPwd = Regex.IsMatch(pwd, @"^[A-Za-z0-9]{6,16}$");
            bool cheackRoles = Regex.IsMatch(roles, @"^[0-9]{1,2}$");

            return cheackAccount && cheackPwd && cheackRoles;
        }


        /// <summary>
        /// 設定跳轉道編輯帳號頁面時，input裡面的預設值
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUserDataForEdit")]
        public object GetUserDataForEdit()
        {
            try
            {
                string sessionUserId = HttpContext.Current.Session["selectUserId"] as string;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getUserData", con))
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
                                UserId = dt.Rows[0]["f_id"],
                                Account = dt.Rows[0]["f_account"],
                                Roles = dt.Rows[0]["f_roles"],
                            };

                            return userObject;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                string fileName = ex.StackTrace.Substring(ex.StackTrace.IndexOf("位置"));
                logger.Error("後端錯誤時間: " + DateTime.Now + " 訊息: " + ex.Message + fileName + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return (int)DatabaseOperationResult.Error;
            }

        }

        /// <summary>
        /// 更改密碼
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditUser")]
        public int EditUser([FromBody] JObject obj)
        {

            if (!EditUserSpecialChar(obj["pwd"].ToString()))
            {
                return (int)UserStatus.InputError;
            }

            try
            {
                string sessionUserId = HttpContext.Current.Session["selectUserId"] as string;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editPwd", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        cmd.Parameters.Add(new SqlParameter("@userId", sessionUserId));
                        cmd.Parameters.Add(new SqlParameter("@pwd", GetSHA256HashFromString(obj["pwd"].ToString())));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return (rowsAffected > 0) ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                string fileName = ex.StackTrace.Substring(ex.StackTrace.IndexOf("位置"));
                logger.Error("後端錯誤時間: " + DateTime.Now + " 訊息: " + ex.Message + fileName + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 判斷輸入值
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [NonAction]
        public bool EditUserSpecialChar(string pwd)
        {
            bool cheackPwd = Regex.IsMatch(pwd, @"^[A-Za-z0-9]{6,16}$");

            return cheackPwd;
        }

    }
}