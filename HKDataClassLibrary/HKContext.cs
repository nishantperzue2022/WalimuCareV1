using Microsoft.EntityFrameworkCore;
using HKDataClassLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HKDataClassLibrary
{
    public class HKContext: DbContext
    {
        public HKContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<Member> Members { get; set; } 
    }
}
