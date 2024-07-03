using NLog;
using ShoppingWeb.Response;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;

namespace ShoppingWeb.Repository
{
    public interface IUserRepository
    {
        (Exception, int?) AddUser(AddUserDto dto);
    };

    public class UserRepository : IUserRepository
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
    }




    public class TestRepository
    {
        private IUserRepository _userRepo;
        private IUserRepository UserRepo
        {
            get
            {
                if (this._userRepo == null)
                {
                    this._userRepo = new UserRepository();
                }

                return this._userRepo;

            }
        }


        /// <summary>
        /// 新增管理員，會先判斷使用者名稱是否存在
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public BaseResponse AddUser([FromBody] AddUserDto dto)
        {
            try
            {
                (Exception exc, int? result) = this.UserRepo.AddUser(dto);

                if (exc != null)
                    throw exc;

                return new BaseResponse
                {
                    Status = (result == 1) ? ActionResult.Success : ActionResult.Failure
                };
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new BaseResponse
                {
                    Status = ActionResult.Error
                };
            }
        }
    }
}