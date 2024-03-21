﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

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
                    int maxFileSize = 500 * 1024;  // 檢查圖片大小（假設限制大小為500KB）
                    if (uploadedFile.ContentLength > maxFileSize)
                    {
                        Response.Write("上傳的圖片大小超過限制（最大500KB）");
                    }
                    else
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
        public static string AddProduct(string productName, string productCategory, string productPrice, string productStock, string productIsOpen, string productIntroduce)
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
                            cmd.Parameters.Add(new SqlParameter("@name", productName));
                            cmd.Parameters.Add(new SqlParameter("@category", productCategory));
                            cmd.Parameters.Add(new SqlParameter("@img", pubguid));
                            cmd.Parameters.Add(new SqlParameter("@price", productPrice));
                            cmd.Parameters.Add(new SqlParameter("@stock", productStock));
                            cmd.Parameters.Add(new SqlParameter("@isOpen", productIsOpen));
                            cmd.Parameters.Add(new SqlParameter("@introduce", productIntroduce));
                            cmd.Parameters.Add(new SqlParameter("@owner", HttpContext.Current.Session["userName"]));
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
                return "輸入值不符合格式";
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
        public static bool SpecialChar(string productName, string productCategory, string productIsOpen, string productIntroduce, string productPrice, string productStock)
        { 
            bool cheackName = Regex.IsMatch(productName, @"^.{1,40}$");
            bool cheackCategory = Regex.IsMatch(productCategory, @"^.{1,}$");
            bool cheackIsOpen = Regex.IsMatch(productIsOpen, @"^.{1,}$");
            bool cheackIntroduce = Regex.IsMatch(productIntroduce, @"^.{1,500}$");
            bool cheackPrice = Regex.IsMatch(productPrice, @"^[0-9]{1,7}$");
            bool cheackStock = Regex.IsMatch(productStock, @"^[0-9]{1,7}$");

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