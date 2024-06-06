using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// AddMember方法參數驗證
    /// </summary>
    public class AddMemberDto
    {
        /// <summary>
        /// 帳號
        /// </summary>
        [Required]  //不能為null 
        [RegularExpression(@"^[A-Za-z0-9]{6,16}$", ErrorMessage = "Account Error")]
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [Required]
        [RegularExpression(@"^[A-Za-z0-9]{6,16}$", ErrorMessage = "Pwd Error")]
        public string Pwd { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        [Required]
        [RegularExpression(@"^[\u4E00-\u9FFF]{1,15}$", ErrorMessage = "Name Error")]
        public string Name { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9-]{8,10}$", ErrorMessage = "Birthday Error")]
        public string Birthday { get; set; }

        /// <summary>
        /// 手機
        /// </summary>
        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone Error")]
        public string Phone { get; set; }

        /// <summary>
        /// 信箱
        /// </summary>
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]{1,25}@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Address Error")]
        public string Email { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Required]
        [RegularExpression(@"^[\u4E00-\u9FFF0-9A-Za-z]{2,50}$", ErrorMessage = "Address Error")]
        public string Address { get; set; }
    }
}