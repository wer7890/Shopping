using NLog;
using ShoppingWeb.Response;
using System;
using System.Web;
using System.Web.Http;

namespace ShoppingWeb.Repository
{
    public class UserController
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