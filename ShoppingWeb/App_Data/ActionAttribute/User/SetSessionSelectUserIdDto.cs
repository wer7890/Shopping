using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    public class SetSessionSelectUserIdDto
    {
        /// <summary>
        /// 管理員ID
        /// </summary>
        [Required]
        public int UserId { get; set; }
    }
}