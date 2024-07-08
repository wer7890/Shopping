using NLog;
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
            if (HttpContext.Current.Session["userInfo"] == null)
            {
                return (null, 0);
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_delUserData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@userId", ((UserInfo)HttpContext.Current.Session["userInfo"]).UserId));
                        cmd.Parameters.Add(new SqlParameter("@delUserId", dto.UserId));

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
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editPwd", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        cmd.Parameters.Add(new SqlParameter("@userId", dto.UserId));
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
        /// 記錄錯誤日誌
        /// </summary>
        /// <param name="ex"></param>
        public void SetNLog(Exception ex)
        {
            Logger logger = LogManager.GetCurrentClassLogger();

            try
            {
                if (HttpContext.Current.Session["userInfo"] == null)
                {
                    logger.Error(ex + " 帳號: null");
                }
                else
                {
                    logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                }
            }
            catch (Exception)
            {
                logger.Error(ex, "後端紀錄NLog錯誤");
            }
        }
    }
}