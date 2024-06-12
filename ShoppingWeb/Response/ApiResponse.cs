namespace ShoppingWeb
{
    public class ApiResponse
    {
        /// <summary>
        /// 存放Enum訊息
        /// </summary>
        public int Msg { get; set; }

        /// <summary>
        /// 存放請求成功後回傳的資料
        /// </summary>
        public object Data { get; set; }
    }
}