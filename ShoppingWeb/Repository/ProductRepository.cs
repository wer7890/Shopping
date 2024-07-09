using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace ShoppingWeb.Repository
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        /// <summary>
        /// 刪除商品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, int?) DelProduct(DelProductDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_delProductData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@productId", dto.ProductId));

                        //設定預存程序輸出參數的名稱與資料類型
                        cmd.Parameters.Add(new SqlParameter("@deletedProductImg", SqlDbType.NVarChar, 50));
                        //設定參數名稱的傳遞方向
                        cmd.Parameters["@deletedProductImg"].Direction = ParameterDirection.Output;

                        int r = (int)cmd.ExecuteScalar();
                        //取得預存程序的輸出參數值
                        string deletedProductImg = cmd.Parameters["@deletedProductImg"].Value.ToString();

                        if (r > 0)
                        {
                            string imagePath = HttpContext.Current.Server.MapPath("~/ProductImg/" + deletedProductImg);
                            File.Delete(imagePath);
                            StockInsufficientCache.SetIsEditStock(true);
                        }

                        return (null, (int)cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }

        /// <summary>
        /// 是否開放開關
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, int?) EditProductStatus(EditProductStatusDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editProductStatus", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@productId", dto.ProductId));

                        return (null, (int)cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }

        /// <summary>
        /// 更改商品資料
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public (Exception, int?) EditProduct(EditProductDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editProductData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        cmd.Parameters.Add(new SqlParameter("@productId", dto.ProductId));
                        cmd.Parameters.Add(new SqlParameter("@price", dto.ProductPrice));
                        cmd.Parameters.Add(new SqlParameter("@stock", dto.ProductStock));
                        cmd.Parameters.Add(new SqlParameter("@introduce", dto.ProductIntroduce));
                        cmd.Parameters.Add(new SqlParameter("@introduceEN", dto.ProductIntroduceEN));
                        cmd.Parameters.Add(new SqlParameter("@warningValue", dto.ProductStockWarning));
                        cmd.Parameters.Add(new SqlParameter("@checkStoct", dto.ProductCheckStock));

                        int result = (int)cmd.ExecuteScalar();

                        if (result > 0)
                        {
                            StockInsufficientCache.SetIsEditStock(true);
                        }

                        return (null, result);
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex, null);
            }
        }

    }
}