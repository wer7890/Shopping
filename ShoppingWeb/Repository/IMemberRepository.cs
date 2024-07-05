using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingWeb.Repository
{
    public interface IMemberRepository
    {
        (Exception, int?) EditMemberStatus(EditMemberStatusDto dto);

        (Exception, int?) EditMemberLevel(EditMemberLevelDto dto);

        (Exception, int?) AddMember(AddMemberDto dto);
    };
}