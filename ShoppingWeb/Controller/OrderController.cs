using Newtonsoft.Json.Linq;
using NLog;
using ShoppingWeb.Filters;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace ShoppingWeb.Controller
{
    [RoutePrefix("/api/Controller/order")]
    [RolesFilter((int)Roles.Member)]
    public class OrderController : BaseController
    {
        /// <summary>
        /// 一開始顯示所有訂單資訊
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAllOrderData")]
        public object GetAllOrderData([FromBody] GetAllOrderDataAttribute order)
        {

            if (!ModelState.IsValid)
            {
                return (int)UserStatus.InputError;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getAllOrderData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", order.PageNumber));
                        cmd.Parameters.Add(new SqlParameter("@pageSize", order.PageSize));
                        cmd.Parameters.Add(new SqlParameter("@beforePagesTotal", order.BeforePagesTotal));
                        cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                        cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;

                        SqlDataAdapter da = new SqlDataAdapter(); //宣告一個配接器(DataTable與DataSet必須)
                        DataSet ds = new DataSet(); //宣告DataSet物件
                        da.SelectCommand = cmd; //執行
                        da.Fill(ds); //結果存放至DataTable

                        int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
                        int totalPages = (int)Math.Ceiling((double)totalCount / order.PageSize);  // 計算總頁數，Math.Ceiling向上進位取整數

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
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
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
        public object GetOrderDetailsData([FromBody] GetOrderDetailsDataAttribute order)
        {

            if (!ModelState.IsValid)
            {
                return (int)UserStatus.InputError;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getOrderDetailsData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@orderId", order.OrderId));
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
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
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
        public int EditOrder([FromBody] EditOrderAttribute order)
        {

            if (!ModelState.IsValid)
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
                        cmd.Parameters.Add(new SqlParameter("@orderId", order.OrderId));
                        cmd.Parameters.Add(new SqlParameter("@orderStatus", order.OrderStatusNum));
                        cmd.Parameters.Add(new SqlParameter("@deliveryStatus", order.DeliveryStatusNum));
                        cmd.Parameters.Add(new SqlParameter("@deliveryMethod", order.DeliveryMethodNum));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        if (rowsAffected > 0)
                        {
                            IsEditStock = true;
                            return (int)DatabaseOperationResult.Success;
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
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 顯示相關訂單資訊
        /// </summary>
        /// <param name="deliveryStatusNum"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderData")]
        public object GetOrderData([FromBody] GetOrderDataAttribute order)
        {

            if (!ModelState.IsValid)
            {
                return (int)UserStatus.InputError;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getOrderData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@deliveryStatusNum", order.DeliveryStatusNum));
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", order.PageNumber));
                        cmd.Parameters.Add(new SqlParameter("@pageSize", order.PageSize));
                        cmd.Parameters.Add(new SqlParameter("@beforePagesTotal", order.BeforePagesTotal));
                        cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                        cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;

                        SqlDataAdapter da = new SqlDataAdapter();
                        DataSet ds = new DataSet();
                        da.SelectCommand = cmd;
                        da.Fill(ds);

                        int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
                        int totalPages = (int)Math.Ceiling((double)totalCount / order.PageSize);  // 計算總頁數，Math.Ceiling向上進位取整數

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
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
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
        public object GetReturnOrderData([FromBody] GetAllOrderDataAttribute order)
        {

            if (!ModelState.IsValid)
            {
                return (int)UserStatus.InputError;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getReturnOrderData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", order.PageNumber));
                        cmd.Parameters.Add(new SqlParameter("@pageSize", order.PageSize));
                        cmd.Parameters.Add(new SqlParameter("@beforePagesTotal", order.BeforePagesTotal));
                        cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                        cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;

                        SqlDataAdapter da = new SqlDataAdapter();
                        DataSet ds = new DataSet();
                        da.SelectCommand = cmd;
                        da.Fill(ds);

                        int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
                        int totalPages = (int)Math.Ceiling((double)totalCount / order.PageSize);  // 計算總頁數，Math.Ceiling向上進位取整數

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
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
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
        public int EditReturnOrder([FromBody] EditReturnOrderAttribute order)
        {
            if (!ModelState.IsValid)
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
                        cmd.Parameters.Add(new SqlParameter("@orderId", order.OrderId));
                        cmd.Parameters.Add(new SqlParameter("@boolReturn", order.BoolReturn));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return (rowsAffected > 0) ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return (int)DatabaseOperationResult.Error;
            }
        }
    }
}