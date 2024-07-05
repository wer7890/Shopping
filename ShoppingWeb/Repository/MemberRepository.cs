using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShoppingWeb.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public readonly string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

        /// <summary>
        /// 是否啟用
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, int?) EditMemberStatus(EditMemberStatusDto dto)
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
        /// 更改會員等級
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, int?) EditMemberLevel(EditMemberLevelDto dto)
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
        /// 新增會員資料
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, int?) AddMember(AddMemberDto dto)
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