using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// EditMemberStatusDto方法參數驗證
    /// </summary>
    public class EditMemberStatusDto
    {
        /// <summary>
        /// 會員ID
        /// </summary>
        [Required]
        public int MemberId { get; set; }
    }
}