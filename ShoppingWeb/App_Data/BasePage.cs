using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Services;

namespace ShoppingWeb.Ajax
{
    public class BasePage : System.Web.UI.Page
    {
        public string cssVersion;
        public string jsVersion;
        public string cookieLanguage;

        public BasePage() 
        {
            this.Init += new EventHandler(BasePage_Init);  //EventHandler: 委派事件  Init:頁面初始化階段
            this.Load += new EventHandler(BasePage_Load);
        }

        /// <summary>
        /// 未登入時，不可進入其他頁面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BasePage_Init(object sender, EventArgs e)
        {
            if (Session["userInfo"] == null)
            {
                Response.Write("<script>window.parent.location.href = 'Login.aspx';</script>");
            }
        }

        /// <summary>
        /// 取得Version.json檔中所記錄js和css的版本號
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BasePage_Load(object sender, EventArgs e) 
        {
            if (!IsPostBack)
            {
                string jsonFilePath = Server.MapPath("~/Version.json");
                string jsonText = System.IO.File.ReadAllText(jsonFilePath);
                dynamic versionData = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonText);

                cssVersion = versionData["cssVersion"];
                jsVersion = versionData["jsVersion"];

                //判斷Cookies["language"]是否存在以及Value是不是符合enum
                if (HttpContext.Current.Request.Cookies["language"] == null || !Enum.TryParse(Request.Cookies["language"].Value.ToString(), out Language language))
                {
                    HttpCookie cookie = new HttpCookie("language")
                    {
                        Value = "TW",
                        Expires = DateTime.Now.AddDays(30)
                    };
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    cookieLanguage = "TW";
                    Response.Write("<script>parent.location.reload();</script>");
                }
                else
                {
                    cookieLanguage = Request.Cookies["language"].Value.ToString();
                }

                string selectedLanguage = Request.Cookies["Language"].Value;
                Thread.CurrentThread.CurrentCulture = new CultureInfo(selectedLanguage);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedLanguage);
            }
        }

        /// <summary>
        /// 判斷同一隻帳號是否有重複登入
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
                        cmd.Parameters.Add(new SqlParameter("@userId", ((UserInfo)HttpContext.Current.Session["userInfo"]).UserId));

                        object dbResult = cmd.ExecuteScalar();

                        if (dbResult != null)
                        {
                            string currentSessionID = HttpContext.Current.Session.SessionID;

                            if (dbResult.ToString() == currentSessionID)
                            {
                                result = true;
                            }
                            else
                            {
                                HttpContext.Current.Session["userInfo"] = null;
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
            return (((UserInfo)HttpContext.Current.Session["userInfo"]).Roles == 1 || ((UserInfo)HttpContext.Current.Session["userInfo"]).Roles == roles);
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

        /// <summary>
        /// 取的Session["language"]
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static string GetLanguage()
        {
            return HttpContext.Current.Session["language"].ToString();
        }

    }
}