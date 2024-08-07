﻿using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb
{
    /// <summary>
    /// GetOrderDetailsData方法參數驗證
    /// </summary>
    public class GetOrderDetailsDataDto
    {
        /// <summary>
        /// 訂單ID
        /// </summary>
        //[Required]
        //[Range(1, int.MaxValue)]
        public int OrderId { get; set; }
    }
}