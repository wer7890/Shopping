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
        [Range(1, int.MaxValue)]
        public int MemberId { get; set; }
    }
}