using NLog;
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
    [ValidationFilter]
    public class MemberController : BaseController
    {
        /// <summary>
        /// 一開始顯示所有會員資訊
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAllMemberData")]
        public object GetAllMemberData([FromBody] GetAllMemberData attribute)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getAllMemberData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@pageNumber", attribute.PageNumber));
                        cmd.Parameters.Add(new SqlParameter("@pageSize", attribute.PageSize));
                        cmd.Parameters.Add(new SqlParameter("@beforePagesTotal", attribute.BeforePagesTotal));
                        cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                        cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;
                        SqlDataReader reader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
                        int totalPages = (int)Math.Ceiling((double)totalCount / attribute.PageSize);  // 計算總頁數，Math.Ceiling向上進位取整數

                        var result = new
                        {
                            Data = ConvertDataTableToJson(dt),
                            TotalPages = totalPages
                        };

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 是否啟用
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ToggleProductStatus")]
        public int ToggleMemberStatus([FromBody] ToggleMemberStatus attribute)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editMemberStatus", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@memberId", attribute.MemberId));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return (rowsAffected > 0) ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure;

                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 更改會員等級
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ToggleMemberLevel")]
        public int ToggleMemberLevel([FromBody] ToggleMemberLevel attribute)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editMemberLevel", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@memberId", attribute.MemberId));
                        cmd.Parameters.Add(new SqlParameter("@level", attribute.Level));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return (rowsAffected > 0) ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return (int)DatabaseOperationResult.Error;
            }
        }

        /// <summary>
        /// 新增會員資料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AddMember")]
        public int AddMember([FromBody] AddMember attribute)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_addMemberData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@account", attribute.Account));
                        cmd.Parameters.Add(new SqlParameter("@pwd", attribute.Pwd));
                        cmd.Parameters.Add(new SqlParameter("@name", attribute.Name));
                        cmd.Parameters.Add(new SqlParameter("@birthday", DateTime.Parse(attribute.Birthday)));
                        cmd.Parameters.Add(new SqlParameter("@phone", attribute.Phone));
                        cmd.Parameters.Add(new SqlParameter("@email", attribute.Email));
                        cmd.Parameters.Add(new SqlParameter("@address", attribute.Address));

                        int result = (int)cmd.ExecuteScalar();

                        return (result == 1) ? (int)DatabaseOperationResult.Success : (int)DatabaseOperationResult.Failure;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return (int)DatabaseOperationResult.Error;
            }
        }

    }
}