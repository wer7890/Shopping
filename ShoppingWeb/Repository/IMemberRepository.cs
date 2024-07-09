using System;
using System.Data;

namespace ShoppingWeb.Repository
{
    public interface IMemberRepository : IBaseRepository
    {
        (Exception, int?) EditMemberStatus(EditMemberStatusDto dto);

        (Exception, int?) EditMemberLevel(EditMemberLevelDto dto);

        (Exception, int?) AddMember(AddMemberDto dto);

        (Exception, int?, DataTable) GetAllMemberData(GetAllMemberDataDto dto);
    };
}