﻿using NLog;
using ShoppingWeb.Filters;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Http;

namespace ShoppingWeb.Controller
{
    [RoutePrefix("/api/Controller/member")]
    [RolesFilter((int)Roles.Member)]
    public class MemberController : BaseController
    {
        /// <summary>
        /// 一開始顯示所有會員資訊
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAllMemberData")]
        public ApiResponse GetAllMemberData([FromBody] GetAllMemberDataDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getAllMemberData", con))
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

                        var result = new
                        {
                            Data = ConvertDataTableToJson(dt),
                            TotalPages = totalPages
                        };

                        return new ApiResponse
                        {
                            Data = result,
                            Msg = (int)DatabaseOperationResult.Success
                        };

                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new ApiResponse
                {
                    Data = null,
                    Msg = (int)DatabaseOperationResult.Error
                };
            }
        }

        /// <summary>
        /// 是否啟用
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditMemberStatus")]
        public ApiResponse EditMemberStatus([FromBody] EditMemberStatusDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editMemberStatus", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@memberId", dto.MemberId));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return new ApiResponse
                        {
                            Data = null,
                            Msg = rowsAffected > 0 ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure
                        };

                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new ApiResponse
                {
                    Data = null,
                    Msg = (int)DatabaseOperationResult.Error
                };
            }
        }

        /// <summary>
        /// 更改會員等級
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditMemberLevel")]
        public ApiResponse EditMemberLevel([FromBody] EditMemberLevelDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editMemberLevel", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@memberId", dto.MemberId));
                        cmd.Parameters.Add(new SqlParameter("@level", dto.Level));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return new ApiResponse
                        {
                            Data = null,
                            Msg = rowsAffected > 0 ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new ApiResponse
                {
                    Data = null,
                    Msg = (int)DatabaseOperationResult.Error
                };

            }
        }

        /// <summary>
        /// 新增會員資料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AddMember")]
        public ApiResponse AddMember([FromBody] AddMemberDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_addMemberData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@account", dto.Account));
                        cmd.Parameters.Add(new SqlParameter("@pwd", dto.Pwd));
                        cmd.Parameters.Add(new SqlParameter("@name", dto.Name));
                        cmd.Parameters.Add(new SqlParameter("@birthday", DateTime.Parse(dto.Birthday)));
                        cmd.Parameters.Add(new SqlParameter("@phone", dto.Phone));
                        cmd.Parameters.Add(new SqlParameter("@email", dto.Email));
                        cmd.Parameters.Add(new SqlParameter("@address", dto.Address));

                        int result = (int)cmd.ExecuteScalar();

                        return new ApiResponse
                        {
                            Data = null,
                            Msg = result == 1 ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new ApiResponse
                {
                    Data = null,
                    Msg = (int)DatabaseOperationResult.Error
                };

            }
        }

    }
}