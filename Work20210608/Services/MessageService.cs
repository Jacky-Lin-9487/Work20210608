using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work20210608.Interfaces;
using Work20210608.Models;
using Work20210608.ViewModel;

namespace Work20210608.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository _dbRepository;
        private readonly IMemberService _memberService;

        public MessageService(IRepository dbrepository, IMemberService memberService)
        {
            _dbRepository = dbrepository;
            _memberService = memberService;
        }

        public Message MessageVMToModel(MessageViewModel messageVM)
        {
            Message message;

            if (messageVM == null) message = null;
            else message = new Message()
            {
                MessageId = messageVM.MessageId,
                MemberId = messageVM.MemberId,
                Content = messageVM.Content,
                Time = messageVM.Time,
            };

            return message;
        }

        public MessageViewModel MessageToVM(Message message)
        {
            //查詢關聯member
            MemberViewModel memberVM = _memberService.GetMember(message.MemberId);

            //轉換VM
            MessageViewModel messageVM;

            if (message == null) messageVM = null;
            else messageVM = new MessageViewModel()
            {
                MessageId = message.MessageId,
                MemberId = message.MemberId,
                UserName = memberVM.UserName,
                Account = memberVM.Account,
                Content = message.Content,
                Time = message.Time,
            };

            return messageVM;
        }

        public void CreateMessage(MessageViewModel messageVM)
        {
            messageVM.Time = DateTime.UtcNow + TimeSpan.FromHours(8);

            Message message = MessageVMToModel(messageVM);
            _dbRepository.Create(message);
            messageVM.MessageId = message.MessageId;
        }

        public List<MessageViewModel> GetMessages()
        {
            //抓取所有留言
            List<Message> messages = _dbRepository.GetAll<Message>().ToList();

            //Model轉VM
            List<MessageViewModel> messageVMs = new List<MessageViewModel>();
            foreach(Message message in messages)
            {
                MessageViewModel messageVM = MessageToVM(message);
                messageVMs.Add(messageVM);
            }

            return messageVMs;
        }
    }
}
