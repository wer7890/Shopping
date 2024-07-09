using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
    }
}