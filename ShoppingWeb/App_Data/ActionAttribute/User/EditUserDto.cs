using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    public class EditUserDto
    {
        /// <summary>
        /// 管理員ID
        /// </summary>
        //[Required]
        //[Range(1, int.MaxValue)]
        public int UserId { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        //[Required]
        //[RegularExpression(@"^[A-Za-z0-9]{6,16}$")]
        public string Pwd { get; set; }
    }
}