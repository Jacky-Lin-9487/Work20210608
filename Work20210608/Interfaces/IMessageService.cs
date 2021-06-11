using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work20210608.ViewModel;

namespace Work20210608.Interfaces
{
    public interface IMessageService
    {
        void CreateMessage(MessageViewModel messageVM);
        List<MessageViewModel> GetMessages();
        MessageViewModel Edit(MessageViewModel messageVM);
        MessageViewModel Delete(MessageViewModel messageVM);
    }
}
