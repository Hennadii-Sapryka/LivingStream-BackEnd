using LivingStream.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivingStream.Data.Context
{
    public class LivingStreamContext : DbContext
    {

        public DbSet<User>? Users { get; set; }

        public LivingStreamContext(DbContextOptions<LivingStreamContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
