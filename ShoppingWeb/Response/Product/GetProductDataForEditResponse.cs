using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ShoppingWeb.Response
{
    public class GetProductDataForEditResponse : BaseResponse
    {
        public static GetProductDataForEditResponse GetInstance(DataTable dt)
        {
            return new GetProductDataForEditResponse()
            {
                ProductDataList = dt.AsEnumerable().Select(dr => new ProductData()
                {
                    Id = dr.Field<int>("f_id"),
                    NameTW = dr.Field<string>("f_nameTW"),
                    NameEN = dr.Field<string>("f_nameEN"),
                    Category = dr.Field<int>("f_category"),
                    Price = dr.Field<int>("f_price"),
                    Stock = dr.Field<int>("f_stock"),
                    CreatedUser = dr.Field<int>("f_createdUser"),
                    CreatedTime = dr.Field<string>("f_createdTime"),
                    IntroduceTW = dr.Field<string>("f_introduceTW"),
                    IntroduceEN = dr.Field<string>("f_introduceEN"),
                    Img = dr.Field<string>("f_img"),
                    WarningValue = dr.Field<int>("f_warningValue"),
                })
            };
        }

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
            /// 中文名稱
            /// </summary>
            public string NameTW { get; set; }

            /// <summary>
            /// 英文名稱
            /// </summary>
            public string NameEN { get; set; }

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
            /// 建立者
            /// </summary>
            public int CreatedUser { get; set; }

            /// <summary>
            /// 建立時間
            /// </summary>
            public string CreatedTime { get; set; }

            /// <summary>
            /// 中文描述
            /// </summary>
            public string IntroduceTW { get; set; }

            /// <summary>
            /// 英文描述
            /// </summary>
            public string IntroduceEN { get; set; }

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