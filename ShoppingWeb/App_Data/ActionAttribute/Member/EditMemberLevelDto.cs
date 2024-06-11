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
        [Range(1, int.MaxValue)]
        public int MemberId { get; set; }

        /// <summary>
        /// 等級
        /// </summary>
        [Required]
        [Range(0, 3)]
        public int Level { get; set; }
    }
}