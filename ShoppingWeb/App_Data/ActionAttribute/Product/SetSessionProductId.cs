using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// SetSessionProductId方法參數驗證
    /// </summary>
    public class SetSessionProductId
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        public int ProductId { get; set; }
    }
}