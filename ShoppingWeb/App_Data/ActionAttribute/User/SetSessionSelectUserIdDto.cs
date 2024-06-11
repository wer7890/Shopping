using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    public class SetSessionSelectUserIdDto
    {
        /// <summary>
        /// 管理員ID
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }
    }
}