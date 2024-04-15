using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

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
    }
}