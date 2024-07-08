using System;

namespace ShoppingWeb.Repository
{
    public interface IBaseRepository
    {
        void SetNLog(Exception ex);
    }
}