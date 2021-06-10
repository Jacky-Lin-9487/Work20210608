using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Work20210608.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        [MaxLength(50)]
        public int MemberId { get; set; }
        public Member Member { get; set; }
        [MaxLength(50)]
        public string Content { get; set; }
        [MaxLength(50)]
        public DateTime Time { get; set; }
    }
}
