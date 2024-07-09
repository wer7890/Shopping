using System;

namespace ShoppingWeb.Repository
{
    public interface IProductRepository : IBaseRepository
    {
        (Exception, int?) DelProduct(DelProductDto dto);

        (Exception, int?) EditProductStatus(EditProductStatusDto dto);

        (Exception, int?) EditProduct(EditProductDto dto);
    }
}