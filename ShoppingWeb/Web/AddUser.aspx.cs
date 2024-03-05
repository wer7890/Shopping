﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingWeb.Web
{
    public partial class AddUser1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)  //頁面加載第一次時
            {
                if (Session["userName"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        /// <summary>
        /// 新增管理員
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [WebMethod]
        public static string AddUser(string userName, string pwd, string roles)
        {
            string strAddUserResult = null;
            try
            {

                if (!IsCheckUserName(userName))  //檢查名稱是否重複
                {

                    if (IsAddUser(userName, pwd, roles))
                    {
                        strAddUserResult = "新增成功";
                    }
                    else
                    {
                        strAddUserResult = "新增失敗";
                    }

                }
                else
                {
                    strAddUserResult = "管理員名稱重複";
                }


                return strAddUserResult;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                // 將詳細錯誤信息寫入日誌或回應
                return "發生內部錯誤: " + ex.Message;
            }
        }

        /// <summary>
        /// 判斷新增的ID是否重複
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsCheckUserName(string name)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT COUNT(*) FROM t_userInfo where f_userName=@name";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.Parameters.Add(new SqlParameter("@name", name));

                    // 使用 ExecuteScalar 取得結果集的第一行第一列的值
                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;

                }
            }
        }


        /// <summary>
        /// 新增管理員資料
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static bool IsAddUser(string name, string pwd, string roles)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
            bool addResult = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO t_userInfo VALUES(@name, @pwd, @roles)";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();

                    cmd.Parameters.Add(new SqlParameter("@name", name));
                    cmd.Parameters.Add(new SqlParameter("@pwd", pwd));
                    cmd.Parameters.Add(new SqlParameter("@roles", roles));

                    int r = cmd.ExecuteNonQuery();

                    if (r > 0)
                    {
                        addResult = true;
                    }
                    else
                    {
                        addResult = false;
                    }
                }
            }

            return addResult;
        }
    }
}