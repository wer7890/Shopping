using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ShoppingWeb.Ajax
{
    public class Logger
    {
        private readonly string logFilePath;

        public Logger()
        {
            // 設定日誌文件路徑
            logFilePath = HttpContext.Current.Server.MapPath("~/Logs/log.txt");

            // 檢查文件是否存在，如果不存在則創建該文件
            if (!File.Exists(logFilePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
                File.Create(logFilePath).Close();
            }
        }

        public void LogException(Exception ex)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now}] Exception: {ex.Message}");  //紀錄時間跟錯誤訊息
                    writer.WriteLine($"StackTrace: {ex.StackTrace}");  //紀錄觸發異常的位置以及相關的調用堆疊信息
                    writer.WriteLine("--------------------------------------------------");
                }
            }
            catch (Exception)
            {

            }
        }
    }
}