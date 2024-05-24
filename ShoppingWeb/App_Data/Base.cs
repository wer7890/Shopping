using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ShoppingWeb.Controller
{
    public class Base : ApiController
    {
        public readonly string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

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
    }
}