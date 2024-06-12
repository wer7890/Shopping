using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ShoppingWeb.Response
{
    public class GetAllUserDataResponse : BaseResponse
    {
        public static GetAllUserDataResponse GetInstance(DataTable dt)
        {
            return new GetAllUserDataResponse()
            {
                UserDataList = dt.AsEnumerable().Select(dr => new UserData()
                {
                    Id = dr.Field<int>("f_id"),
                    Account = dr.Field<string>("f_account"),
                    Roles = dr.Field<byte>("f_roles"),
                })
            };
        }

        /// <summary>
        /// 總頁數
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 管理員清單
        /// </summary>
        public IEnumerable<UserData> UserDataList { get; set; }

        public class UserData
        {
            /// <summary>
            /// 管理員ID
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// 帳號
            /// </summary>
            public string Account { get; set; }

            /// <summary>
            /// 身分
            /// </summary>
            public byte Roles { get; set; }
        }
    }
}