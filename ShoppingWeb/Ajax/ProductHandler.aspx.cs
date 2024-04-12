using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.Services;
using System.IO;

namespace ShoppingWeb.Ajax
{
    public partial class ProductHandler : BasePage
    {
        private static string pubguid = "";

        /// <summary>
        /// 商品系統所要求的權限
        /// </summary>
        private const int PERMITTED_PRODUCT_ROLES = 3;

        public ProductHandler()
        {
            //判斷權限是否可使用該功能
            if (!CheckRoles(PERMITTED_PRODUCT_ROLES))
            {

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Utility.CheckDuplicateLogin())
            {
                Response.Write("重複登入");
            }
            else if (!Utility.CheckRoles(PERMITTED_PRODUCT_ROLES))
            {
                Response.Write("權限不足");
            }
            else
            {
                //檢查ProductImg是否存在，如果不存在就建立資料夾
                string checkTargetFolderPath = Server.MapPath("~/ProductImg/");
                if (!Directory.Exists(checkTargetFolderPath))
                {
                    Directory.CreateDirectory(checkTargetFolderPath);
                }

                // 檢查是否有上傳的檔案
                if (Request.HttpMethod == "POST" && Request.Files.Count > 0)
                {
                    HttpPostedFile uploadedFile = Request.Files[0];
                    string fileName = Path.GetFileName(uploadedFile.FileName);
                    int maxFileSize = 500 * 1024;

                    // 檢查是否為圖片和圖片大小（限制大小為500KB）
                    if (CheckFileExtension(Path.GetExtension(fileName)) && !(uploadedFile.ContentLength > maxFileSize))
                    {
                        string guid = Guid.NewGuid().ToString("D");  // 建立新的檔名，GUID每个x是0-9或a-f范围内一个32位十六进制数 8 4 4 4 12

                        string newFileName = guid + Path.GetExtension(fileName);
                        pubguid = newFileName;
                        string targetFolderPath = Server.MapPath("~/ProductImg/" + newFileName);  // 指定儲存路徑

                        if (File.Exists(Path.Combine(targetFolderPath, fileName)))   // 檢查檔案是否已存在於目標資料夾中
                        {
                            Response.Write("上傳的檔案名稱已存在");
                        }
                        else
                        {
                            uploadedFile.SaveAs(targetFolderPath);
                            string productName = Request.Form["productName"];
                            string productCategory = Request.Form["productCategory"];
                            string productPrice = Request.Form["productPrice"];
                            string productStock = Request.Form["productStock"];
                            string productIsOpen = Request.Form["productIsOpen"];
                            string productIntroduce = Request.Form["productIntroduce"];
                            Response.Write(AddProduct(productName, productCategory, productPrice, productStock, productIsOpen, productIntroduce));

                        }
                    }
                    else
                    {
                        Response.Write("上傳的不是圖片或圖片大小超過限制（最大500KB）");
                    }
                }
                else
                {
                    Response.Write("未選擇要上傳的檔案");
                }
            }
        }

        /// <summary>
        /// 一開始顯示所有商品
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static object GetAllProductData(int pageNumber, int pageSize)
        {

            if (!Utility.CheckDuplicateLogin())
            {
                return "重複登入";
            }

            if (!Utility.CheckRoles(PERMITTED_PRODUCT_ROLES))
            {
                return "權限不足";
            }

            string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pro_sw_getAllProductData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
                    cmd.Parameters.Add(new SqlParameter("@PageSize", pageSize));
                    cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                    cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;

                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
                    int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);  // 計算總頁數，Math.Ceiling向上進位取整數

                    var result = new
                    {
                        Data = Utility.ConvertDataTableToJson(dt),
                        TotalPages = totalPages
                    };

