using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingWeb.Models
{
    public class Enums
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
            AccessDenied,

            /// <summary>
            /// 輸入值錯誤
            /// </summary>
            InputError
        }

        /// <summary>
        /// 操作資料庫的結果
        /// </summary>
        public enum DatabaseOperationResult
        {   
            /// <summary>
            /// 更改成功
            /// </summary>
            Success = 100,

            /// <summary>
            /// 更改失敗
            /// </summary>
            Failure,

            /// <summary>
            /// 發生錯誤
            /// </summary>
            Error
        }
        
    }
}