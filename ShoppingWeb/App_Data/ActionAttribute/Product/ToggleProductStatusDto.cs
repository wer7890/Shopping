using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// ToggleProductStatus方法參數驗證
    /// </summary>
    public class ToggleProductStatusDto
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        public int ProductId { get; set; }
    }
}