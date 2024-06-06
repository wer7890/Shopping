using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// GetAllMemberData方法參數驗證
    /// </summary>
    public class GetAllMemberDataAttribute
    {
        /// <summary>
        /// 現在頁數
        /// </summary>
        [Required]
        public int PageNumber { get; set; }

        /// <summary>
        /// 每頁資料筆數
        /// </summary>
        [Required]
        public int PageSize { get; set; }

        /// <summary>
        /// 之前頁數
        /// </summary>
        [Required]
        public int BeforePagesTotal { get; set; }
    }

    /// <summary>
    /// ToggleProductStatus方法參數驗證
    /// </summary>
    public class ToggleMemberStatusAttribute
    {
        /// <summary>
        /// 會員ID
        /// </summary>
        [Required]
        public int MemberId { get; set; }
    }

    /// <summary>
    /// ToggleMemberLevel方法參數驗證
    /// </summary>
    public class ToggleMemberLevelAttribute
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

    /// <summary>
    /// AddMember方法參數驗證
    /// </summary>
    public class AddMemberAttribute
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