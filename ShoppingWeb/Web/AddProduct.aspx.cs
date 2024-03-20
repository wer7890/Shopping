using System;

namespace ShoppingWeb.Web
{
    public partial class AddProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)  
            {

                if (Session["userName"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

            }

        }
    }
}