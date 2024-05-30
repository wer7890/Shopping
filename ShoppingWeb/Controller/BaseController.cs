using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Timers;
using System.Web;
using System.Web.Http;

namespace ShoppingWeb.Controller
{
    [CustomAuthorize]
    public class BaseController : ApiController
    {
        public readonly string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

        public static readonly HashSet<string> errorsSet = new HashSet<string>();  //錯誤資料不重複的陣列
        public static bool openTimer = false;

        public BaseController()
        {
            if (!openTimer)
            {
                Timer t = new Timer(3600000)  //創建Timer，時間間隔1小時3600000毫秒
                {
                    AutoReset = true  //一直執行(true)
                };
                t.Elapsed += new ElapsedEventHandler(RemoveErrorsSet);  //到達時間的时候執行事件；
                t.Start();  //啟動計時器
                openTimer = true;
            }
        }

        /// <summary>
        /// 判斷權限是否可使用該功能
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        [NonAction]
        public bool CheckRoles(int roles)
        {
            return (((UserInfo)HttpContext.Current.Session["userInfo"]).Roles == 1 || ((UserInfo)HttpContext.Current.Session["userInfo"]).Roles == roles);
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
        /// 刪除錯誤日誌資料的Set
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [NonAction]
        public void RemoveErrorsSet(object sender, ElapsedEventArgs e)
        {
            errorsSet.Clear();
        }
    }
}