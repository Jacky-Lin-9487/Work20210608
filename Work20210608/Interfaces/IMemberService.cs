using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work20210608.ViewModel;

namespace Work20210608.Interfaces
{
    public interface IMemberService
    {
        void Register(MemberViewModel memberVM);

        MemberViewModel Login(MemberViewModel memberVM);

        MemberViewModel GetMember(int MemberId);

    }
}
