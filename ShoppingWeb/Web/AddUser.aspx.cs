﻿using System;

namespace ShoppingWeb.Web
{
    public partial class AddUser1 : System.Web.UI.Page
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