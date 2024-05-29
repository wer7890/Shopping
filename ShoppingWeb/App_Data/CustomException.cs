using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingWeb.Controller
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {

        }
    }
}