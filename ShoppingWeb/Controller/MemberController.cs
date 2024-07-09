﻿using NLog;
using ShoppingWeb.Filters;
using ShoppingWeb.Repository;
using ShoppingWeb.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace ShoppingWeb.Controller
{
    [RoutePrefix("/api/Controller/member")]
    [RolesFilter((int)Roles.Member)]
    public class MemberController : BaseController
    {
        private IMemberRepository _memberRepo;

        private IMemberRepository MemberRepo
        {
            get
            {
                if (this._memberRepo == null)
                {
                    this._memberRepo = new MemberRepository();
                }

                return this._memberRepo;
            }
        }

        /// <summary>
        /// 是否啟用
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditMemberStatus")]
        public BaseResponse EditMemberStatus([FromBody] EditMemberStatusDto dto)
        {
            try
            {
                if (!(dto.MemberId >= 1 && dto.MemberId <= int.MaxValue))
                {
                    return new BaseResponse
                    {
                        Status = ActionResult.InputError
                    };
                }

                (Exception exc, int? result) = this.MemberRepo.EditMemberStatus(dto);

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
                this.MemberRepo.SetNLog(ex);
                return new BaseResponse
                {
                    Status = ActionResult.Error
                };
            }
        }

        /// <summary>
        /// 更改會員等級
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditMemberLevel")]
        public BaseResponse EditMemberLevel([FromBody] EditMemberLevelDto dto)
        {
            try
            {
                if (!(dto.MemberId >= 1 && dto.MemberId <= int.MaxValue) || !(dto.Level >= 0 && dto.Level <= 3))
                {
                    return new BaseResponse
                    {
                        Status = ActionResult.InputError
                    };
                }

                (Exception exc, int? result) = this.MemberRepo.EditMemberLevel(dto);

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
                this.MemberRepo.SetNLog(ex);
                return new BaseResponse
                {
                    Status = ActionResult.Error
                };

            }
        }

        /// <summary>
        /// 新增會員資料
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddMember")]
        public BaseResponse AddMember([FromBody] AddMemberDto dto)
        {
            try
            {
                if (!Regex.IsMatch(dto.Account, @"^[A-Za-z0-9]{6,16}$") || !Regex.IsMatch(dto.Pwd, @"^[A-Za-z0-9]{6,16}$") || !Regex.IsMatch(dto.Name, @"^[\u4E00-\u9FFF]{1,15}$") || !Regex.IsMatch(dto.Birthday, @"^[0-9-]{10}$") || !Regex.IsMatch(dto.Phone, @"^[0-9]{10}$") || !Regex.IsMatch(dto.Email, @"^[a-zA-Z0-9_.+-]{1,25}@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$") || !Regex.IsMatch(dto.Address, @"^[\u4E00-\u9FFF0-9A-Za-z]{2,50}$"))
                {
                    return new BaseResponse
                    {
                        Status = ActionResult.InputError
                    };
                }

                (Exception exc, int? result) = this.MemberRepo.AddMember(dto);

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
                this.MemberRepo.SetNLog(ex);
                return new BaseResponse
                {
                    Status = ActionResult.Error
                };

            }
        }

        /// <summary>
        /// 一開始顯示所有會員資訊
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAllMemberData")]
        public GetAllMemberDataResponse GetAllMemberData([FromBody] GetAllMemberDataDto dto)
        {
            try
            {
                if (!(dto.PageNumber >= 1 && dto.PageNumber <= int.MaxValue) || !(dto.PageSize >= 1 && dto.PageSize <= int.MaxValue) || !(dto.BeforePagesTotal >= 1 && dto.BeforePagesTotal <= int.MaxValue))
                {
                    return new GetAllMemberDataResponse
                    {
                        Status = ActionResult.InputError
                    };
                }

                (Exception exc, int? totalCount, DataTable dt) = this.MemberRepo.GetAllMemberData(dto);

                if (exc != null)
                {
                    throw exc;
                }

                if (totalCount > 0)
                {
                    int totalPages = (int)Math.Ceiling((double)totalCount / dto.PageSize);  // 計算總頁數，Math.Ceiling向上進位取整數
                    GetAllMemberDataResponse result = GetAllMemberDataResponse.GetInstance(dt);
                    result.TotalPages = totalPages;
                    result.Status = ActionResult.Success;

                    return result;
                }
                else
                {
                    return new GetAllMemberDataResponse
                    {
                        Status = ActionResult.Failure
                    };
                }
            }
            catch (Exception ex)
            {
                this.MemberRepo.SetNLog(ex);
                return new GetAllMemberDataResponse
                {
                    Status = ActionResult.Error
                };
            }
        }





        /// <summary>
        /// 一開始顯示所有會員資訊
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //[Route("GetAllMemberData")]
        //public GetAllMemberDataResponse GetAllMemberData([FromBody] GetAllMemberDataDto dto)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("pro_sw_getAllMemberData", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                con.Open();
        //                cmd.Parameters.Add(new SqlParameter("@pageNumber", dto.PageNumber));
        //                cmd.Parameters.Add(new SqlParameter("@pageSize", dto.PageSize));
        //                cmd.Parameters.Add(new SqlParameter("@beforePagesTotal", dto.BeforePagesTotal));
        //                cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
        //                cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;
        //                SqlDataReader reader = cmd.ExecuteReader();
        //                DataTable dt = new DataTable();
        //                dt.Load(reader);

        //                int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
        //                int totalPages = (int)Math.Ceiling((double)totalCount / dto.PageSize);  // 計算總頁數，Math.Ceiling向上進位取整數

        //                if (totalCount > 0)
        //                {
        //                    GetAllMemberDataResponse result = GetAllMemberDataResponse.GetInstance(dt);
        //                    result.TotalPages = totalPages;
        //                    result.Status = ActionResult.Success;

        //                    return result;
        //                }
        //                else
        //                {
        //                    return new GetAllMemberDataResponse
        //                    {
        //                        Status = ActionResult.Failure
        //                    };
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger logger = LogManager.GetCurrentClassLogger();
        //        logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
        //        return new GetAllMemberDataResponse
        //        {
        //            Status = ActionResult.Error
        //        };
        //    }
        //}

        /// <summary>
        /// 是否啟用
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("EditMemberStatus")]
        //public BaseResponse EditMemberStatus([FromBody] EditMemberStatusDto dto)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("pro_sw_editMemberStatus", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                con.Open();
        //                cmd.Parameters.Add(new SqlParameter("@memberId", dto.MemberId));

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

        /// <summary>
        /// 更改會員等級
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("EditMemberLevel")]
        //public BaseResponse EditMemberLevel([FromBody] EditMemberLevelDto dto)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("pro_sw_editMemberLevel", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                con.Open();
        //                cmd.Parameters.Add(new SqlParameter("@memberId", dto.MemberId));
        //                cmd.Parameters.Add(new SqlParameter("@level", dto.Level));

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

        /// <summary>
        /// 新增會員資料
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //[Route("AddMember")]
        //public BaseResponse AddMember([FromBody] AddMemberDto dto)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("pro_sw_addMemberData", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                con.Open();
        //                cmd.Parameters.Add(new SqlParameter("@account", dto.Account));
        //                cmd.Parameters.Add(new SqlParameter("@pwd", dto.Pwd));
        //                cmd.Parameters.Add(new SqlParameter("@name", dto.Name));
        //                cmd.Parameters.Add(new SqlParameter("@birthday", DateTime.Parse(dto.Birthday)));
        //                cmd.Parameters.Add(new SqlParameter("@phone", dto.Phone));
        //                cmd.Parameters.Add(new SqlParameter("@email", dto.Email));
        //                cmd.Parameters.Add(new SqlParameter("@address", dto.Address));

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

    }
}