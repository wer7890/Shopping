﻿using System;

namespace ShoppingWeb.Web
{
    public partial class EditUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["userId"] == null)
            {
                Response.Write("<script>window.parent.location.href = 'Login.aspx';</script>");
            }

        } 

    }
}