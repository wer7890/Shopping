namespace ShoppingWeb.Response
{
    public class GetDefaultLowStockResponse : BaseResponse
    {
        /// <summary>
        /// 庫存量不足的資訊
        /// </summary>
        public string StockInsufficient { get; set; }

        /// <summary>
        /// 語言
        /// </summary>
        public string Language { get; set; }
    }
}