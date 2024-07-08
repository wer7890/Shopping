using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb.Controller
{
    public class GetProductDataForEditDto
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }
    }
}