using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.Services;

namespace ShoppingWeb.Ajax
{
    public partial class OrderHandler : BasePage
    {

        public enum Status 
        {
            /// <summary>
            /// 重複登入
            /// </summary>
            DuplicateLogin = 1,

            /// <summary>
            /// 權限不足
            /// </summary>
            Roles = 2,

            /// <summary>
            /// 後端輸入值錯誤
            /// </summary>
            SpecialChar = 3
        }

        private const int PERMITTED_Order_ROLES = 2;

        /// <summary>
        /// 一開始顯示所有訂單資訊
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static object GetAllOrderData()
        {
            if (!CheckDuplicateLogin())
            {
                return (int)Status.DuplicateLogin;
            }

            if (!CheckRoles(PERMITTED_Order_ROLES))
            {
                return (int)Status.Roles;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pro_sw_getAllOrderData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(); //宣告一個配接器(DataTable與DataSet必須)
                    DataSet ds = new DataSet(); //宣告DataSet物件
                    da.SelectCommand = cmd; //執行
                    da.Fill(ds); //結果存放至DataTable

                    object[] resultArr = new object[2];

                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        resultArr[i] = ConvertDataTableToJson(ds.Tables[i]);
                    }

                    return resultArr;
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

            if (!CheckRoles(PERMITTED_Order_ROLES))
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
        /// <param name="deliveryStatusNum"></param>
        /// <param name="deliveryMethodNum"></param>
        /// <returns></returns>
        [WebMethod]
        public static string EditOrder(int orderId, int orderStatusNum, int deliveryStatusNum, int deliveryMethodNum)
        {

            if (!CheckDuplicateLogin())
            {
                return "重複登入";
            }

            if (!CheckRoles(PERMITTED_Order_ROLES))
            {
                return "權限不足";
            }

            if (!EditOrderSpecialChar(orderId, orderStatusNum, deliveryStatusNum, deliveryMethodNum))
            {
                return "後端輸入值錯誤";
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
                        cmd.Parameters.Add(new SqlParameter("@deliveryStatus", deliveryStatusNum));
                        cmd.Parameters.Add(new SqlParameter("@deliveryMethod", deliveryMethodNum));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return (rowsAffected > 0) ? "更改成功" : "庫存不足或更新時發生錯誤";

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
        /// 判斷輸入值
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderStatusNum"></param>
        /// <param name="deliveryStatusNum"></param>
        /// <param name="deliveryMethodNum"></param>
        /// <returns></returns>
        public static bool EditOrderSpecialChar(int orderId, int orderStatusNum, int deliveryStatusNum, int deliveryMethodNum)
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
        [WebMethod]
        public static object GetOrderData(int deliveryStatusNum)
        {
            if (!CheckDuplicateLogin())
            {
                return "重複登入";
            }

            if (!CheckRoles(PERMITTED_Order_ROLES))
            {
                return "權限不足";
            }

            string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pro_sw_getOrderData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.Add(new SqlParameter("@deliveryStatusNum", deliveryStatusNum));

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da.SelectCommand = cmd;
                    da.Fill(ds);

                    object[] resultArr = new object[2];

                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        resultArr[i] = ConvertDataTableToJson(ds.Tables[i]);
                    }

                    return resultArr;
                }
            }

        }

        /// <summary>
        /// 顯示申請退貨訂單資訊
        /// </summary>
        /// <param name="orderStatusNum"></param>
        /// <returns></returns>
        [WebMethod]
        public static object GetReturnOrderData()
        {
            if (!CheckDuplicateLogin())
            {
                return "重複登入";
            }

            if (!CheckRoles(PERMITTED_Order_ROLES))
            {
                return "權限不足";
            }

            string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pro_sw_getReturnOrderData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da.SelectCommand = cmd;
                    da.Fill(ds);

                    object[] resultArr = new object[2];

                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        resultArr[i] = ConvertDataTableToJson(ds.Tables[i]);
                    }

                    return resultArr;
                }
            }

        }

        /// <summary>
        /// 更改退貨訂單資訊
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [WebMethod]
        public static object EditReturnOrder(int orderId, bool boolReturn)
        {
            if (!CheckDuplicateLogin())
            {
                return "重複登入";
            }

            if (!CheckRoles(PERMITTED_Order_ROLES))
            {
                return "權限不足";
            }

            if (!EditReturnOrderSpecialChar(orderId))
            {
                return "後端輸入值錯誤";
            }

            string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pro_sw_editReturnOrder", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.Add(new SqlParameter("@orderId", orderId));
                    cmd.Parameters.Add(new SqlParameter("@boolReturn", boolReturn));

                    int rowsAffected = (int)cmd.ExecuteScalar();

                    return (rowsAffected > 0) ? "更改成功" : "更改失敗";
                }
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
        public static bool EditReturnOrderSpecialChar(int orderId)
        {
            bool cheackOrderId = Regex.IsMatch(orderId.ToString(), @"^[0-9]{1,10}$");

            return cheackOrderId;
        }
    }
}