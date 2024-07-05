using ShoppingWeb.Response;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ShoppingWeb.Repository
{
    public class UserRepository : IUserRepository  //實作介面(介面有宣告哪些方法或變數，實作中就必須都要去定義)
    {
        public readonly string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
        public string GetSHA256HashFromString(string strData)
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
        /// 新增管理員，會先判斷使用者名稱是否存在
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, int?) AddUser(AddUserDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_addUserData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@account", dto.Account));
                        cmd.Parameters.Add(new SqlParameter("@pwd", GetSHA256HashFromString(dto.Pwd)));
                        cmd.Parameters.Add(new SqlParameter("@roles", dto.Roles));

                        return (null, (int)cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }

        /// <summary>
        /// 刪除管理員
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, int?) DelUserInfo(DelUserInfoDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_delUserData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userId", dto.UserId));

                        return (null, (int)cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }

        /// <summary>
        /// 更改密碼
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, int?) EditUser(EditUserDto dto)
        {
            try
            {
                string sessionUserId = HttpContext.Current.Session["selectUserId"].ToString();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editPwd", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        cmd.Parameters.Add(new SqlParameter("@userId", sessionUserId));
                        cmd.Parameters.Add(new SqlParameter("@pwd", GetSHA256HashFromString(dto.Pwd)));
                     
                        return (null, (int)cmd.ExecuteScalar());                      
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }

        /// <summary>
        /// 更改管理員身分
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, int?) EditUserRoles(EditRolesDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editRoles", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userId", dto.UserId));
                        cmd.Parameters.Add(new SqlParameter("@roles", dto.Roles));

                        return (null, (int)cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }

        /// <summary>
        /// 設定Session["selectUserId"]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, int?) SetSessionSelectUserId(SetSessionSelectUserIdDto dto)
        {
            try
            {
                HttpContext.Current.Session["selectUserId"] = dto.UserId;  //存儲資料到 Session 變數

                if ((int)HttpContext.Current.Session["selectUserId"] == dto.UserId)
                {
                    return (null, 1);
                }
                else
                {
                    return (null, 0);
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
            
        }

        /// <summary>
        /// 顯示所有管理員，依照分頁
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, int?, DataTable) GetAllUserData(GetAllUserDataDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getAllUserData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", dto.PageNumber));
                        cmd.Parameters.Add(new SqlParameter("@pageSize", dto.PageSize));
                        cmd.Parameters.Add(new SqlParameter("@beforePagesTotal", dto.BeforePagesTotal));
                        cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                        cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;

                        SqlDataReader reader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
                        
                        return (null, totalCount, dt);
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex, null, null);
            }
        }

        /// <summary>
        /// 設定跳轉道編輯帳號頁面時，input裡面的預設值
        /// </summary>
        /// <returns></returns>
        public (Exception, DataTable) GetUserDataForEdit()
        {
            try
            {
                string sessionUserId = HttpContext.Current.Session["selectUserId"].ToString();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getUserData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userId", sessionUserId));

                        using (SqlDataAdapter sqlData = new SqlDataAdapter(cmd))
                        {
                            SqlDataReader reader = cmd.ExecuteReader();
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            
                            return (null, dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }
    }
}