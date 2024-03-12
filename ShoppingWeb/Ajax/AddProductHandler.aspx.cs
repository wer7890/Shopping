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
                string fileName = Path.GetFileName(uploadedFile.FileName);

                string filePath = Path.Combine(Server.MapPath("../Images/"), fileName);

                uploadedFile.SaveAs(filePath);

                Response.Write("檔案上傳成功！文件名：" + fileName);
            }
            else
            {
                Response.Write("未選擇要上傳的檔案");
            }
        }


        [WebMethod]
        public static string AddProduct(string productName, string productCategory, string productImg, string productPrice, string productStock, string productIsOpen, string productIntroduce)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("", con))
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