using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingWeb
{
    public class StockInsufficientCache
    {
        private static string _stockInsufficient;
        private static bool _isEditStock = true;

        /// <summary>
        /// 庫存量不足的商品資訊
        /// </summary>
        public static string StockInsufficient
        {
            get { return _stockInsufficient; }
            private set { _stockInsufficient = value; }
        }

        /// <summary>
        /// 是否更改過庫存量
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
            StockInsufficient = value;
        }

        /// <summary>
        /// 設置是否更改庫存的資訊
        /// </summary>
        /// <param name="value"></param>
        public static void SetIsEditStock(bool value)
        {
            IsEditStock = value;
        }
    }
}