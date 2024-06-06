using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// GetAllMemberData方法參數驗證
    /// </summary>
    public class GetAllProductDataDto
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
}