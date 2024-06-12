using System;
using System.Collections.Generic;
using System.Data;

namespace ShoppingWeb.Response.Order
{
    public class GetAllOrderDataResponse : BaseResponse
    {
        public static GetAllOrderDataResponse GetInstance(DataSet ds)
        {
            if (ds.Tables.Count < 2)
            {
                throw new Exception("資料表數量錯誤!");
            }

            return new GetAllOrderDataResponse()
            {
                OrderList = ds.Tables[0].AsEnumerable().Select(dr => new OrderData()
                {
                    Id = dr.Field<int>("f_id"),
                    Account = dr.Field<string>("f_account"),
                    CreatedTime = dr.Field<string>("f_createdTime"),
                    OrderStatus = dr.Field<byte>("f_orderStatus"),
                    DeliveryStatus = dr.Field<byte>("f_deliveryStatus"),
                    DeliveryMethod = dr.Field<byte>("f_deliveryMethod"),
                    Total = dr.Field<int>("f_total"),
                }),

                StatusList = ds.Tables[1].AsEnumerable().Select(dr => new OrderStatus()
                {
                    All = dr.Field<int>("statusAll"),
                    Shipping = dr.Field<int>("status1"),
                    Shipped = dr.Field<int>("status2"),
                    Arrived = dr.Field<int>("status3"),
                    Received = dr.Field<int>("status4"),
                    Returning = dr.Field<int>("status5"),
                    Returned = dr.Field<int>("status6"),
                    Return = dr.Field<int>("orderStatus2"),
                })
            };
        }

        /// <summary>
        /// 總頁數
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 訂單清單
        /// </summary>
        public IEnumerable<OrderData> OrderList { get; set; }

        /// <summary>
        /// 狀態列表
        /// </summary>
        public IEnumerable<OrderStatus> StatusList { get; set; }

        public class OrderStatus
        {
            /// <summary>
            /// 全部
            /// </summary>
            public int All { get; set; }

            /// <summary>
            /// 發貨中
            /// </summary>
            public int Shipping { get; set; }

            /// <summary>
            /// 已發貨
            /// </summary>
            public int Shipped { get; set; }

            /// <summary>
            /// 已到貨
            /// </summary>
            public int Arrived { get; set; }

            /// <summary>
            /// 已取貨
            /// </summary>
            public int Received { get; set; }

            /// <summary>
            /// 退貨中
            /// </summary>
            public int Returning { get; set; }

            /// <summary>
            /// 已退貨
            /// </summary>
            public int Returned { get; set; }

            /// <summary>
            /// 申請退貨
            /// </summary>
            public int Return { get; set; }
        }

        public class OrderData
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
            /// 建立時間
            /// </summary>
            public string CreatedTime { get; set; }

            /// <summary>
            /// 訂單狀態
            /// </summary>
            public byte OrderStatus { get; set; }

            /// <summary>
            /// 配送狀態
            /// </summary>
            public byte DeliveryStatus { get; set; }

            /// <summary>
            /// 配送方式
            /// </summary>
            public byte DeliveryMethod { get; set; }

            /// <summary>
            /// 總金額
            /// </summary>
            public int Total { get; set; }
        }
    }
}