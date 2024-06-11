﻿using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// SetSessionProductId方法參數驗證
    /// </summary>
    public class SetSessionProductIdDto
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }
    }
}