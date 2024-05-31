using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace ShoppingWeb.Controller
{
    [RoutePrefix("/api/Controller/order")]
    public class OrderController : BaseController
    {
        /// <summary>
        /// 一開始顯示所有訂單資訊
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAllOrderData")]
        public object GetAllOrderData([FromBody] JObject obj)
        {

            if (!CheckRoles((int)Roles.Member))
            {
                return (int)UserStatus.AccessDenied;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getAllOrderData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", obj["pageNumber"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@pageSize", obj["pageSize"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@beforePagesTotal", obj["beforePagesTotal"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                        cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;

                        SqlDataAdapter da = new SqlDataAdapter(); //宣告一個配接器(DataTable與DataSet必須)
                        DataSet ds = new DataSet(); //宣告DataSet物件
                        da.SelectCommand = cmd; //執行
                        da.Fill(ds); //結果存放至DataTable

                        int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
                        int totalPages = (int)Math.Ceiling((double)totalCount / (int)obj["pageSize"]);  // 計算總頁數，Math.Ceiling向上進位取整數

                        object[] resultArr = new object[2];

                        for (int i = 0; i < ds.Tables.Count; i++)
                        {
                            resultArr[i] = ConvertDataTableToJson(ds.Tables[i]);
                        }

                        var result = new
                        {
                            Data = resultArr,
                            TotalPages = totalPages
                        };

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error("後端" + ex.Message + "帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 顯示訂單詳細資訊
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderDetailsData")]
        public object GetOrderDetailsData([FromBody] JObject obj)
        {

            if (!CheckRoles((int)Roles.Member))
            {
                return (int)UserStatus.AccessDenied;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getOrderDetailsData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@orderId", obj["orderId"].ToString()));
                        int languageNum = (HttpContext.Current.Request.Cookies["language"].Value == "TW") ? (int)Language.TW : (int)Language.EN;
                        cmd.Parameters.Add(new SqlParameter("@languageNum", languageNum));
                        SqlDataReader reader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        // 將資料轉換為 JSON 格式返回
                        return ConvertDataTableToJson(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error("後端" + ex.Message + "帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 更改訂單資料
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderStatusNum"></param>
        /// <param name="deliveryStatusNum"></param>
        /// <param name="deliveryMethodNum"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditOrder")]
        public int EditOrder([FromBody] JObject obj)
        {

            if (!CheckRoles((int)Roles.Member))
            {
                return (int)UserStatus.AccessDenied;
            }

            if (!EditOrderSpecialChar((int)obj["orderId"], (int)obj["orderStatusNum"], (int)obj["deliveryStatusNum"], (int)obj["deliveryMethodNum"]))
            {
                return (int)UserStatus.InputError;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editOrderData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@orderId", obj["orderId"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@orderStatus", obj["orderStatusNum"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@deliveryStatus", obj["deliveryStatusNum"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@deliveryMethod", obj["deliveryMethodNum"].ToString()));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return (rowsAffected > 0) ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure;

                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error("後端" + ex.Message + "帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 判斷輸入值
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderStatusNum"></param>
        /// <param name="deliveryStatusNum"></param>
        /// <param name="deliveryMethodNum"></param>
        /// <returns></returns>
        [NonAction]
        public bool EditOrderSpecialChar(int orderId, int orderStatusNum, int deliveryStatusNum, int deliveryMethodNum)
        {
            bool cheackOrderId = Regex.IsMatch(orderId.ToString(), @"^[0-9]{1,10}$");
            bool cheackOrderStatusNum = Regex.IsMatch(orderStatusNum.ToString(), @"^[1-4]{1}$");
            bool cheackDeliveryStatusNum = Regex.IsMatch(deliveryStatusNum.ToString(), @"^[1-6]{1}$");
            bool cheackDeliveryMethodNum = Regex.IsMatch(deliveryMethodNum.ToString(), @"^[1-3]{1}$");

            return cheackOrderId && cheackOrderStatusNum && cheackDeliveryStatusNum && cheackDeliveryMethodNum;
        }

        /// <summary>
        /// 顯示相關訂單資訊
        /// </summary>
        /// <param name="deliveryStatusNum"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderData")]
        public object GetOrderData([FromBody] JObject obj)
        {

            if (!CheckRoles((int)Roles.Member))
            {
                return (int)UserStatus.AccessDenied;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getOrderData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@deliveryStatusNum", obj["deliveryStatusNum"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", obj["pageNumber"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@pageSize", obj["pageSize"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@beforePagesTotal", obj["beforePagesTotal"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                        cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;

                        SqlDataAdapter da = new SqlDataAdapter();
                        DataSet ds = new DataSet();
                        da.SelectCommand = cmd;
                        da.Fill(ds);

                        int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
                        int totalPages = (int)Math.Ceiling((double)totalCount / (int)obj["pageSize"]);  // 計算總頁數，Math.Ceiling向上進位取整數

                        object[] resultArr = new object[2];

                        for (int i = 0; i < ds.Tables.Count; i++)
                        {
                            resultArr[i] = ConvertDataTableToJson(ds.Tables[i]);
                        }

                        if (totalCount > 0)
                        {
                            var result = new
                            {
                                Data = resultArr,
                                TotalPages = totalPages
                            };
                            return result;
                        }
                        else
                        {
                            return (int)DatabaseOperationResult.Failure;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error("後端" + ex.Message + "帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 顯示申請退貨訂單資訊
        /// </summary>
        /// <param name="orderStatusNum"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetReturnOrderData")]
        public object GetReturnOrderData([FromBody] JObject obj)
        {

            if (!CheckRoles((int)Roles.Member))
            {
                return (int)UserStatus.AccessDenied;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getReturnOrderData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", obj["pageNumber"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@pageSize", obj["pageSize"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@beforePagesTotal", obj["beforePagesTotal"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                        cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;

                        SqlDataAdapter da = new SqlDataAdapter();
                        DataSet ds = new DataSet();
                        da.SelectCommand = cmd;
                        da.Fill(ds);

                        int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
                        int totalPages = (int)Math.Ceiling((double)totalCount / (int)obj["pageSize"]);  // 計算總頁數，Math.Ceiling向上進位取整數

                        object[] resultArr = new object[2];

                        for (int i = 0; i < ds.Tables.Count; i++)
                        {
                            resultArr[i] = ConvertDataTableToJson(ds.Tables[i]);
                        }

                        if (totalCount > 0)
                        {
                            var result = new
                            {
                                Data = resultArr,
                                TotalPages = totalPages
                            };
                            return result;
                        }
                        else
                        {
                            return (int)DatabaseOperationResult.Failure;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error("後端" + ex.Message + "帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 更改退貨訂單資訊
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditReturnOrder")]
        public int EditReturnOrder([FromBody] JObject obj)
        {

            if (!CheckRoles((int)Roles.Member))
            {
                return (int)UserStatus.AccessDenied;
            }

            if (!EditReturnOrderSpecialChar((int)obj["orderId"]))
            {
                return (int)UserStatus.InputError;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editReturnOrder", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@orderId", obj["orderId"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@boolReturn", obj["boolReturn"].ToString()));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return (rowsAffected > 0) ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error("後端" + ex.Message + "帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return (int)DatabaseOperationResult.Error;
            }

        }

        /// <summary>
        /// 判斷退貨訂單輸入值
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderStatusNum"></param>
        /// <param name="deliveryStatusNum"></param>
        /// <param name="deliveryMethodNum"></param>
        /// <returns></returns>
        [NonAction]
        public bool EditReturnOrderSpecialChar(int orderId)
        {
            bool cheackOrderId = Regex.IsMatch(orderId.ToString(), @"^[0-9]{1,10}$");

            return cheackOrderId;
        }
    }
}