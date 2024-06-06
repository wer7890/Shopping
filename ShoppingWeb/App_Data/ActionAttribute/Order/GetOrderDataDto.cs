using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// GetOrderData方法參數驗證
    /// </summary>
    public class GetOrderDataDto
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
}