using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// EditProduct方法參數驗證
    /// </summary>
    public class EditProductDto
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        //[Required]
        //[Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        /// <summary>
        /// 商品價格
        /// </summary>
        //[Required]
        public int ProductPrice { get; set; }

        /// <summary>
        /// 商品庫存
        /// </summary>
        //[Required]
        public int ProductStock { get; set; }

        /// <summary>
        /// 商品中文詳情
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string ProductIntroduce { get; set; }

        /// <summary>
        /// 商品英文詳情
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string ProductIntroduceEN { get; set; }

        /// <summary>
        /// 判斷商品庫存增或減
        /// </summary>
        [Required]
        public bool ProductCheckStock { get; set; }

        /// <summary>
        /// 商品預警值
        /// </summary>
        [Required]
        public int ProductStockWarning { get; set; }

    }
}