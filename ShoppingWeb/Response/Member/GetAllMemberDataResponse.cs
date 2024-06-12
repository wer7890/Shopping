using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ShoppingWeb.Response.Member
{
    public class GetAllMemberDataResponse : BaseResponse
    {
        public static GetAllMemberDataResponse GetInstance(DataTable dt)
        {
            return new GetAllMemberDataResponse()
            {
                MemberDataList = dt.AsEnumerable().Select(dr => new MemberData()
                {
                    Id = dr.Field<int>("f_id"),
                    Account = dr.Field<string>("f_account"),
                    Pwd = dr.Field<string>("f_pwd"),
                    Name = dr.Field<string>("f_name"),
                    Level = dr.Field<byte>("f_level"),
                    PhoneNumber = dr.Field<string>("f_phoneNumber"),
                    Email = dr.Field<string>("f_email"),
                    AccountStatus = dr.Field<bool>("f_accountStatus"),
                    Amount = dr.Field<int>("f_amount"),
                    TotalSpent = dr.Field<int>("f_totalSpent"),
                })
            };
        }

        /// <summary>
        /// 總頁數
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 會員清單
        /// </summary>
        public IEnumerable<MemberData> MemberDataList { get; set; }

        public class MemberData
        {
            /// <summary>
            /// ID
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// 帳號
            /// </summary>
            public string Account { get; set; }

            /// <summary>
            /// 密碼
            /// </summary>
            public string Pwd { get; set; }

            /// <summary>
            /// 名稱
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 等級
            /// </summary>
            public byte Level { get; set; }

            /// <summary>
            /// 手機
            /// </summary>
            public string PhoneNumber { get; set; }

            /// <summary>
            /// 信箱
            /// </summary>
            public string Email { get; set; }

            /// <summary>
            /// 狀態
            /// </summary>
            public bool AccountStatus { get; set; }

            /// <summary>
            /// 錢包
            /// </summary>
            public int Amount { get; set; }

            /// <summary>
            /// 總花費
            /// </summary>
            public int TotalSpent { get; set; }
        }
    }
}