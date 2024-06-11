using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// EditOrder方法參數驗證
    /// </summary>
    public class GetReturnOrderDataDto
    {
        /// <summary>
        /// 現在頁數
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; }

        /// <summary>
        /// 每頁資料筆數
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int PageSize { get; set; }

        /// <summary>
        /// 之前頁數
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int BeforePagesTotal { get; set; }
    }
}