using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// EditOrder方法參數驗證
    /// </summary>
    public class EditOrder
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
}