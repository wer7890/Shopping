﻿using System;
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
    public partial class RenewUserHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static object SetRenewUserInput()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                string sessionUserId = HttpContext.Current.Session["userID"] as string;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "SELECT f_userId, f_userName, f_pwd, f_roles FROM t_userInfo WHERE f_userId=@id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Id", sessionUserId));

                        using (SqlDataAdapter sqlData = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sqlData.Fill(dt);

                            // 構建包含數據的匿名對象
                            var userObject = new
                            {
                                UserId = dt.Rows[0]["f_userId"],
                                UserName = dt.Rows[0]["f_userName"],
                                Password = dt.Rows[0]["f_pwd"],
                                Roles = dt.Rows[0]["f_roles"]
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



        [WebMethod]
        public static bool UpDataUser(string userId, string userName, string pwd, string roles)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                string sessionUserId = HttpContext.Current.Session["userID"] as string;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sql = "UPDATE t_userInfo SET f_userName=@userName, f_pwd=@pwd, f_roles=@roles WHERE f_userId=@id";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        con.Open();

                        cmd.Parameters.Add(new SqlParameter("@id", sessionUserId));
                        cmd.Parameters.Add(new SqlParameter("@pwd", pwd));
                        cmd.Parameters.Add(new SqlParameter("@userName", userName));
                        cmd.Parameters.Add(new SqlParameter("@roles", roles));

                        int r = cmd.ExecuteNonQuery();

                        if (r > 0)
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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                return false;
            }


        }
    }
}