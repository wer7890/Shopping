﻿using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    public class LoginUserDto
    {
        /// <summary>
        /// 帳號
        /// </summary>
        [Required]
        [RegularExpression(@"^[A-Za-z0-9]{6,16}$", ErrorMessage = "Account Error")]
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [Required]
        [RegularExpression(@"^[A-Za-z0-9]{6,16}$", ErrorMessage = "Pwd Error")]
        public string Pwd { get; set; }
    }
}