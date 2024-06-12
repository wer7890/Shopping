using NLog;
using ShoppingWeb.Filters;
using ShoppingWeb.Response;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;

namespace ShoppingWeb.Controller
{
    [RoutePrefix("/api/Controller/login")]
    [ValidationFilter]
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
        public BaseResponse LoginUser([FromBody] LoginUserDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getPwdAndEditSessionId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@account", dto.Account));
                        cmd.Parameters.Add(new SqlParameter("@pwd", GetSHA256HashFromString(dto.Pwd)));
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
                                Roles = (int)cmd.Parameters["@roles"].Value,
                                Account = dto.Account
                            };
                            HttpContext.Current.Session["userInfo"] = user;

                            return new BaseResponse
                            {
                                Status = DatabaseOperationResult.Success
                            };
                        }

                        return new BaseResponse
                        {
                            Status = DatabaseOperationResult.Failure
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex);
                return new BaseResponse
                {
                    Status = DatabaseOperationResult.Error
                };
            }
        }

        /// <summary>
        /// 按下中英文按鈕時，Cookies["language"]紀錄該語言
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetLanguage")]
        public void SetLanguage([FromBody] SetLanguageDto dto)
        {
            if (HttpContext.Current.Request.Cookies["language"] != null)
            {
                HttpContext.Current.Response.Cookies["language"].Value = dto.Language;
                HttpContext.Current.Response.Cookies["language"].Expires = DateTime.Now.AddDays(30);
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

        /// <summary>
        /// 取得管理員身分
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUserPermission")]
        public GetUserPermissionResponse GetUserPermission()
        {
            return new GetUserPermissionResponse
            {
                Account = ((UserInfo)HttpContext.Current.Session["userInfo"]).Account,
                Roles = ((UserInfo)HttpContext.Current.Session["userInfo"]).Roles,
                Status = DatabaseOperationResult.Success
            };
        }

        /// <summary>
        /// 刪除Session["userId"]
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteSession")]
        public void DeleteSession()
        {
            HttpContext.Current.Session["userInfo"] = null;
        }
    }
}