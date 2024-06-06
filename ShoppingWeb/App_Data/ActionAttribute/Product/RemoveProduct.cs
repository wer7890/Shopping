using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// RemoveProduct方法參數驗證
    /// </summary>
    public class RemoveProduct
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        public int ProductId { get; set; }
    }
}