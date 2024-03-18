﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingWeb.Ajax
{
    public partial class SearchProductHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 一開始顯示所有商品
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static object GetAllProductData()
        {
            // 連接資料庫，獲取使用者資料
            string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("getAllProductData", con))
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
        /// 尋找特定商品
        /// </summary>
        /// <param name="sqlName"></param>
        /// <param name="sqlAdd"></param>
        /// <param name="searchName"></param>
        /// <returns></returns>
        [WebMethod]
        public static object GetProductData(string productCategory, string productName )
        {
            // 連接資料庫，獲取使用者資料
            string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("getSearchProductData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.Add(new SqlParameter("@productCategory", productCategory));
                    cmd.Parameters.Add(new SqlParameter("@productName", productName));
                    object result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        return "null";
                    }
                    else
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        // 將資料轉換為 JSON 格式返回
                        return ConvertDataTableToJson(dt);
                    }

                }
            }
        }

        /// <summary>
        /// 將 DataTable 轉換為 JSON 字串的輔助方法
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static string ConvertDataTableToJson(DataTable dt)
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
        /// 刪除商品
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool RemoveProduct(string productId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("deleteProduct", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@productId", productId));

                        //設定預存程序輸出參數的名稱與資料類型
                        cmd.Parameters.Add(new SqlParameter("@deletedProductImg", SqlDbType.NVarChar, 50));
                        //設定參數名稱的傳遞方向
                        cmd.Parameters["@deletedProductImg"].Direction = ParameterDirection.Output;

                        int r = (int)cmd.ExecuteScalar();
                        //取得預存程序的輸出參數值
                        string deletedProductImg = cmd.Parameters["@deletedProductImg"].Value.ToString();

                        if (r > 0)
                        {
                            string imagePath = HttpContext.Current.Server.MapPath("~/ProductImg/" + deletedProductImg);
                            File.Delete(imagePath);
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 是否開放開關
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool ToggleProductStatus(string productId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("toggleProductStatus", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@productId", productId));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return (rowsAffected > 0) ? true : false;

                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                return false;
            }
        }


    }
}