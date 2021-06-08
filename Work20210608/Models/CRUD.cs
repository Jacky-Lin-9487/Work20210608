using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Work20210608.Models
{
    public class CRUD
    {
        public int ID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
