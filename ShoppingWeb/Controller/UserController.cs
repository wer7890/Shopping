using NLog;
using ShoppingWeb.Filters;
using ShoppingWeb.Response;
using System;
using System.Data;
using System.Web;
using System.Web.Http;
using ShoppingWeb.Repository;
using System.Text.RegularExpressions;

namespace ShoppingWeb.Controller
{
    [RoutePrefix("/api/Controller/user")]
    [RolesFilter((int)Roles.User)]
    public class UserController : BaseController
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
        [HttpPost]
        [Route("AddUser")]
        public BaseResponse AddUser([FromBody] AddUserDto dto)
        {
            try
            {
                if (!Regex.IsMatch(dto.Account, @"^[A-Za-z0-9]{6,16}$") || !Regex.IsMatch(dto.Pwd, @"^[A-Za-z0-9]{6,16}$") || !(dto.Roles >= 1 && dto.Roles <= 3))
                {
                    return new BaseResponse
                    {
                        Status = ActionResult.InputError
                    };
                }

                (Exception exc, int? result) = this.UserRepo.AddUser(dto);

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

        /// <summary>
        /// 刪除管理員
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DelUserInfo")]
        public BaseResponse DelUserInfo([FromBody] DelUserInfoDto dto)
        {
            try
            {
                if (!(dto.UserId >= 1 && dto.UserId <= int.MaxValue))
                {
                    return new BaseResponse
                    {
                        Status = ActionResult.InputError
                    };
                }

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

        /// <summary>
        /// 更改密碼
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditUser")]
        public BaseResponse EditUser([FromBody] EditUserDto dto)
        {
            try
            {
                if (!(dto.UserId >= 1 && dto.UserId <= int.MaxValue) || !Regex.IsMatch(dto.Pwd, @"^[A-Za-z0-9]{6,16}$"))
                {
                    return new BaseResponse
                    {
                        Status = ActionResult.InputError
                    };
                }

                (Exception exc, int? result) = this.UserRepo.EditUser(dto);

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

        /// <summary>
        /// 更改管理員身分
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditUserRoles")]
        public BaseResponse EditUserRoles([FromBody] EditRolesDto dto)
        {
            try
            {
                if (!(dto.UserId >= 1 && dto.UserId <= int.MaxValue) || !(dto.Roles >= 1 && dto.Roles <= 3))
                {
                    return new BaseResponse
                    {
                        Status = ActionResult.InputError
                    };
                }

                (Exception exc, int? result) = this.UserRepo.EditUserRoles(dto);

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

        /// <summary>
        /// 顯示所有管理員，依照分頁
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetSessionSelectUserId")]
        public GetAllUserDataResponse GetAllUserData([FromBody] GetAllUserDataDto dto)
        {
            try
            {
                if (!(dto.PageNumber >= 1 && dto.PageNumber <= int.MaxValue) || !(dto.PageSize >= 1 && dto.PageSize <= int.MaxValue) || !(dto.BeforePagesTotal >= 1 && dto.BeforePagesTotal <= int.MaxValue))
                {
                    return new GetAllUserDataResponse
                    {
                        Status = ActionResult.InputError
                    };
                }

                (Exception exc, int? totalCount, DataTable dt) = this.UserRepo.GetAllUserData(dto);

                if (exc != null)
                {
                    throw exc;
                }

                if (totalCount > 0)
                { 
                    int totalPages = (int)Math.Ceiling((double)totalCount / dto.PageSize);  // 計算總頁數，Math.Ceiling向上進位取整數
                    GetAllUserDataResponse result = GetAllUserDataResponse.GetInstance(dt);
                    result.TotalPages = totalPages;
                    result.Status = ActionResult.Success;

                    return result;
                }
                else
                {
                    return new GetAllUserDataResponse
                    {
                        Status = ActionResult.Failure
                    };
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new GetAllUserDataResponse
                {
                    Status = ActionResult.Error
                };
            }
        }
    }
}