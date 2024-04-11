using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;

namespace ShoppingWeb.Ajax
{
    public class BasePage : System.Web.UI.Page
    {
        private static UserInfo userInfo;

        /// <summary>
        /// 取得使用者資訊物件
        /// </summary>
        public static UserInfo UserInfo
        { 
            set { userInfo = value; } 
            get { return userInfo; } 
        }
        

        public BasePage() 
        {
            //判斷是否有登入
            this.Load += new EventHandler(BasePage_Load);

        }

        void BasePage_Load(object sender, EventArgs e)
        {
            if (Session["userId"] == null)
            {
                Response.Write("<script>window.parent.location.href = 'Login.aspx';</script>");
            }
        }

        

        /// <summary>
        /// 確認是否有重複登入
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static bool CheckDuplicateLogin()
        {
            bool result = false;
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getSessionId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userId", userInfo.UID));

                        object dbResult = cmd.ExecuteScalar();

                        if (dbResult != null)
                        {
                            HttpContext.Current.Session["dbID"] = dbResult.ToString();

                            string currentSessionID = HttpContext.Current.Session.SessionID;
                            string dbSessionID = HttpContext.Current.Session["dbID"].ToString();

                            if (dbSessionID == currentSessionID)
                            {
                                result = true;
                            }
                            else
                            {
                                HttpContext.Current.Session["userId"] = null;
                            }
                        }

                        return result;

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
        /// 將 DataTable 轉換為 JSON 字串的輔助方法
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ConvertDataTableToJson(DataTable dt)
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
        /// 判斷權限是否可使用該功能
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static bool CheckRoles(int roles)
        {
            return (userInfo.Roles == 1 || userInfo.Roles == roles);
        }

        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static string GetSHA256HashFromString(string strData)
        {
            byte[] bytValue = Encoding.UTF8.GetBytes(strData);
            byte[] retVal = SHA256.Create().ComputeHash(bytValue);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }

    }
}