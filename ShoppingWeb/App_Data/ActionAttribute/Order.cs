using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// GetAllOrderData和GetReturnOrderData方法參數驗證
    /// </summary>
    public class GetAllOrderDataAttribute
    {
        /// <summary>
        /// 現在頁數
        /// </summary>
        [Required]
        public int PageNumber { get; set; }

        /// <summary>
        /// 每頁資料筆數
        /// </summary>
        [Required]
        public int PageSize { get; set; }

        /// <summary>
        /// 之前頁數
        /// </summary>
        [Required]
        public int BeforePagesTotal { get; set; }
    }

    /// <summary>
    /// GetOrderDetailsData方法參數驗證
    /// </summary>
    public class GetOrderDetailsDataAttribute
    {
        /// <summary>
        /// 訂單ID
        /// </summary>
        [Required]
        public int OrderId { get; set; }
    }

    /// <summary>
    /// EditOrder方法參數驗證
    /// </summary>
    public class EditOrderAttribute
    {
        /// <summary>
        /// 訂單ID
        /// </summary>
        [Required]
        public int OrderId { get; set; }

        /// <summary>
        /// 訂單狀態
        /// </summary>
        [Required]
        public int OrderStatusNum { get; set; }

        /// <summary>
        /// 配送狀態
        /// </summary>
        [Required]
        public int DeliveryStatusNum { get; set; }

        /// <summary>
        /// 配送方式
        /// </summary>
        [Required]
        public int DeliveryMethodNum { get; set; }
    }

    /// <summary>
    /// GetOrderData方法參數驗證
    /// </summary>
    public class GetOrderDataAttribute
    {
        /// <summary>
        /// 配送狀態
        /// </summary>
        [Required]
        public int DeliveryStatusNum { get; set; }

        /// <summary>
        /// 現在頁數
        /// </summary>
        [Required]
        public int PageNumber { get; set; }

        /// <summary>
        /// 每頁資料筆數
        /// </summary>
        [Required]
        public int PageSize { get; set; }

        /// <summary>
        /// 之前頁數
        /// </summary>
        [Required]
        public int BeforePagesTotal { get; set; }
    }

    /// <summary>
    /// EditReturnOrder方法參數驗證
    /// </summary>
    public class EditReturnOrderAttribute
    {
        /// <summary>
        /// 訂單ID
        /// </summary>
        [Required]
        public int OrderId { get; set; }

        /// <summary>
        /// 是否同意退貨
        /// </summary>
        [Required]
        public bool BoolReturn { get; set; }
    }
}