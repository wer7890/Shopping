using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;

namespace ShoppingWeb.Ajax
{
    public partial class RenewProductHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 設定跳轉道編輯商品頁面時，input裡面的預設值
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static object GetProductDataForEdit()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                string sessionProductId = HttpContext.Current.Session["productId"] as string;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("getProductData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@productId", sessionProductId));

                        using (SqlDataAdapter sqlData = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sqlData.Fill(dt);

                            // 構建包含數據的匿名對象
                            var productObject = new
                            {
                                ProductId = dt.Rows[0]["f_id"],
                                ProductName = dt.Rows[0]["f_productName"],
                                ProductCategory = dt.Rows[0]["f_productCategory"],
                                ProductPrice = dt.Rows[0]["f_productPrice"],
                                ProductStock = dt.Rows[0]["f_productStock"],
                                //ProductIsOpen = dt.Rows[0]["f_productIsOpen"],
                                ProductOwner = dt.Rows[0]["f_productOwner"],
                                ProductCreatedOn = dt.Rows[0]["f_productCreatedOn"].ToString(),
                                ProductIntroduce = dt.Rows[0]["f_productIntroduce"],
                                ProductImg = dt.Rows[0]["f_productImg"]
                            };

                            return productObject;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                return ex;
            }

        }

        /// <summary>
        /// 更改商品資料
        /// </summary>
        /// <param name="productPrice"></param>
        /// <param name="productStock"></param>
        /// <param name="productIntroduce"></param>
        /// <returns></returns>
        [WebMethod]
        public static string EditProduct(int productPrice, int productStock, string productIntroduce)
        {
            if (SpecialChar(productPrice, productStock, productIntroduce))
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                    string sessionProductId = HttpContext.Current.Session["productId"] as string;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("editProduct", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();

                            cmd.Parameters.Add(new SqlParameter("@productId", sessionProductId));
                            cmd.Parameters.Add(new SqlParameter("@productPrice", productPrice));
                            cmd.Parameters.Add(new SqlParameter("@productStock", productStock));
                            cmd.Parameters.Add(new SqlParameter("@productIntroduce", productIntroduce));

                            int rowsAffected = (int)cmd.ExecuteScalar();

                            return (rowsAffected > 0) ? "修改成功" : "修改失敗";
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                    return "錯誤";
                }
            }
            else
            {
                return "名稱和密碼不能含有非英文和數字且長度應在6到16之間且腳色不能為空";
            }

        }

        /// <summary>
        /// 判斷輸入值
        /// </summary>
        /// <param name="productPrice"></param>
        /// <param name="productStock"></param>
        /// <param name="productIntroduce"></param>
        /// <returns></returns>
        public static bool SpecialChar(int productPrice, int productStock, string productIntroduce)
        {
            bool cheackIntroduce = Regex.IsMatch(productIntroduce, @"^.{1,500}$");
            bool cheackPrice = Regex.IsMatch(productPrice.ToString(), @"^[0-9]{1,7}$");
            bool cheackStock = Regex.IsMatch(productStock.ToString(), @"^[0-9]{1,7}$");

            if (cheackIntroduce && cheackPrice && cheackStock)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}