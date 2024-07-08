using NLog;
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ShoppingWeb.Repository
{
    public class BaseRepository : IBaseRepository
    {
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

    }
}