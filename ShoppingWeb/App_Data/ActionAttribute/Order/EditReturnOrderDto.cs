using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// EditReturnOrder方法參數驗證
    /// </summary>
    public class EditReturnOrderDto
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