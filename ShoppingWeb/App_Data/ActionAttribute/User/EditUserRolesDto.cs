using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    public class EditRolesDto
    {
        /// <summary>
        /// 管理員ID
        /// </summary>
        //[Required]
        //[Range(1, int.MaxValue)]
        public int UserId { get; set; }

        /// <summary>
        /// 管理員身分
        /// </summary>
        //[Required]
        //[Range(1, 3)]
        public int Roles { get; set; }
    }
}