using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Work20210608.Models;

namespace Work20210608.Data
{
    public class Work20210608Context : DbContext
    {
        public Work20210608Context (DbContextOptions<Work20210608Context> options)
            : base(options)
        {
        }

        public DbSet<CRUD> CRUD { get; set; }
        public DbSet<Member> Member { get; set; }
    }
}
