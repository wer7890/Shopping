using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Services;


namespace ShoppingWeb.Ajax
{
    public partial class AddUserHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 判斷使用者名稱是否存在，如果沒有就新增管理員
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [WebMethod]
        public static string RegisterNewUser(string account, string pwd, string roles)
        {
            bool loginResult = IndexHandler.AnyoneLongin();
            if (!loginResult)
            {
                return "重複登入";
            }
            else
            {
                if (SpecialChar(account, pwd, roles))
                {
                    try
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("registerNewUser", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                con.Open();
                                cmd.Parameters.Add(new SqlParameter("@account", account));
                                cmd.Parameters.Add(new SqlParameter("@pwd", GetSHA256HashFromString(pwd)));
                                cmd.Parameters.Add(new SqlParameter("@roles", roles));

                                string result = cmd.ExecuteScalar().ToString();

                                return result;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                        return "發生內部錯誤: " + ex.Message;
                    }
                }
                else
                {
                    return "帳號和密碼不能含有非英文和數字且長度應在6到16之間且腳色不能為空";
                }
            }
        }

        /// <summary>
        /// 判斷輸入值
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static bool SpecialChar(string account, string pwd, string roles)
        {
            bool cheackAccount = Regex.IsMatch(account, @"^[A-Za-z0-9]{6,16}$");
            bool cheackPwd = Regex.IsMatch(pwd, @"^[A-Za-z0-9]{6,16}$");
            bool cheackRoles = Regex.IsMatch(roles, @"^[0-9]{1,2}$");

            if (cheackAccount && cheackPwd && cheackRoles)
            {
                return true;
            }
            else
            {
                return false;
            }
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