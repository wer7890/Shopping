using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ShoppingWeb.Repository
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        /// <summary>
        /// 更改訂單
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, int?) EditOrder(EditOrderDto dto)
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

                        return (null, (int)cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }

        /// <summary>
        /// 更改退貨訂單資訊
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, int?) EditReturnOrder(EditReturnOrderDto dto)
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

                        return (null, (int)cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }

        /// <summary>
        /// 顯示訂單詳細資訊
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, DataTable) GetOrderDetailsData(GetOrderDetailsDataDto dto)
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

                        return (null, dt);
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }

        /// <summary>
        /// 顯示所有訂單資訊
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, int?, DataSet) GetAllOrderData(GetAllOrderDataDto dto)
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

                        return (null, totalCount, ds);
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex, null, null);
            }
        }

        /// <summary>
        /// 顯示申請退貨訂單資訊
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, int?, DataSet) GetReturnOrderData(GetReturnOrderDataDto dto)
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

                        return (null, totalCount, ds);
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex, null, null);
            }
        }

        /// <summary>
        /// 顯示相關訂單資訊
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, int?, DataSet) GetOrderData(GetOrderDataDto dto)
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

                        return (null, totalCount, ds);
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex, null, null);
            }
        }
    }
}