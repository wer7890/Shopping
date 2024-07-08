using NLog;
using System;
using System.Web;

namespace ShoppingWeb.Repository
{
    public class BaseRepository : IBaseRepository
    {
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