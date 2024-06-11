using Newtonsoft.Json.Linq;
using NLog;
using ShoppingWeb.Filters;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace ShoppingWeb.Controller
{
    [RoutePrefix("/api/Controller/product")]
    [RolesFilter((int)Roles.Product)]
    public class ProductController : BaseController
    {
        private string pubguid = "";

        /// <summary>
        /// 一開始顯示所有商品
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAllProductData")]
        public ApiResponse GetAllProductData([FromBody] GetAllProductDataDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getAllProductData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@PageNumber", dto.PageNumber));
                        cmd.Parameters.Add(new SqlParameter("@PageSize", dto.PageSize));
                        cmd.Parameters.Add(new SqlParameter("@beforePagesTotal", dto.BeforePagesTotal));
                        int languageNum = (HttpContext.Current.Request.Cookies["language"].Value == "TW") ? (int)Language.TW : (int)Language.EN;
                        cmd.Parameters.Add(new SqlParameter("@languageNum", languageNum));
                        cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                        cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;

                        SqlDataReader reader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
                        int totalPages = (int)Math.Ceiling((double)totalCount / dto.PageSize);  // 計算總頁數，Math.Ceiling向上進位取整數

                        var result = new
                        {
                            Data = ConvertDataTableToJson(dt),
                            TotalPages = totalPages
                        };

                        return new ApiResponse
                        {
                            Data = result,
                            Msg = (int)DatabaseOperationResult.Success
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new ApiResponse
                {
                    Data = null,
                    Msg = (int)DatabaseOperationResult.Error
                };
            }
        }

        /// <summary>
        /// 尋找特定商品
        /// </summary>
        /// <param name="sqlName"></param>
        /// <param name="sqlAdd"></param>
        /// <param name="searchName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetProductData")]
        public ApiResponse GetProductData([FromBody] GetProductDataDto dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_getSearchProductData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@category", dto.ProductCategory));
                        cmd.Parameters.Add(new SqlParameter("@name", dto.ProductName));
                        cmd.Parameters.Add(new SqlParameter("@allMinorCategories", dto.CheckAllMinorCategories));
                        cmd.Parameters.Add(new SqlParameter("@allBrand", dto.CheckAllBrand));
                        cmd.Parameters.Add(new SqlParameter("@PageNumber", dto.PageNumber));
                        cmd.Parameters.Add(new SqlParameter("@PageSize", dto.PageSize));
                        cmd.Parameters.Add(new SqlParameter("@beforePagesTotal", dto.BeforePagesTotal));
                        int languageNum = (HttpContext.Current.Request.Cookies["language"].Value == "TW") ? (int)Language.TW : (int)Language.EN;
                        cmd.Parameters.Add(new SqlParameter("@languageNum", languageNum));
                        cmd.Parameters.Add(new SqlParameter("@totalCount", SqlDbType.Int));
                        cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;

                        SqlDataReader reader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        int totalCount = int.Parse(cmd.Parameters["@totalCount"].Value.ToString());
                        int totalPages = (int)Math.Ceiling((double)totalCount / dto.PageSize);  // 計算總頁數，Math.Ceiling向上進位取整數

                        if (totalCount > 0)
                        {
                            var result = new
                            {
                                Data = ConvertDataTableToJson(dt),
                                TotalPages = totalPages
                            };

                            return new ApiResponse
                            {
                                Data = result,
                                Msg = (int)DatabaseOperationResult.Success
                            };
                        }
                        else
                        {
                            return new ApiResponse
                            {
                                Data = null,
                                Msg = (int)DatabaseOperationResult.Failure
                            };
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new ApiResponse
                {
                    Data = null,
                    Msg = (int)DatabaseOperationResult.Error
                };
            }
        }

        /// <summary>
        /// 刪除商品
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DelProduct")]
        public ApiResponse DelProduct([FromBody] DelProductDto dto)
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
                            return new ApiResponse
                            {
                                Data = null,
                                Msg = (int)DatabaseOperationResult.Success
                            };
                        }
                        else
                        {
                            return new ApiResponse
                            {
                                Data = null,
                                Msg = (int)DatabaseOperationResult.Failure
                            };
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new ApiResponse
                {
                    Data = null,
                    Msg = (int)DatabaseOperationResult.Error
                };
            }
        }

        /// <summary>
        /// 是否開放開關
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ToggleProductStatus")]
        public ApiResponse EditProductStatus([FromBody] EditProductStatusDto dto)
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

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        if (rowsAffected > 0)
                        {
                            StockInsufficientCache.SetIsEditStock(true);
                            return new ApiResponse
                            {
                                Data = null,
                                Msg = (int)DatabaseOperationResult.Success
                            };
                        }
                        else
                        {
                            return new ApiResponse
                            {
                                Data = null,
                                Msg = (int)DatabaseOperationResult.Failure
                            };
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new ApiResponse
                {
                    Data = null,
                    Msg = (int)DatabaseOperationResult.Error
                };
            }
        }

        /// <summary>
        /// 設定Session["productId"]
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SetSessionProductId")]
        public ApiResponse SetSessionProductId([FromBody] SetSessionProductIdDto dto)
        {
            HttpContext.Current.Session["productId"] = dto.ProductId;
            return new ApiResponse
            {
                Data = null,
                Msg = (int)DatabaseOperationResult.Success
            };
        }

        /// <summary>
        /// 回傳stockInsufficient變數
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDefaultLowStock")]
        public ApiResponse GetDefaultLowStock()
        {
            var result = new
            {
                Data = StockInsufficientCache.StockInsufficient,
                Language = HttpContext.Current.Request.Cookies["language"].Value
            };

            return new ApiResponse
            {
                Data = result,
                Msg = (int)DatabaseOperationResult.Success
            };
        }

        /// <summary>
        /// 新增商品和上傳圖片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadProduct")]
        public ApiResponse UploadProduct()
        {
            //檢查ProductImg資料夾是否存在，如果不存在就建立資料夾
            string checkTargetFolderPath = HttpContext.Current.Server.MapPath("~/ProductImg/");
            if (!Directory.Exists(checkTargetFolderPath))
            {
                Directory.CreateDirectory(checkTargetFolderPath);
            }

            // 檢查是否有上傳的檔案
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpPostedFile uploadedFile = HttpContext.Current.Request.Files[0];
                string fileName = Path.GetFileName(uploadedFile.FileName);
                int maxFileSize = 500 * 1024;

                // 檢查是否為圖片和圖片大小（限制大小為500KB）
                if (CheckFileExtension(Path.GetExtension(fileName)) && uploadedFile.ContentLength <= maxFileSize)
                {
                    string guid = Guid.NewGuid().ToString("D");  // 建立新的檔名，GUID每个x是0-9或a-f范围内一个32位十六进制数 8 4 4 4 12
                    string newFileName = guid + Path.GetExtension(fileName);
                    pubguid = newFileName;
                    string targetFolderPath = HttpContext.Current.Server.MapPath("~/ProductImg/" + newFileName);  // 指定儲存路徑

                    if (File.Exists(Path.Combine(targetFolderPath, fileName)))  // 檢查檔案是否已存在於目標資料夾中
                    {
                        return new ApiResponse
                        {
                            Data = null,
                            Msg = (int)UserStatus.InputError
                        };
                    }
                    else
                    {
                        uploadedFile.SaveAs(targetFolderPath);
                        string productName = HttpContext.Current.Request.Form["productName"];
                        string productNameEN = HttpContext.Current.Request.Form["productNameEN"];
                        string productCategory = HttpContext.Current.Request.Form["productCategory"];
                        string productPrice = HttpContext.Current.Request.Form["productPrice"];
                        string productStock = HttpContext.Current.Request.Form["productStock"];
                        string productIsOpen = HttpContext.Current.Request.Form["productIsOpen"];
                        string productIntroduce = HttpContext.Current.Request.Form["productIntroduce"];
                        string productIntroduceEN = HttpContext.Current.Request.Form["productIntroduceEN"];
                        string productStockWarning = HttpContext.Current.Request.Form["productStockWarning"];

                        ApiResponse result = AddProduct(productName, productNameEN, productCategory, productPrice, productStock, productIsOpen, productIntroduce, productIntroduceEN, productStockWarning);

                        return result;
                    }
                }
                else
                {
                    return new ApiResponse
                    {
                        Data = null,
                        Msg = (int)UserStatus.InputError
                    };
                }
            }
            else
            {
                return new ApiResponse
                {
                    Data = null,
                    Msg = (int)UserStatus.InputError
                };
            }
        }

        /// <summary>
        /// DB新增商品
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="productCategory"></param>
        /// <param name="productPrice"></param>
        /// <param name="productStock"></param>
        /// <param name="productIsOpen"></param>
        /// <param name="productIntroduce"></param>
        /// <returns></returns>
        [NonAction]
        public ApiResponse AddProduct(string productName, string productNameEN, string productCategory, string productPrice, string productStock, string productIsOpen, string productIntroduce, string productIntroduceEN, string productStockWarning)
        {

            if (!AddProductSpecialChar(productName, productNameEN, productCategory, productIsOpen, productIntroduce, productIntroduceEN, productPrice, productStock, productStockWarning))
            {
                return new ApiResponse
                {
                    Data = null,
                    Msg = (int)UserStatus.InputError
                };
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_addProductData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.Parameters.Add(new SqlParameter("@name", productName));
                        cmd.Parameters.Add(new SqlParameter("@nameEN", productNameEN));
                        cmd.Parameters.Add(new SqlParameter("@category", productCategory));
                        cmd.Parameters.Add(new SqlParameter("@img", pubguid));
                        cmd.Parameters.Add(new SqlParameter("@price", productPrice));
                        cmd.Parameters.Add(new SqlParameter("@stock", productStock));
                        cmd.Parameters.Add(new SqlParameter("@isOpen", productIsOpen));
                        cmd.Parameters.Add(new SqlParameter("@introduce", productIntroduce));
                        cmd.Parameters.Add(new SqlParameter("@introduceEN", productIntroduceEN));
                        cmd.Parameters.Add(new SqlParameter("@warningValue", productStockWarning));
                        cmd.Parameters.Add(new SqlParameter("@owner", ((UserInfo)HttpContext.Current.Session["userInfo"]).UserId));
                        int result = (int)cmd.ExecuteScalar();

                        if (result != 1)
                        {
                            string imagePath = HttpContext.Current.Server.MapPath("~/ProductImg/" + pubguid);
                            File.Delete(imagePath);
                            return new ApiResponse
                            {
                                Data = null,
                                Msg = (int)DatabaseOperationResult.Failure
                            };
                        }

                        StockInsufficientCache.SetIsEditStock(true);
                        return new ApiResponse
                        {
                            Data = null,
                            Msg = (int)DatabaseOperationResult.Success
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                string imagePath = HttpContext.Current.Server.MapPath("~/ProductImg/" + pubguid);
                File.Delete(imagePath);
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new ApiResponse
                {
                    Data = null,
                    Msg = (int)DatabaseOperationResult.Error
                };
            }
        }

        /// <summary>
        /// 判斷是否為圖片
        /// </summary>
        /// <param name="imgName"></param>
        /// <returns></returns>
        [NonAction]
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
        [NonAction]
        public bool AddProductSpecialChar(string productName, string productNameEN, string productCategory, string productIsOpen, string productIntroduce, string productIntroduceEN, string productPrice, string productStock, string productStockWarning)
        {
            bool cheackName = Regex.IsMatch(productName, @"^.{1,40}$");
            bool cheackNameEN = Regex.IsMatch(productNameEN, @"^[^\u4e00-\u9fa5]{1,100}$");
            bool cheackCategory = Regex.IsMatch(productCategory, @"^.{6,}$");
            bool cheackIsOpen = Regex.IsMatch(productIsOpen, @"^.{1,}$");
            bool cheackIntroduce = Regex.IsMatch(productIntroduce, @"^.{1,500}$");
            bool cheackIntroduceEN = Regex.IsMatch(productIntroduceEN, @"^[^\u4e00-\u9fa5]{1,1000}$");
            bool cheackPrice = Regex.IsMatch(productPrice, @"^[0-9]{1,7}$");
            bool cheackStock = Regex.IsMatch(productStock, @"^[0-9]{1,7}$");
            bool cheackStockWarning = Regex.IsMatch(productStockWarning, @"^[0-9]{1,7}$");

            return cheackName && cheackNameEN && cheackCategory && cheackIsOpen && cheackIntroduce && cheackIntroduceEN && cheackPrice && cheackStock && cheackStockWarning;
        }


        /// <summary>
        /// 設定跳轉道編輯商品頁面時，input裡面的預設值
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetProductDataForEdit")]
        public ApiResponse GetProductDataForEdit()
        {
            try
            {
                string sessionProductId = HttpContext.Current.Session["productId"].ToString();

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
                                ProductName = dt.Rows[0]["f_nameTW"],
                                ProductNameEN = dt.Rows[0]["f_nameEN"],
                                ProductCategory = dt.Rows[0]["f_category"],
                                ProductPrice = dt.Rows[0]["f_price"],
                                ProductStock = dt.Rows[0]["f_stock"],
                                ProductOwner = dt.Rows[0]["f_createdUser"],
                                ProductCreatedOn = dt.Rows[0]["f_createdTime"].ToString(),
                                ProductIntroduce = dt.Rows[0]["f_introduceTW"],
                                ProductIntroduceEN = dt.Rows[0]["f_introduceEN"],
                                ProductImg = dt.Rows[0]["f_img"],
                                ProductStockWarning = dt.Rows[0]["f_warningValue"]
                            };

                            return new ApiResponse
                            {
                                Data = productObject,
                                Msg = (int)DatabaseOperationResult.Success
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new ApiResponse
                {
                    Data = null,
                    Msg = (int)DatabaseOperationResult.Error
                };
            }

        }

        /// <summary>
        /// 更改商品資料
        /// </summary>
        /// <param name="productPrice"></param>
        /// <param name="productStock"></param>
        /// <param name="productIntroduce"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditProduct")]
        public ApiResponse EditProduct([FromBody] EditProductDto dto)
        {
            try
            {
                string sessionProductId = HttpContext.Current.Session["productId"].ToString();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("pro_sw_editProductData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        cmd.Parameters.Add(new SqlParameter("@productId", sessionProductId));
                        cmd.Parameters.Add(new SqlParameter("@price", dto.ProductPrice));
                        cmd.Parameters.Add(new SqlParameter("@stock", dto.ProductStock));
                        cmd.Parameters.Add(new SqlParameter("@introduce", dto.ProductIntroduce));
                        cmd.Parameters.Add(new SqlParameter("@introduceEN", dto.ProductIntroduceEN));
                        cmd.Parameters.Add(new SqlParameter("@warningValue", dto.ProductStockWarning));
                        cmd.Parameters.Add(new SqlParameter("@checkStoct", dto.ProductCheckStock));

                        int rowsAffected = (int)cmd.ExecuteScalar();

                        if (rowsAffected > 0)
                        {
                            StockInsufficientCache.SetIsEditStock(true);
                            return new ApiResponse
                            {
                                Data = null,
                                Msg = (int)DatabaseOperationResult.Success
                            };
                        }
                        else
                        {
                            return new ApiResponse
                            {
                                Data = null,
                                Msg = (int)DatabaseOperationResult.Failure
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex + " 帳號: " + ((UserInfo)HttpContext.Current.Session["userInfo"]).Account);
                return new ApiResponse
                {
                    Data = null,
                    Msg = (int)DatabaseOperationResult.Error
                };
            }
        }

    }
}