using NLog;
using ShoppingWeb.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace ShoppingWeb.Controller
{
    [LoginFilter]
    [ExceptionFilter]
    public class BaseController : ApiController
    {
        public readonly string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

        public BaseController()
        {
        }

        /// <summary>
        /// 將 DataTable 轉換為 JSON 字串的輔助方法
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [NonAction]
        public string ConvertDataTableToJson(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            System.Collections.ArrayList rows = new System.Collections.ArrayList();
            System.Collections.IDictionary row;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return serializer.Serialize(rows);
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
        /// 紀錄前端錯誤
        /// </summary>
        /// <param name="errorDetails"></param>
        [AllowAnonymous]
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
    }
}