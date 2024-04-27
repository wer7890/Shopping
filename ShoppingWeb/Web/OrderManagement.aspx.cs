using System;
using ShoppingWeb.Ajax;

namespace ShoppingWeb.Web
{
    public partial class OrderManagement : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btCssLink.Attributes["href"] = $"/css/{cssVersion}/bootstrap.min.css";
            cssLink.Attributes["href"] = $"/css/{cssVersion}/OrderManagement.css";
        }
    }
}