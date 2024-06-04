using FTsR.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FTsR.Data
{
    public class PinDbContext : DbContext
    {
        public DbSet<Pin> Pin { get; set; }
        public DbSet<SavedPinModel> SavedPin { get; set; }

        public PinDbContext(DbContextOptions<PinDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pin>().ToTable(tb => tb.HasTrigger("TriggerName"));
            base.OnModelCreating(modelBuilder);
        }
    }
}
