﻿using System;
using ShoppingWeb.Ajax;

namespace ShoppingWeb.Web
{
    public partial class AddUser : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btCssLink.Attributes["href"] = $"/css/{cssVersion}/bootstrap.min.css";
        }
    }
}