using System;

namespace ShoppingWeb.Repository
{
    public interface IUserRepository  //宣告介面
    {
        (Exception, int?) AddUser(AddUserDto dto);  //該介面中包含一個AddUser的方法(介面中預設是公開，所以不用加public)，該方法的返回值有可能是Exception(例外)，也有可能是可能為null的int
    };
}
