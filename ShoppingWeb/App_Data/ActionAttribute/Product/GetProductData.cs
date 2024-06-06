using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// GetProductData方法參數驗證
    /// </summary>
    public class GetProductData
    {
        /// <summary>
        /// 商品類型
        /// </summary>
        [Required]
        public int ProductCategory { get; set; }

        /// <summary>
        /// 商品名稱
        /// </summary>
        [MaxLength(100)]
        public string ProductName { get; set; }

        /// <summary>
        /// 商品小分類是否選擇全部
        /// </summary>
        [Required]
        public bool CheckAllMinorCategories { get; set; }

        /// <summary>
        /// 商品品牌是否選擇全部
        /// </summary>
        [Required]
        public bool CheckAllBrand { get; set; }

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