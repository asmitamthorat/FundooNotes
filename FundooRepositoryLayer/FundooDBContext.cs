using FundooModelLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace FundooRepositoryLayer
{
    public class FundooDBContext:DbContext
    {
        public DbSet<Account> Account { get; set; }
        public DbSet<Lable> Lable { get; set; }

        public DbSet<Notes> Notes { get; set; }
    }
}
