using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.Services;
using System.Text;
using System.Security.Cryptography;

namespace ShoppingWeb.Ajax
{
    public partial class UserHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 登入，如果成功就把sessionId寫入資料庫，並且把userId存到Session["userId"]
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [WebMethod]
        public static string LoginUser(string account, string pwd)
        {
            if (LoginSpecialChar(account, pwd))
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("pro_sw_getPwdAndEditSessionId", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.Add(new SqlParameter("@account", account));
                            cmd.Parameters.Add(new SqlParameter("@pwd", GetSHA256HashFromString(pwd)));
                            cmd.Parameters.Add(new SqlParameter("@sessionId", HttpContext.Current.Session.SessionID.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int));
                            cmd.Parameters["@userId"].Direction = ParameterDirection.Output;

                            object result = cmd.ExecuteScalar();

                            if (result != null && result.ToString() == "1")
                            {
                                HttpContext.Current.Session["userId"] = cmd.Parameters["@userId"].Value.ToString();
                                return "登入成功";
                            }

                            return "帳號密碼錯誤";
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                    return "登入失敗";
                }
            }
            else
            {
                return "帳號和密碼不能含有非英文和數字且長度應在6到16之間";
            }
        }


        /// <summary>
        /// 判斷輸入值
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool LoginSpecialChar(string account, string pwd)
        {
            bool cheackAccount = Regex.IsMatch(account, @"^[A-Za-z0-9]{6,16}$");
            bool cheackPwd = Regex.IsMatch(pwd, @"^[A-Za-z0-9]{6,16}$");

            if (cheackAccount && cheackPwd)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 取得管理員身分
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static object GetUserPermission()
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
                        using (SqlCommand cmd = new SqlCommand("pro_sw_getAccountRoles", con))
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
        /// 刪除管理員
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string RemoveUserInfo(string userId)
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
                        using (SqlCommand cmd = new SqlCommand("pro_sw_delUserData", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.Add(new SqlParameter("@userId", userId));

                            int r = (int)cmd.ExecuteScalar();
                            if (r > 0)
                            {
                                return "刪除成功";
                            }
                            else
                            {
                                return "刪除失敗";
                            }
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
        /// 設定Session["selectUserId"]
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool SetSessionSelectUserId(string userId)
        {
            HttpContext.Current.Session["selectUserId"] = userId;  //存儲資料到 Session 變數
            return true;
        }

        /// <summary>
        /// 顯示所有管理員，依照分頁
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [WebMethod]
        public static object GetAllUserData(int pageNumber, int pageSize)
        {
            bool loginResult = Utility.CheckDuplicateLogin();
            if (!loginResult)
            {
                return "重複登入";
            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getAllUserData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
                        cmd.Parameters.Add(new SqlParameter("@PageSize", pageSize));
                        cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                        cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;

                        SqlDataReader reader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
                        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);  // 計算總頁數，Math.Ceiling向上進位取整數

                        var result = new
                        {
                            Data = Utility.ConvertDataTableToJson(dt),
                            TotalPages = totalPages
                        };

                        return result;
                    }
                }
            }
        }

        /// <summary>
        /// 更改管理員身分
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [WebMethod]
        public static string ToggleUserRoles(string userId, string roles)
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
                        using (SqlCommand cmd = new SqlCommand("pro_sw_editRoles", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.Add(new SqlParameter("@userId", userId));
                            cmd.Parameters.Add(new SqlParameter("@roles", roles));

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
        /// 判斷使用者名稱是否存在，如果沒有就新增管理員
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [WebMethod]
        public static string RegisterNewUser(string account, string pwd, string roles)
        {
            bool loginResult = Utility.CheckDuplicateLogin();
            if (!loginResult)
            {
                return "重複登入";
            }
            else
            {
                if (AddUserSpecialChar(account, pwd, roles))
                {
                    try
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("pro_sw_addUserData", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                con.Open();
                                cmd.Parameters.Add(new SqlParameter("@account", account));
                                cmd.Parameters.Add(new SqlParameter("@pwd", GetSHA256HashFromString(pwd)));
                                cmd.Parameters.Add(new SqlParameter("@roles", roles));

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
                else
                {
                    return "帳號和密碼不能含有非英文和數字且長度應在6到16之間且腳色不能為空";
                }
            }
        }

        /// <summary>
        /// 判斷輸入值
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static bool AddUserSpecialChar(string account, string pwd, string roles)
        {
            bool cheackAccount = Regex.IsMatch(account, @"^[A-Za-z0-9]{6,16}$");
            bool cheackPwd = Regex.IsMatch(pwd, @"^[A-Za-z0-9]{6,16}$");
            bool cheackRoles = Regex.IsMatch(roles, @"^[0-9]{1,2}$");

            if (cheackAccount && cheackPwd && cheackRoles)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static string GetSHA256HashFromString(string strData)
        {
            byte[] bytValue = Encoding.UTF8.GetBytes(strData);
            byte[] retVal = SHA256.Create().ComputeHash(bytValue);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }



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
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                return ex;
            }

        }

        /// <summary>
        /// 更改密碼
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [WebMethod]
        public static string EditUser(string pwd)
        {
            bool loginResult = Utility.CheckDuplicateLogin();
            if (!loginResult)
            {
                return "重複登入";
            }
            else
            {
                if (RenewUserSpecialChar(pwd))
                {
                    try
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                        string sessionUserId = HttpContext.Current.Session["selectUserId"] as string;
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("pro_sw_editPwd", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                con.Open();

                                cmd.Parameters.Add(new SqlParameter("@userId", sessionUserId));
                                cmd.Parameters.Add(new SqlParameter("@pwd",GetSHA256HashFromString(pwd)));

                                int rowsAffected = (int)cmd.ExecuteScalar();

                                return (rowsAffected > 0) ? "修改成功" : "修改失敗";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                        return "錯誤";
                    }
                }
                else
                {
                    return "名稱和密碼不能含有非英文和數字且長度應在6到16之間且腳色不能為空";
                }
            }

        }

        /// <summary>
        /// 判斷輸入值
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static bool RenewUserSpecialChar(string pwd)
        {
            bool cheackPwd = Regex.IsMatch(pwd, @"^[A-Za-z0-9]{6,16}$");

            if (cheackPwd)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}