using Newtonsoft.Json.Linq;
using NLog;
using ShoppingWeb.Ajax;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace ShoppingWeb.Controller
{
    [RoutePrefix("/api/Controller/login")]
    public class LoginController : ApiController
    {
        public readonly string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

        /// <summary>
        /// 登入，如果成功就把sessionId寫入資料庫，並且創建userInfo物件把userId和roles存到userInfo物件中，再存到Session["userInfo"]
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("LoginUser")]
        public int LoginUser([FromBody] JObject obj)
        {

            if (!LoginSpecialChar(obj["account"].ToString(), obj["pwd"].ToString()))
            {
                return (int)UserStatus.InputError;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getPwdAndEditSessionId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@account", obj["account"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@pwd", GetSHA256HashFromString(obj["pwd"].ToString())));
                        cmd.Parameters.Add(new SqlParameter("@sessionId", HttpContext.Current.Session.SessionID.ToString()));
                        cmd.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int));
                        cmd.Parameters["@userId"].Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(new SqlParameter("@roles", SqlDbType.Int));
                        cmd.Parameters["@roles"].Direction = ParameterDirection.Output;

                        object result = cmd.ExecuteScalar();

                        if (result != null && result.ToString() == "1")
                        {
                            UserInfo user = new UserInfo
                            {
                                UserId = (int)cmd.Parameters["@userId"].Value,
                                Roles = (int)cmd.Parameters["@roles"].Value
                            };
                            HttpContext.Current.Session["userInfo"] = user;

                            return (int)DatabaseOperationResult.Success;
                        }

                        return (int)DatabaseOperationResult.Failure;
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
        public bool LoginSpecialChar(string account, string pwd)
        {
            bool cheackAccount = Regex.IsMatch(account, @"^[A-Za-z0-9]{6,16}$");
            bool cheackPwd = Regex.IsMatch(pwd, @"^[A-Za-z0-9]{6,16}$");

            return (cheackAccount && cheackPwd);
        }

        /// <summary>
        /// 按下中英文按鈕時，Cookies["language"]紀錄該語言
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetLanguage")]
        public bool SetLanguage([FromBody] JObject obj)
        {

            if (HttpContext.Current.Request.Cookies["language"] != null)
            {
                HttpContext.Current.Response.Cookies["language"].Value = obj["language"].ToString();
                HttpContext.Current.Response.Cookies["language"].Expires = DateTime.Now.AddDays(30);
            }

            return true;
        }


        /// <summary>
        /// 取得管理員身分
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUserPermission")]
        public object GetUserPermission()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getAccountRoles", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userId", ((UserInfo)HttpContext.Current.Session["userInfo"]).UserId));
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
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex);
                return (int)DatabaseOperationResult.Error;
            }

        }

        /// <summary>
        /// 刪除Session["userId"]
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteSession")]
        public bool DeleteSession()
        {
            HttpContext.Current.Session["userInfo"] = null;
            return true;
        }


        /// <summary>
        /// 紀錄前端錯誤
        /// </summary>
        /// <param name="errorDetails"></param>
        [HttpPost]
        [Route("LogClientError")]
        public void LogClientError([FromBody] string[] errorDetails)
        {
            Logger logger = LogManager.GetCurrentClassLogger();

            try
            {
                foreach (var error in errorDetails)
                {
                    logger.Error(error);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "前端NLog錯誤");
            }
        }

        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        [NonAction]
        public string GetSHA256HashFromString(string strData)
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
    }
}