using System;

namespace ShoppingWeb.Web
{
    public partial class MemberManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["userId"] == null)
            {
                Response.Redirect("Login.aspx");
            }

        }
    }
}