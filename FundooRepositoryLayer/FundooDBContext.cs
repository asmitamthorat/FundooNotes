using FundooModelLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Text;

namespace FundooRepositoryLayer
{
    public class FundooDBContext:DbContext
    {
        public FundooDBContext(DbContextOptions<FundooDBContext> options):base(options)
        { 
        }
        public DbSet<Account> Account { get; set; }
        public DbSet<Lable> Lable { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<Collaborator> Collaborator { get; set; } 
    }
}
