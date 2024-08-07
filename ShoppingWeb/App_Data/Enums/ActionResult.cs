﻿namespace ShoppingWeb
{
    /// <summary>
    /// 操作的結果
    /// </summary>
    public enum ActionResult
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
        InputError = 2,

        /// <summary>
        /// 登入正確
        /// </summary>
        LoginCorrect = 3,


        /// <summary>
        /// 更改成功
        /// </summary>
        Success = 100,

        /// <summary>
        /// 更改失敗
        /// </summary>
        Failure = 101,

        /// <summary>
        /// 發生發生內部錯誤，請看日誌
        /// </summary>
        Error = 102
    }
}