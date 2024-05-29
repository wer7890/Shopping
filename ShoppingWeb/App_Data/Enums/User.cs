namespace ShoppingWeb
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
        InputError = 2,

        /// <summary>
        /// 權限驗證異常
        /// </summary>
        validationException = 3
    }
}