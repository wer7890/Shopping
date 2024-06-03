using System;

namespace ShoppingWeb.Controller
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SkipFilter : Attribute
    {
        public string FilterName { get; }

        public SkipFilter(string filterName)
        {
            FilterName = filterName;
        }
    }
}
