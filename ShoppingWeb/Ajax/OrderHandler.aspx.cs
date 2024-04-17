﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

namespace ShoppingWeb.Ajax
{
    public partial class OrderHandler : BasePage
    {
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
                return "重複登入";
            }

            if (!CheckRoles(PERMITTED_Order_ROLES))
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

            if (!CheckRoles(PERMITTED_Order_ROLES))
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
        /// 顯示有關訂單資訊
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
    }
}