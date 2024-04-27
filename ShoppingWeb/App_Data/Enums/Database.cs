namespace ShoppingWeb.Ajax
{
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
        Failure = 101,

        /// <summary>
        /// 發生發生內部錯誤，請看日誌
        /// </summary>
        Error = 102
    }
}