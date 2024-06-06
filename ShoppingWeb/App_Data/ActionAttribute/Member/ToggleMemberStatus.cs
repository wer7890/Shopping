using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// ToggleProductStatus方法參數驗證
    /// </summary>
    public class ToggleMemberStatus
    {
        /// <summary>
        /// 會員ID
        /// </summary>
        [Required]
        public int MemberId { get; set; }
    }
}