using LivingStream.Data.Entities;
using Microsoft.EntityFrameworkCore;
using LivingStream.Data;


namespace LivingStream.Data.Context
{
    public class LivingStreamContext : DbContext
    {

        public DbSet<User>? Users { get; set; }

        public LivingStreamContext(DbContextOptions<LivingStreamContext> options)
            : base(options)
        {
        }

    }
}
