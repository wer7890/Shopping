using NLog;
using ShoppingWeb.Filters;
using ShoppingWeb.Response;
using System;
using System.Data;
using System.Data.SqlClient;
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
        public GetAllOrderDataResponse GetAllOrderData([FromBody] GetAllOrderDataDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getAllOrderData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", dto.PageNumber));
                        cmd.Parameters.Add(new SqlParameter("@pageSize", dto.PageSize));
                        cmd.Parameters.Add(new SqlParameter("@beforePagesTotal", dto.BeforePagesTotal));
                        cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                        cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;

                        SqlDataAdapter da = new SqlDataAdapter(); //宣告一個配接器(DataTable與DataSet必須)
                        DataSet ds = new DataSet(); //宣告DataSet物件
                        da.SelectCommand = cmd; //執行
                        da.Fill(ds); //結果存放至DataTable

                        int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
                        int totalPages = (int)Math.Ceiling((double)totalCount / dto.PageSize);  // 計算總頁數，Math.Ceiling向上進位取整數

                        if (totalCount > 0)
                        {
                            GetAllOrderDataResponse result = GetAllOrderDataResponse.GetInstance(ds);
                            result.TotalPages = totalPages;
                            result.Status = ActionResult.Success;

                            return result;
                        }
                        else
                        {
                            return new GetAllOrderDataResponse
                            {
                                Status = ActionResult.Failure
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new GetAllOrderDataResponse
                {
                    Status = ActionResult.Error
                };
            }
        }

        /// <summary>
        /// 顯示訂單詳細資訊
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderDetailsData")]
        public GetOrderDetailsDataResponse GetOrderDetailsData([FromBody] GetOrderDetailsDataDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getOrderDetailsData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@orderId", dto.OrderId));
                        int languageNum = (HttpContext.Current.Request.Cookies["language"].Value == "TW") ? (int)Language.TW : (int)Language.EN;
                        cmd.Parameters.Add(new SqlParameter("@languageNum", languageNum));
                        SqlDataReader reader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        GetOrderDetailsDataResponse result = GetOrderDetailsDataResponse.GetInstance(dt);
                        result.Status = ActionResult.Success;

                        return result;

                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new GetOrderDetailsDataResponse
                {
                    Status = ActionResult.Error
                };
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
        public BaseResponse EditOrder([FromBody] EditOrderDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editOrderData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@orderId", dto.OrderId));
                        cmd.Parameters.Add(new SqlParameter("@orderStatus", dto.OrderStatusNum));
                        cmd.Parameters.Add(new SqlParameter("@deliveryStatus", dto.DeliveryStatusNum));
                        cmd.Parameters.Add(new SqlParameter("@deliveryMethod", dto.DeliveryMethodNum));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        if (rowsAffected > 0)
                        {
                            StockInsufficientCache.SetIsEditStock(true);
                            return new BaseResponse
                            {
                                Status = ActionResult.Success
                            };
                        }
                        else
                        {
                            return new BaseResponse
                            {
                                Status = ActionResult.Failure
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new BaseResponse
                {
                    Status = ActionResult.Error
                };
            }
        }

        /// <summary>
        /// 顯示相關訂單資訊
        /// </summary>
        /// <param name="deliveryStatusNum"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderData")]
        public GetOrderDataResponse GetOrderData([FromBody] GetOrderDataDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getOrderData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@deliveryStatusNum", dto.DeliveryStatusNum));
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", dto.PageNumber));
                        cmd.Parameters.Add(new SqlParameter("@pageSize", dto.PageSize));
                        cmd.Parameters.Add(new SqlParameter("@beforePagesTotal", dto.BeforePagesTotal));
                        cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                        cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;

                        SqlDataAdapter da = new SqlDataAdapter();
                        DataSet ds = new DataSet();
                        da.SelectCommand = cmd;
                        da.Fill(ds);

                        int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
                        int totalPages = (int)Math.Ceiling((double)totalCount / dto.PageSize);  // 計算總頁數，Math.Ceiling向上進位取整數
                     
                        if (totalCount > 0)
                        {
                            GetOrderDataResponse result = GetOrderDataResponse.GetInstance(ds);
                            result.TotalPages = totalPages;
                            result.Status = ActionResult.Success;

                            return result;
                        }
                        else
                        {
                            return new GetOrderDataResponse
                            {
                                Status = ActionResult.Failure
                            };
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new GetOrderDataResponse
                {
                    Status = ActionResult.Error
                };
            }
        }

        /// <summary>
        /// 顯示申請退貨訂單資訊
        /// </summary>
        /// <param name="orderStatusNum"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetReturnOrderData")]
        public GetReturnOrderDataResponse GetReturnOrderData([FromBody] GetReturnOrderDataDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getReturnOrderData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", dto.PageNumber));
                        cmd.Parameters.Add(new SqlParameter("@pageSize", dto.PageSize));
                        cmd.Parameters.Add(new SqlParameter("@beforePagesTotal", dto.BeforePagesTotal));
                        cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                        cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;

                        SqlDataAdapter da = new SqlDataAdapter();
                        DataSet ds = new DataSet();
                        da.SelectCommand = cmd;
                        da.Fill(ds);

                        int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
                        int totalPages = (int)Math.Ceiling((double)totalCount / dto.PageSize);  // 計算總頁數，Math.Ceiling向上進位取整數

                        if (totalCount > 0)
                        {
                            GetReturnOrderDataResponse result = GetReturnOrderDataResponse.GetInstance(ds);
                            result.TotalPages = totalPages;
                            result.Status = ActionResult.Success;

                            return result;
                        }
                        else
                        {
                            return new GetReturnOrderDataResponse
                            {
                                Status = ActionResult.Failure
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new GetReturnOrderDataResponse
                {
                    Status = ActionResult.Error
                };
            }
        }

        /// <summary>
        /// 更改退貨訂單資訊
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditReturnOrder")]
        public BaseResponse EditReturnOrder([FromBody] EditReturnOrderDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editReturnOrder", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@orderId", dto.OrderId));
                        cmd.Parameters.Add(new SqlParameter("@boolReturn", dto.BoolReturn));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return new BaseResponse
                        {
                            Status = (rowsAffected > 0) ? ActionResult.Success : ActionResult.Failure
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new BaseResponse
                {
                    Status = ActionResult.Error
                };
            }
        }
    }
}