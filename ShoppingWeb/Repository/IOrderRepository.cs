using System;
using System.Data;

namespace ShoppingWeb.Repository
{
    public interface IOrderRepository : IBaseRepository
    {
        (Exception, int?) EditOrder(EditOrderDto dto);

        (Exception, int?) EditReturnOrder(EditReturnOrderDto dto);

        (Exception, DataTable) GetOrderDetailsData(GetOrderDetailsDataDto dto);

        (Exception, int?, DataSet) GetAllOrderData(GetAllOrderDataDto dto);

        (Exception, int?, DataSet) GetReturnOrderData(GetReturnOrderDataDto dto);

        (Exception, int?, DataSet) GetOrderData(GetOrderDataDto dto);
    }
}