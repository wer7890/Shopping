using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    public class AddUserDto
    {
        /// <summary>
        /// 帳號
        /// </summary>
        //[Required]
        //[RegularExpression(@"^[A-Za-z0-9]{6,16}$")]
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        //[Required]
        //[RegularExpression(@"^[A-Za-z0-9]{6,16}$")]
        public string Pwd { get; set; }

        /// <summary>
        /// 身分
        /// </summary>
        //[Required]
        //[Range(1, 3)]
        public int Roles { get; set; }
    }
}