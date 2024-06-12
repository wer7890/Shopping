using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ShoppingWeb.Response
{
    public class GetAllProductDataResponse : BaseResponse
    {
        public static GetAllProductDataResponse GetInstance(DataTable dt)
        {
            return new GetAllProductDataResponse()
            {
                ProductDataList = dt.AsEnumerable().Select(dr => new ProductData()
                {
                    Id = dr.Field<int>("f_id"),
                    Name = dr.Field<string>("f_name"),
                    Category = dr.Field<int>("f_category"),
                    Price = dr.Field<int>("f_price"),
                    Stock = dr.Field<int>("f_stock"),
                    IsOpen = dr.Field<bool>("f_isOpen"),
                    Introduce = dr.Field<string>("f_introduce"),
                    Img = dr.Field<string>("f_img"),
                    WarningValue = dr.Field<int>("f_warningValue"),
                })
            };
        }

        /// <summary>
        /// 總頁數
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 商品清單
        /// </summary>
        public IEnumerable<ProductData> ProductDataList { get; set; }

        public class ProductData
        {
            /// <summary>
            /// ID
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// 名稱
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 類型
            /// </summary>
            public int Category { get; set; }

            /// <summary>
            /// 價格
            /// </summary>
            public int Price { get; set; }

            /// <summary>
            /// 庫存量
            /// </summary>
            public int Stock { get; set; }

            /// <summary>
            /// 是否開放
            /// </summary>
            public bool IsOpen { get; set; }

            /// <summary>
            /// 描述
            /// </summary>
            public string Introduce { get; set; }

            /// <summary>
            /// 圖片
            /// </summary>
            public string Img { get; set; }

            /// <summary>
            /// 庫存量預警值
            /// </summary>
            public int WarningValue { get; set; }
        }
    }
}