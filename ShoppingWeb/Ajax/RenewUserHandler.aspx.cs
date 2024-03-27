﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;

namespace ShoppingWeb.Ajax
{
    public partial class RenewUserHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 設定跳轉道編輯帳號頁面時，input裡面的預設值
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static object GetUserDataForEdit()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                string sessionUserId = HttpContext.Current.Session["selectUserId"] as string;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("getUserData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userId", sessionUserId));

                        using (SqlDataAdapter sqlData = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sqlData.Fill(dt);

                            // 構建包含數據的匿名對象
                            var userObject = new
                            {
                                UserId = dt.Rows[0]["f_id"],
                                Account = dt.Rows[0]["f_account"],
                                Roles = dt.Rows[0]["f_roles"],
                            };

                            return userObject;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                return ex;
            }

        }

        /// <summary>
        /// 更改密碼
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [WebMethod]
        public static string EditUser(string pwd)
        {
            bool loginResult = IndexHandler.AnyoneLongin();
            if (!loginResult)
            {
                return "重複登入";
            }
            else
            {
                if (SpecialChar(pwd))
                {
                    try
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                        string sessionUserId = HttpContext.Current.Session["selectUserId"] as string;
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand("editUser", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                con.Open();

                                cmd.Parameters.Add(new SqlParameter("@userId", sessionUserId));
                                cmd.Parameters.Add(new SqlParameter("@pwd", AddUserHandler.GetSHA256HashFromString(pwd)));

                                int rowsAffected = (int)cmd.ExecuteScalar();

                                return (rowsAffected > 0) ? "修改成功" : "修改失敗";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                        return "錯誤";
                    }
                }
                else
                {
                    return "名稱和密碼不能含有非英文和數字且長度應在6到16之間且腳色不能為空";
                }
            }
                
        }

        /// <summary>
        /// 判斷輸入值
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static bool SpecialChar(string pwd)
        {
            bool cheackPwd = Regex.IsMatch(pwd, @"^[A-Za-z0-9]{6,16}$");

            if (cheackPwd)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}