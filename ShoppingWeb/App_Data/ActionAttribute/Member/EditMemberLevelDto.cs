using System.ComponentModel.DataAnnotations;


namespace ShoppingWeb
{
    /// <summary>
    /// ToggleMemberLevel方法參數驗證
    /// </summary>
    public class EditMemberLevelDto
    {
        /// <summary>
        /// 會員ID
        /// </summary>
        [Required]
        public int MemberId { get; set; }

        /// <summary>
        /// 等級
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{1,3}$", ErrorMessage = "Level Error")]
        public int Level { get; set; }
    }
}