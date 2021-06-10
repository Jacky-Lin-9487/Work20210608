using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work20210608.Interfaces;
using Work20210608.Models;
using Work20210608.ViewModel;

namespace Work20210608.Services
{
    public class MemberService : IMemberService
    {
        private readonly IRepository _dbRepository;

        public MemberService(IRepository dbrepository)
        {
            _dbRepository = dbrepository;
        }

        public Member MemberVMToModel(MemberViewModel memberVM)
        {
            Member member;

            if (memberVM == null) member = null;
            else member = new Member()
            {
                MemberId = memberVM.MemberId,
                Account = memberVM.Account,
                Password = memberVM.Password,
                UserName = memberVM.UserName,
            };

            return member;
        }

        public MemberViewModel MemberToVM(Member member)
        {
            MemberViewModel memberVM;

            if (member == null) memberVM = null;
            else memberVM = new MemberViewModel()
            {
                MemberId = member.MemberId,
                Account = member.Account,
                Password = member.Password,
                UserName = member.UserName,
            };

            return memberVM;
        }

        public void Register(MemberViewModel memberVM)
        {
            //檢查帳號重複
            Member member = _dbRepository.GetAll<Member>().FirstOrDefault(member =>
                member.Account == memberVM.Account
            );
            if (member != null)
            {
                memberVM.MemberId = -1;
                return;
            }

            //建立帳號
            member = MemberVMToModel(memberVM);
            _dbRepository.Create(member);
            memberVM.MemberId = member.MemberId;
        }

        public MemberViewModel Login(MemberViewModel memberVM)
        {
            Member member = _dbRepository.GetAll<Member>().FirstOrDefault(member => 
                member.Account == memberVM.Account
                && member.Password == memberVM.Password
            );

            memberVM = MemberToVM(member);

            return memberVM;
        }

        public MemberViewModel GetMember(int MemberId)
        {
            Member member = _dbRepository.GetAll<Member>().FirstOrDefault(member =>
                member.MemberId == MemberId
            );

            MemberViewModel memberVM = MemberToVM(member);

            return memberVM;
        }
    }
}
