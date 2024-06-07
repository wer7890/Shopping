using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Timers;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.SessionState;

namespace ShoppingWeb
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)  //第一個請求到達網站的時候被執行(網站的初始化)，Application_End()網站停止服務的時候被執行(網站的掃尾處理)
        {
            //註冊WebAPI路由
            RouteTable.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{folder}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },  //預設值
                constraints: new { folder = "Controller" }   //條件約束
            );

            GetLowStockData(null, null);
            Timer t = new Timer(30000)  //創建Timer，時間間隔30秒
            {
                AutoReset = true  //一直執行(true)
            };
            t.Elapsed += new ElapsedEventHandler(GetLowStockData);  //到達時間的时候執行事件
            t.Start();  //啟動計時器
        }

        /// <summary>
        /// 得到商品低於警戒值的資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GetLowStockData(object sender, ElapsedEventArgs e)
        {
            if (StockInsufficientCache.IsEditStock)
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("pro_sw_getLowStock", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();

                            SqlDataReader reader = cmd.ExecuteReader();
                            DataTable dt = new DataTable();
                            dt.Load(reader);

                            StockInsufficientCache.SetStockInsufficient(ConvertDataTableToJson(dt));
                            StockInsufficientCache.SetIsEditStock(false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger logger = LogManager.GetCurrentClassLogger();
                    logger.Error("後端" + ex.Message);
                }
            }            
        }

        /// <summary>
        /// 將 DataTable 轉換為 JSON 字串的輔助方法
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
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
        /// 檢查當前請求的URL是否以~/api開頭
        /// </summary>
        /// <returns></returns>
        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/api");
        }

        void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }
    }
}