using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ShoppingWeb.Repository
{
    public interface IOrderRepository : IBaseRepository
    {
        (Exception, int?) EditOrder(EditOrderDto dto);
    }
}