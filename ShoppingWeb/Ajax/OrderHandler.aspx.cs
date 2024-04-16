using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

namespace ShoppingWeb.Ajax
{
    public partial class OrderHandler : BasePage
    {
        private const int PERMITTED_USER_ROLES = 3;

        /// <summary>
        /// 一開始顯示所有訂單資訊
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static object GetAllOrderData()
        {
            if (!CheckDuplicateLogin())
            {
                return "重複登入";
            }

            if (!CheckRoles(PERMITTED_USER_ROLES))
            {
                return "權限不足";
            }

            string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pro_sw_getAllOrderData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // 將資料轉換為 JSON 格式返回
                    return ConvertDataTableToJson(dt);
                }
            }

        }

        /// <summary>
        /// 顯示訂單詳細資訊
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [WebMethod]
        public static object GetOrderDetailsData(int orderId)
        {
            if (!CheckDuplicateLogin())
            {
                return "重複登入";
            }

            if (!CheckRoles(PERMITTED_USER_ROLES))
            {
                return "權限不足";
            }

            string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pro_sw_getOrderDetailsData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.Add(new SqlParameter("@orderId", orderId));
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    // 將資料轉換為 JSON 格式返回
                    return ConvertDataTableToJson(dt);
                }
            }

        }

        /// <summary>
        /// 更改訂單資料
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderStatusNum"></param>
        /// <param name="paymentStatusNum"></param>
        /// <param name="deliveryStatusNum"></param>
        /// <param name="deliveryMethodNum"></param>
        /// <returns></returns>
        [WebMethod]
        public static string EditOrder(int orderId, int orderStatusNum, int paymentStatusNum, int deliveryStatusNum, int deliveryMethodNum)
        {

            if (!CheckDuplicateLogin())
            {
                return "重複登入";
            }

            if (!CheckRoles(PERMITTED_USER_ROLES))
            {
                return "權限不足";
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editOrderData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@orderId", orderId));
                        cmd.Parameters.Add(new SqlParameter("@orderStatus", orderStatusNum));
                        cmd.Parameters.Add(new SqlParameter("@paymentStatus", paymentStatusNum));
                        cmd.Parameters.Add(new SqlParameter("@deliveryStatus", deliveryStatusNum));
                        cmd.Parameters.Add(new SqlParameter("@deliveryMethod", deliveryMethodNum));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return (rowsAffected > 0) ? "更改成功" : "更改失敗";

                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = new Logger();
                logger.LogException(ex);
                return "錯誤";
            }
        }

        /// <summary>
        /// 一開始顯示所有訂單資訊
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static object GetDeliveryStatusCount()
        {
            if (!CheckDuplicateLogin())
            {
                return "重複登入";
            }

            if (!CheckRoles(PERMITTED_USER_ROLES))
            {
                return "權限不足";
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getDeliveryStatusCount", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataAdapter sqlData = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sqlData.Fill(dt);

                            // 構建包含數據的匿名對象
                            var orderObject = new
                            {
                                StatusAll = dt.Rows[0]["statusAll"],
                                Status1 = dt.Rows[0]["status1"],
                                Status2 = dt.Rows[0]["status2"],
                                Status3 = dt.Rows[0]["status3"],
                                Status4 = dt.Rows[0]["status4"],
                                Status5 = dt.Rows[0]["status5"],
                                Status6 = dt.Rows[0]["status6"],
                                Status7 = dt.Rows[0]["status7"],
                            };

                            return orderObject;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = new Logger();
                logger.LogException(ex);
                return ex;
            }

        }
    }
}