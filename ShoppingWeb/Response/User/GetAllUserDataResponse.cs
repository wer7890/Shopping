using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ShoppingWeb.Response
{
    public class GetAllUserDataResponse : BaseResponse
    {
        public static GetAllUserDataResponse GetInstance(DataTable dt)
        {
            return new GetAllUserDataResponse()  //建立GetAllUserDataResponse()物件
            {
                UserDataList = dt.AsEnumerable().Select(dr => new UserData()  //dt.AsEnumerable()將DataTable 轉換為一個可枚舉的 EnumerableRowCollection<DataRow>，使得可以使用 LINQ 查詢。
                {                                                             //Select(dr => new UserData() { ... }): 使用 LINQ 的 Select 方法將每一行 DataRow 轉換為一個 UserData 對象。
                    Id = dr.Field<int>("f_id"),                               //new UserData() { ... }: 為每一行創建一个新的 UserData 對象，並從 DataRow 中提取相應的字段来初始化對象的屬性。
                    Account = dr.Field<string>("f_account"),                  //從當前 DataRow 提取 f_account 字段的值，並賦值给 UserData 對象的 Account 屬性。
                    Roles = dr.Field<byte>("f_roles"),                        //返回一個 GetAllUserDataResponse 對象，其 UserDataList 屬性包含了 DataTable 中所有行轉换後的 UserData 對象的集合。
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
        public IEnumerable<UserData> UserDataList { get; set; }  //IEnumerable<UserData>可以遍歷UserData對象的元素，UserDataList 可以包含一個 UserData 對象的集合，並且可以對這個集合進行遍歷（例如使用 foreach 循環）。

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