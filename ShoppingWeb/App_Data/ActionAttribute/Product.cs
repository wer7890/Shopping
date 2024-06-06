using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// GetAllMemberData方法參數驗證
    /// </summary>
    public class GetAllProductDataAttribute
    {
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

    /// <summary>
    /// GetProductData方法參數驗證
    /// </summary>
    public class GetProductDataAttribute
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

    /// <summary>
    /// RemoveProduct方法參數驗證
    /// </summary>
    public class RemoveProductAttribute
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        public int ProductId { get; set; }
    }

    /// <summary>
    /// ToggleProductStatus方法參數驗證
    /// </summary>
    public class ToggleProductStatusAttribute
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        public int ProductId { get; set; }
    }

    /// <summary>
    /// SetSessionProductId方法參數驗證
    /// </summary>
    public class SetSessionProductIdAttribute
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        public int ProductId { get; set; }
    }

    /// <summary>
    /// EditProduct方法參數驗證
    /// </summary>
    public class EditProductAttribute
    {
        /// <summary>
        /// 商品價格
        /// </summary>
        [Required]
        public int ProductPrice { get; set; }

        /// <summary>
        /// 商品庫存
        /// </summary>
        [Required]
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