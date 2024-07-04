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

        public BaseResponse DelUserInfo([FromBody] DelUserInfoDto dto)
        {
            try
            {
                (Exception exc, int? result) = this.UserRepo.DelUserInfo(dto);

                if (exc != null)
                {
                    throw exc;
                }

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