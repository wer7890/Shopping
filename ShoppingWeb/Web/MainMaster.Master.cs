using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShoppingWeb.Web
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            Session["userName"] = null;
            Response.Redirect("Login.aspx");
        }
    }
}