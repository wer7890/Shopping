using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingWeb.Ajax
{
    /// <summary>
    /// 判斷使用者狀態
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// 重複登入
        /// </summary>
        DuplicateLogin = 0,

        /// <summary>
        /// 權限不足
        /// </summary>
        AccessDenied = 1,

        /// <summary>
        /// 輸入值錯誤
        /// </summary>
        InputError = 2
    }
}