using System;
//using ShoppingWeb.Ajax;

namespace ShoppingWeb.Web
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)  //頁面加載第一次時
            {

                if (Session["userName"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                
            }
            //Test test = new Test();
            //bool b = test.AnyoneLongin();
            //if (!b)
            //{
            //    Response.Write("<script>alert('重複登入，已被登出2');location.href='./Login.aspx';</script>");
            //}

        }
    }
}

