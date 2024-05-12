using FTsR.Models;
using Microsoft.EntityFrameworkCore;

namespace FTsR.Data
{
    public class CompaniesDbContext : DbContext
    {
        public DbSet<FTsR.Models.Companies> Companies { get; set; } = default!;

        public CompaniesDbContext(DbContextOptions<CompaniesDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Companies>().ToTable(tb => tb.HasTrigger("TriggerName"));
            base.OnModelCreating(modelBuilder);
        }
    }
}
