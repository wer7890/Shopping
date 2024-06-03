using System;

namespace ShoppingWeb.Controller
{
    [AttributeUsage(AttributeTargets.Method)]  //自訂屬性只能應用於方法
    public class SkipFilter : Attribute
    {
        public string FilterName { get; }

        public SkipFilter(string filterName)
        {
            FilterName = filterName;
        }
    }
}
