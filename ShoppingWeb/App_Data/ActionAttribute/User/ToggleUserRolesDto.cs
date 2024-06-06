using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    public class ToggleUserRolesDto
    {
        /// <summary>
        /// 管理員ID
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// 管理員身分
        /// </summary>
        [Required]
        public int Roles { get; set; }
    }
}