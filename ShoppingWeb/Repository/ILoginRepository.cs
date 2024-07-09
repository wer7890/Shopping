using System;

namespace ShoppingWeb.Repository
{
    public interface ILoginRepository : IBaseRepository
    {
        (Exception, int?) LoginUser(LoginUserDto dto);
    }
}