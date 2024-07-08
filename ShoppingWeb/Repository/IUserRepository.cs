using System;
using System.Data;

namespace ShoppingWeb.Repository
{
    public interface IUserRepository : IBaseRepository  //宣告介面
    {
        (Exception, int?) AddUser(AddUserDto dto);  //該介面中包含一個AddUser的方法(介面中預設是公開，所以不用加public)，該方法的返回值為: 是否有Exception(例外)操作成功則為 null，如果失敗則包含具體的異常信息，和null或是int(操作成功則為 int，表示插入成功的結果；如果操作失敗則為 null)

        (Exception, int?) DelUserInfo(DelUserInfoDto dto);

        (Exception, int?) EditUser(EditUserDto dto);

        (Exception, int?) EditUserRoles(EditRolesDto dto);

        (Exception, int?, DataTable) GetAllUserData(GetAllUserDataDto dto);
    };
}
