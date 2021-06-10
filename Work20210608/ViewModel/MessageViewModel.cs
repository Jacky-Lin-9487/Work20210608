using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Work20210608.ViewModel
{
    public class MessageViewModel
    {
        public int MessageId { get; set; }
        public int MemberId { get; set; }
        public string UserName { get; set; }
        public string Account { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }
    }
}
