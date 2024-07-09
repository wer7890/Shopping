using NLog;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ShoppingWeb.Repository
{
    public class BaseRepository : IBaseRepository
    {
        /// <summary>
        /// 資料庫連接字串
        /// </summary>
        public string connectionString {
            get { 
                return ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
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
        /// 記錄錯誤日誌
        /// </summary>
        /// <param name="ex"></param>
        public void SetNLog(Exception ex)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
          
            try
            {
                if (HttpContext.Current.Session["userInfo"] == null)
                {
                    logger.Error(ex + " 帳號: null");
                }
                else
                {
                    logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                }
            }
            catch (Exception)
            {
                logger.Error(ex, "後端紀錄NLog錯誤");
            }
            
        }

        /// <summary>
        /// 判斷是否重複登入
        /// </summary>
        /// <returns></returns>
        public (Exception, int?) RepeatLogin()
        {
            if (HttpContext.Current.Session["userInfo"] == null)
            {
                return (null, 0);
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getSessionId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userId", ((UserInfo)HttpContext.Current.Session["userInfo"]).UserId));
                        cmd.Parameters.Add(new SqlParameter("@sessionId", HttpContext.Current.Session.SessionID.ToString()));

                        return (null, (int)cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }
    }
}