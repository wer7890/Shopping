using System;

namespace ShoppingWeb.Repository
{
    public interface IBaseRepository
    {
        /// <summary>
        /// 資料庫連接字串
        /// </summary>
        string connectionString { get; }
        
        /// <summary>
        /// 記錄錯誤日誌
        /// </summary>
        /// <param name="ex"></param>
        void SetNLog(Exception ex);

        string GetSHA256HashFromString(string strData);
    }
}