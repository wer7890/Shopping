using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ShoppingWeb.Repository
{
    public class LoginRepository : BaseRepository, ILoginRepository
    {
        /// <summary>
        /// 登入，如果成功就把sessionId寫入資料庫，並且創建userInfo物件把userId和roles存到userInfo物件中，再存到Session["userInfo"]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, int?) LoginUser(LoginUserDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getPwdAndEditSessionId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@account", dto.Account));
                        cmd.Parameters.Add(new SqlParameter("@pwd", GetSHA256HashFromString(dto.Pwd)));
                        cmd.Parameters.Add(new SqlParameter("@sessionId", HttpContext.Current.Session.SessionID.ToString()));
                        cmd.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int));
                        cmd.Parameters["@userId"].Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(new SqlParameter("@roles", SqlDbType.Int));
                        cmd.Parameters["@roles"].Direction = ParameterDirection.Output;

                        int result = (int)cmd.ExecuteScalar();

                        if (result == 1)
                        {
                            UserInfo user = new UserInfo
                            {
                                UserId = (int)cmd.Parameters["@userId"].Value,
                                Roles = (int)cmd.Parameters["@roles"].Value,
                                Account = dto.Account
                            };
                            HttpContext.Current.Session["userInfo"] = user;                          
                        }

                        return (null, (int)cmd.ExecuteScalar());
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