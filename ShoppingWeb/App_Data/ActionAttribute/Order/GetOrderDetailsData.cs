using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// GetOrderDetailsData方法參數驗證
    /// </summary>
    public class GetOrderDetailsData
    {
        /// <summary>
        /// 訂單ID
        /// </summary>
        [Required]
        public int OrderId { get; set; }
    }
}