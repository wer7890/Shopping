using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;


namespace ShoppingWeb.Ajax
{
    public partial class SearchUserHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 刪除管理員
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string RemoveUserInfo(string userId)
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
                        using (SqlCommand cmd = new SqlCommand("deleteUser", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.Add(new SqlParameter("@userId", userId));

                            int r = (int)cmd.ExecuteScalar();
                            if (r > 0)
                            {
                                return "刪除成功";
                            }
                            else
                            {
                                return "刪除失敗";
                            }
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

        /// <summary>
        /// 設定Session["userId"]
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool SetSessionUserId(string userId)
        {
            HttpContext.Current.Session["userId"] = userId;  //存儲資料到 Session 變數
            return true;
        }

        /// <summary>
        /// 表格
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static object GetUserData()
        {
            bool loginResult = IndexHandler.AnyoneLongin();
            if (!loginResult)
            {
                return "重複登入";
            }
            else
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("getAllUserData", con))
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
                row = new System.Collections.Generic.Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return serializer.Serialize(rows);
        }

        /// <summary>
        /// 更改管理員身分
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [WebMethod]
        public static string ToggleUserRoles(string userId, string roles)
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
                        using (SqlCommand cmd = new SqlCommand("toggleUserRoles", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.Add(new SqlParameter("@userId", userId));
                            cmd.Parameters.Add(new SqlParameter("@roles", roles));

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