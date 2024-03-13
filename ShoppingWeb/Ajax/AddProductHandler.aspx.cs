using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingWeb.Ajax
{
    public partial class AddProductHandler : System.Web.UI.Page
    {
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

                    // 設定目標資料夾路徑
                    string targetFolderPath = Server.MapPath("../Images/");

                    // 檢查檔案是否已存在於目標資料夾中
                    if (File.Exists(Path.Combine(targetFolderPath, fileName)))
                    {
                        Response.Write("上傳的檔案名稱已存在");
                    }
                    else
                    {
                        // 上傳檔案到目標資料夾
                        string filePath = Path.Combine(targetFolderPath, fileName);
                        uploadedFile.SaveAs(filePath);

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
        public static int AddProduct(string productName, string productCategory, string productImg, string productPrice, string productStock, string productIsOpen, string productIntroduce)
        {
            int result = 0;
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
                        cmd.Parameters.Add(new SqlParameter("@productImg", productImg));
                        cmd.Parameters.Add(new SqlParameter("@productPrice", productPrice));
                        cmd.Parameters.Add(new SqlParameter("@productStock", productStock));
                        cmd.Parameters.Add(new SqlParameter("@productIsOpen", productIsOpen));
                        cmd.Parameters.Add(new SqlParameter("@productIntroduce", productIntroduce));

                        SqlParameter resultParam = new SqlParameter("@result", SqlDbType.Int);
                        resultParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(resultParam);

                        // 执行存储过程
                        cmd.ExecuteNonQuery();

                        // 获取存储过程返回值
                        result = Convert.ToInt32(resultParam.Value);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }

  


    }
}