using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;

namespace ShoppingWeb.Ajax
{
    public partial class AddProductHandler : System.Web.UI.Page
    {
        private static string pubguid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // 檢查是否有上傳的檔案
            if (Request.HttpMethod == "POST" && Request.Files.Count > 0)
            {
                HttpPostedFile uploadedFile = Request.Files[0];
                string fileName = Path.GetFileName(uploadedFile.FileName);

                if (CheckFileExtension(Path.GetExtension(fileName)))
                {
                    
                    // 建立新的檔名，GUID每个x是0-9或a-f范围内一个32位十六进制数 8 4 4 4 12
                    string guid = Guid.NewGuid().ToString("D");   
                    
                    string newFileName = guid + Path.GetExtension(fileName);
                    pubguid = newFileName;

                    // 指定儲存路徑
                    string targetFolderPath = Server.MapPath("~/ProductImg/" + newFileName);

                    // 檢查檔案是否已存在於目標資料夾中
                    if (File.Exists(Path.Combine(targetFolderPath, fileName)))
                    {
                        Response.Write("上傳的檔案名稱已存在");
                    }
                    else
                    {
                        uploadedFile.SaveAs(targetFolderPath);

                        Response.Write("圖片上傳成功");
                    }

                }
                else
                {
                    Response.Write("上傳的檔案不是圖片");
                }
            }
            else
            {
                Response.Write("未選擇要上傳的檔案");
            }
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
        [WebMethod]
        public static string AddProduct(string productName, string productCategory, int productPrice, int productStock, string productIsOpen, string productIntroduce)
        {

            if (SpecialChar(productName, productCategory, productIsOpen, productIntroduce, productPrice, productStock))
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("insertProduct", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.Add(new SqlParameter("@productName", productName));
                            cmd.Parameters.Add(new SqlParameter("@productCategory", productCategory));
                            cmd.Parameters.Add(new SqlParameter("@productImg", pubguid));
                            cmd.Parameters.Add(new SqlParameter("@productPrice", productPrice));
                            cmd.Parameters.Add(new SqlParameter("@productStock", productStock));
                            cmd.Parameters.Add(new SqlParameter("@productIsOpen", productIsOpen));
                            cmd.Parameters.Add(new SqlParameter("@productIntroduce", productIntroduce));
                            cmd.Parameters.Add(new SqlParameter("@productOwner", HttpContext.Current.Session["userName"]));
                            string result = cmd.ExecuteScalar().ToString();

                            return result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                    return "發生內部錯誤: " + ex.Message;
                }
            }
            else
            {
                return "輸入數值錯誤";
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
        public static bool SpecialChar(string productName, string productCategory, string productIsOpen, string productIntroduce, int productPrice, int productStock)
        { 
            bool cheackName = Regex.IsMatch(productName, @"^.{1,40}$");
            bool cheackCategory = Regex.IsMatch(productCategory, @"^.{1,}$");
            bool cheackIsOpen = Regex.IsMatch(productIsOpen, @"^.{1,}$");
            bool cheackIntroduce = Regex.IsMatch(productIntroduce, @"^.{1,500}$");
            bool cheackPrice = Regex.IsMatch(productPrice.ToString(), @"^[0-9]{1,7}$");
            bool cheackStock = Regex.IsMatch(productStock.ToString(), @"^[0-9]{1,7}$");

            if (cheackName && cheackCategory && cheackIsOpen && cheackIntroduce && cheackPrice && cheackStock)
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