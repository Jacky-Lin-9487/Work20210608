using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Work20210608.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        
        [MaxLength(50)]
        public string Account { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }

        //public List<Message> Messages { get; set; }
    }
}
