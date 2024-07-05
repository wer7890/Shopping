﻿using NLog;
using ShoppingWeb.Filters;
using ShoppingWeb.Response;
using System;
using System.Data;
using System.Data.SqlClient;
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
                if (!Regex.IsMatch(dto.Pwd, @"^[A-Za-z0-9]{6,16}$"))
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
        /// 刪除管理員
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("DelUserInfo")]
        //public BaseResponse DelUserInfo([FromBody] DelUserInfoDto dto)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("pro_sw_delUserData", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                con.Open();
        //                cmd.Parameters.Add(new SqlParameter("@userId", dto.UserId));

        //                int r = (int)cmd.ExecuteScalar();

        //                return new BaseResponse
        //                {
        //                    Status = (r > 0) ? ActionResult.Success : ActionResult.Failure
        //                };
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger logger = LogManager.GetCurrentClassLogger();
        //        logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
        //        return new BaseResponse
        //        {
        //            Status = ActionResult.Error
        //        };
        //    }
        //}

        /// <summary>
        /// 設定Session["selectUserId"]
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetSessionSelectUserId")]
        public BaseResponse SetSessionSelectUserId([FromBody] SetSessionSelectUserIdDto dto)
        {
            HttpContext.Current.Session["selectUserId"] = dto.UserId;  //存儲資料到 Session 變數
            return new BaseResponse
            {
                Status = ActionResult.Success
            };
        }

        /// <summary>
        /// 顯示所有管理員，依照分頁
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetSessionSelectUserId")]
        public GetAllUserDataResponse GetAllUserData([FromBody] GetAllUserDataDto dto)
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
                        int totalPages = (int)Math.Ceiling((double)totalCount / dto.PageSize);  // 計算總頁數，Math.Ceiling向上進位取整數

                        if (totalCount > 0)
                        {
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

        /// <summary>
        /// 更改管理員身分
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditUserRoles")]
        public BaseResponse EditUserRoles([FromBody] EditRolesDto dto)
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

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return new BaseResponse
                        {
                            Status = (rowsAffected > 0) ? ActionResult.Success : ActionResult.Failure
                        };
                    }
                }
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
        /// 新增管理員，會先判斷使用者名稱是否存在
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("AddUser")]
        //public BaseResponse AddUser([FromBody] AddUserDto dto)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("pro_sw_addUserData", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                con.Open();
        //                cmd.Parameters.Add(new SqlParameter("@account", dto.Account));
        //                cmd.Parameters.Add(new SqlParameter("@pwd", GetSHA256HashFromString(dto.Pwd)));
        //                cmd.Parameters.Add(new SqlParameter("@roles", dto.Roles));

        //                int result = (int)cmd.ExecuteScalar();

        //                return new BaseResponse
        //                {
        //                    Status = (result == 1) ? ActionResult.Success : ActionResult.Failure
        //                };
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger logger = LogManager.GetCurrentClassLogger();
        //        logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
        //        return new BaseResponse
        //        {
        //            Status = ActionResult.Error
        //        };
        //    }
        //}


        /// <summary>
        /// 設定跳轉道編輯帳號頁面時，input裡面的預設值
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUserDataForEdit")]
        public GetUserDataForEditResponse GetUserDataForEdit()
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

                            GetUserDataForEditResponse result = GetUserDataForEditResponse.GetInstance(dt);
                            result.Status = ActionResult.Success;

                            return result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new GetUserDataForEditResponse
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
        //[HttpPost]
        //[Route("EditUser")]
        //public BaseResponse EditUser([FromBody] EditUserDto dto)
        //{
        //    try
        //    {
        //        string sessionUserId = HttpContext.Current.Session["selectUserId"].ToString();
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("pro_sw_editPwd", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                con.Open();

        //                cmd.Parameters.Add(new SqlParameter("@userId", sessionUserId));
        //                cmd.Parameters.Add(new SqlParameter("@pwd", GetSHA256HashFromString(dto.Pwd)));

        //                int rowsAffected = (int)cmd.ExecuteScalar();

        //                return new BaseResponse
        //                {
        //                    Status = (rowsAffected > 0) ? ActionResult.Success : ActionResult.Failure
        //                };
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger logger = LogManager.GetCurrentClassLogger();
        //        logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
        //        return new BaseResponse
        //        {
        //            Status = ActionResult.Error
        //        };
        //    }
        //}
    }
}