using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace ShoppingWeb.Web
{
    public partial class SearchUser1 : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["cns"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)  
            {

                if (Session["userName"] == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }
                
            }

        }

    }
}