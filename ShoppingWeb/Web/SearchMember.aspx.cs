﻿using System;

namespace ShoppingWeb.Web
{
    public partial class SearchMember : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (Session["userId"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

            }

        }
    }
}