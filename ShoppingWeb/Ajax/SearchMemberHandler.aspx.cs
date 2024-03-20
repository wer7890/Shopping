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
    public partial class SearchMemberHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 一開始顯示所有會員資訊
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static object GetAllMemberData()
        {
            bool loginResult = IndexHandler.AnyoneLongin();
            if (!loginResult)
            {
                return "重複登入";
            }
            else
            {
                // 連接資料庫，獲取使用者資料
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("getAllMemberData", con))
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
        /// 是否停權
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string ToggleProductStatus(string memberId)
        {
            bool loginResult = IndexHandler.AnyoneLongin();
            if (!loginResult)
            {
                return "重複登入";
            }
            else
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("toggleMemberStatus", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.Add(new SqlParameter("@memberId", memberId));

                            int rowsAffected = (int)cmd.ExecuteScalar();

                            return (rowsAffected > 0) ? "更改成功" : "更改失敗";

                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                    return "錯誤";
                }
            }
        }
    }
}