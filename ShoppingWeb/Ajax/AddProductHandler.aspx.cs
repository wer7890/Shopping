using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
                // 檢查檔案是否是圖片
                if (uploadedFile.ContentType.StartsWith("image/"))
                {
                    string fileName = Path.GetFileName(uploadedFile.FileName);

                    // 建立新的檔名，GUID每个x是0-9或a-f范围内一个32位十六进制数
                    string guid = Guid.NewGuid().ToString("N").Substring(0, 29);   
                    
                    string newFileName = DateTime.Now.ToString(guid) + Path.GetExtension(fileName);
                    pubguid = newFileName;

                    // 指定儲存路徑
                    string targetFolderPath = Server.MapPath("~/Images/" + newFileName);

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


        [WebMethod]
        public static string AddProduct(string productName, string productCategory, string productPrice, string productStock, string productIsOpen, string productIntroduce)
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

    }
}