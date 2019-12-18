using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ConferanceDbContext : DbContext
    {
        public ConferanceDbContext(DbContextOptions<ConferanceDbContext> dbContextOptions)
            :base(dbContextOptions)
        {
                
        }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
    }
}
