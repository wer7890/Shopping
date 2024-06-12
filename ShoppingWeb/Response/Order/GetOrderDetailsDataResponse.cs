using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ShoppingWeb.Response.Order
{
    public class GetOrderDetailsDataResponse : BaseResponse
    {
        public static GetOrderDetailsDataResponse GetInstance(DataTable dt)
        {
            return new GetOrderDetailsDataResponse()
            {
                OrderDetailList = dt.AsEnumerable().Select(dr => new OrderDetail()
                {
                    ProductName = dr.Field<string>("f_productName"),
                    ProductPrice = dr.Field<int>("f_productPrice"),
                    ProductCategory = dr.Field<int>("f_productCategory"),
                    Quantity = dr.Field<int>("f_quantity"),
                    Subtotal = dr.Field<int>("f_subtotal"),
                })
            };
        }



        /// <summary>
        /// 訂單詳情清單
        /// </summary>
        public IEnumerable<OrderDetail> OrderDetailList { get; set; }

        public class OrderDetail
        {
            /// <summary>
            /// 商品名稱
            /// </summary>
            public string ProductName { get; set; }

            /// <summary>
            /// 商品價格
            /// </summary>
            public int ProductPrice { get; set; }

            /// <summary>
            /// 商品類型
            /// </summary>
            public int ProductCategory { get; set; }

            /// <summary>
            /// 商品數量
            /// </summary>
            public int Quantity { get; set; }

            /// <summary>
            /// 小記
            /// </summary>
            public int Subtotal { get; set; }
        }

        
    }
}
