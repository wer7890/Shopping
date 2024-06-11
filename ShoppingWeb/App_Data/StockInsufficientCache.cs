namespace ShoppingWeb
{
    public class StockInsufficientCache
    {
        private static string _stockInsufficient = string.Empty;  //設成空字串
        private static bool _isEditStock = true;

        /// <summary>
        /// 獲取庫存量不足的商品資訊（只讀）
        /// </summary>
        public static string StockInsufficient
        {
            get { return _stockInsufficient; }
            private set { _stockInsufficient = value; }
        }

        /// <summary>
        /// 獲取是否更改過庫存量的狀態（只讀）
        /// </summary>
        public static bool IsEditStock
        {
            get { return _isEditStock; }
            private set { _isEditStock = value; }
        }

        /// <summary>
        /// 設置商品庫存不足的資訊
        /// </summary>
        /// <param name="value"></param>
        public static void SetStockInsufficient(string value)
        {
            _stockInsufficient = value;
        }

        /// <summary>
        /// 設置是否更改庫存的資訊
        /// </summary>
        /// <param name="value"></param>
        public static void SetIsEditStock(bool value)
        {
            _isEditStock = value;
        }
    }
}