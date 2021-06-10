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

        public Member memberVMToModel(MemberViewModel memberVM)
        {
            Member member;

            if (memberVM == null) member = null;
            else member = new Member()
            {
                MemberId = memberVM.MemberId,
                Name = memberVM.Name,
                Password = memberVM.Password,
            };

            return member;
        }

        public MemberViewModel memberToVM(Member member)
        {
            MemberViewModel memberVM;

            if (member == null) memberVM = null;
            else memberVM = new MemberViewModel()
            {
                MemberId = member.MemberId,
                Name = member.Name,
                Password = member.Password,
            };

            return memberVM;
        }

        public void Register(MemberViewModel memberVM)
        {
            //檢查帳號重複
            Member member = _dbRepository.GetAll<Member>().FirstOrDefault(member =>
                member.Name == memberVM.Name
            );
            if (member != null)
            {
                memberVM.MemberId = -1;
                return;
            }

            member = memberVMToModel(memberVM);

            _dbRepository.Create(member);

            memberVM.MemberId = member.MemberId;
        }

        public MemberViewModel Login(MemberViewModel memberVM)
        {
            Member member = _dbRepository.GetAll<Member>().FirstOrDefault(member => 
                member.Name == memberVM.Name
                && member.Password == memberVM.Password
            );

            memberVM = memberToVM(member);

            return memberVM;
        }

        public MemberViewModel GetMember(int MemberId)
        {
            Member member = _dbRepository.GetAll<Member>().FirstOrDefault(member =>
                member.MemberId == MemberId
            );

            MemberViewModel memberVM = memberToVM(member);

            return memberVM;
        }
    }
}