                    return result;
                }
            }
        }

        /// <summary>
        /// 尋找特定商品
        /// </summary>
        /// <param name="sqlName"></param>
        /// <param name="sqlAdd"></param>
        /// <param name="searchName"></param>
        /// <returns></returns>
        [WebMethod]
        public static object GetProductData(string productCategory, string productName, bool checkAllMinorCategories, bool checkAllBrand, int pageNumber, int pageSize)
        {

            if (!Utility.CheckDuplicateLogin())
            {
                return "重複登入";
            }

            if (!Utility.CheckRoles(PERMITTED_PRODUCT_ROLES))
            {
                return "權限不足";
            }

            string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("pro_sw_getSearchProductData", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.Add(new SqlParameter("@category", productCategory));
                    cmd.Parameters.Add(new SqlParameter("@name", productName));
                    cmd.Parameters.Add(new SqlParameter("@allMinorCategories", checkAllMinorCategories));
                    cmd.Parameters.Add(new SqlParameter("@allBrand", checkAllBrand));
                    cmd.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
                    cmd.Parameters.Add(new SqlParameter("@PageSize", pageSize));
                    cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                    cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;

                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
                    int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);  // 計算總頁數，Math.Ceiling向上進位取整數

                    if (totalCount > 0)
                    {
                        var result = new
                        {
                            Data = Utility.ConvertDataTableToJson(dt),
                            TotalPages = totalPages
                        };
                        return result;
                    }
                    else
                    {
                        return "null";
                    }

                }
            }
        }

        /// <summary>
        /// 刪除商品
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string RemoveProduct(string productId)
        {

            if (!Utility.CheckDuplicateLogin())
            {
                return "重複登入";
            }

            if (!Utility.CheckRoles(PERMITTED_PRODUCT_ROLES))
            {
                return "權限不足";
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_delProductData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@productId", productId));

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
                            return "刪除成功";
                        }
                        else
                        {
                            return "刪除失敗";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = new Logger();
                logger.LogException(ex);
                return "錯誤";
            }
        }

        /// <summary>
        /// 是否開放開關
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [WebMethod]
        public static string ToggleProductStatus(string productId)
        {

            if (!Utility.CheckDuplicateLogin())
            {
                return "重複登入";
            }

            if (!Utility.CheckRoles(PERMITTED_PRODUCT_ROLES))
            {
                return "權限不足";
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editProductStatus", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@productId", productId));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return (rowsAffected > 0) ? "更改成功" : "更改失敗";

                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = new Logger();
                logger.LogException(ex);
                return "錯誤";
            }
        }

        /// <summary>
        /// 設定Session["productId"]
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool SetSessionProductId(string productId)
        {
            HttpContext.Current.Session["productId"] = productId;
            return true;
        }



        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="productCategory"></param>
        /// <param name="productPrice"></param>
        /// <param name="productStock"></param>
        /// <param name="productIsOpen"></param>
        /// <param name="productIntroduce"></param>
        /// <returns></returns>
        public static string AddProduct(string productName, string productCategory, string productPrice, string productStock, string productIsOpen, string productIntroduce)
        {

            if (!AddProductSpecialChar(productName, productCategory, productIsOpen, productIntroduce, productPrice, productStock))
            {
                return "輸入值不符合格式";
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_addProductData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@name", productName));
                        cmd.Parameters.Add(new SqlParameter("@category", productCategory));
                        cmd.Parameters.Add(new SqlParameter("@img", pubguid));
                        cmd.Parameters.Add(new SqlParameter("@price", productPrice));
                        cmd.Parameters.Add(new SqlParameter("@stock", productStock));
                        cmd.Parameters.Add(new SqlParameter("@isOpen", productIsOpen));
                        cmd.Parameters.Add(new SqlParameter("@introduce", productIntroduce));
                        cmd.Parameters.Add(new SqlParameter("@owner", HttpContext.Current.Session["userId"]));
                        string result = cmd.ExecuteScalar().ToString();

                        if (result != "1")
                        {
                            string imagePath = HttpContext.Current.Server.MapPath("~/ProductImg/" + pubguid);
                            File.Delete(imagePath);
                        }

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                string imagePath = HttpContext.Current.Server.MapPath("~/ProductImg/" + pubguid);
                File.Delete(imagePath);
                Logger logger = new Logger();
                logger.LogException(ex);
                return "發生內部錯誤: " + ex.Message;
            }
        }

        /// <summary>
        /// 判斷是否為圖片
        /// </summary>
        /// <param name="imgName"></param>
        /// <returns></returns>
        public bool CheckFileExtension(string imgName)
        {
            bool checkFile = Regex.IsMatch(imgName, @"(\.jpg|\.png)$", RegexOptions.IgnoreCase);
            return checkFile;
        }

        /// <summary>
        /// 判斷輸入值
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="productCategory"></param>
        /// <param name="productIsOpen"></param>
        /// <param name="productIntroduce"></param>
        /// <param name="productPrice"></param>
        /// <param name="productStock"></param>
        /// <returns></returns>
        public static bool AddProductSpecialChar(string productName, string productCategory, string productIsOpen, string productIntroduce, string productPrice, string productStock)
        {
            bool cheackName = Regex.IsMatch(productName, @"^.{1,40}$");
            bool cheackCategory = Regex.IsMatch(productCategory, @"^.{6,}$");
            bool cheackIsOpen = Regex.IsMatch(productIsOpen, @"^.{1,}$");
            bool cheackIntroduce = Regex.IsMatch(productIntroduce, @"^.{1,500}$");
            bool cheackPrice = Regex.IsMatch(productPrice, @"^[0-9]{1,7}$");
            bool cheackStock = Regex.IsMatch(productStock, @"^[0-9]{1,7}$");

            return cheackName && cheackCategory && cheackIsOpen && cheackIntroduce && cheackPrice && cheackStock;
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
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getProductData", con))
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
                                ProductName = dt.Rows[0]["f_name"],
                                ProductCategory = dt.Rows[0]["f_category"],
                                ProductPrice = dt.Rows[0]["f_price"],
                                ProductStock = dt.Rows[0]["f_stock"],
                                ProductOwner = dt.Rows[0]["f_createdUser"],
                                ProductCreatedOn = dt.Rows[0]["f_createdTime"].ToString(),
                                ProductIntroduce = dt.Rows[0]["f_introduce"],
                                ProductImg = dt.Rows[0]["f_img"]
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
        public static string EditProduct(int productPrice, int productStock, string productIntroduce, string productCheckStock)
        {

            if (!Utility.CheckDuplicateLogin())
            {
                return "重複登入";
            }

            if (!Utility.CheckRoles(PERMITTED_PRODUCT_ROLES))
            {
                return "權限不足";
            }

            if (!RenewProductSpecialChar(productPrice, productStock, productIntroduce))
            {
                return "輸入值不符合格式";
            }

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                string sessionProductId = HttpContext.Current.Session["productId"] as string;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editProductData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        cmd.Parameters.Add(new SqlParameter("@productId", sessionProductId));
                        cmd.Parameters.Add(new SqlParameter("@price", productPrice));
                        cmd.Parameters.Add(new SqlParameter("@stock", productStock));
                        cmd.Parameters.Add(new SqlParameter("@introduce", productIntroduce));
                        cmd.Parameters.Add(new SqlParameter("@checkStoct", productCheckStock));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        return (rowsAffected > 0) ? "修改成功" : "修改失敗，庫存量不能小於0";
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = new Logger();
                logger.LogException(ex);
                return "錯誤";
            }
        }

        /// <summary>
        /// 判斷輸入值
        /// </summary>
        /// <param name="productPrice"></param>
        /// <param name="productStock"></param>
        /// <param name="productIntroduce"></param>
        /// <returns></returns>
        public static bool RenewProductSpecialChar(int productPrice, int productStock, string productIntroduce)
        {
            bool cheackIntroduce = Regex.IsMatch(productIntroduce, @"^.{1,500}$");
            bool cheackPrice = Regex.IsMatch(productPrice.ToString(), @"^[0-9]{1,7}$");
            bool cheackStock = Regex.IsMatch(productStock.ToString(), @"^[0-9]{1,7}$");

            return cheackIntroduce && cheackPrice && cheackStock;
        }
    }
}