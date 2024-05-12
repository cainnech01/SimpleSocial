using FTsR.Models;
using Microsoft.EntityFrameworkCore;

namespace FTsR.Data
{
    public class PinDbContext : DbContext
    {
        public DbSet<PinModel> Pin { get; set; }

        public PinDbContext(DbContextOptions<PinDbContext> options)
        : base(options)
        {
        }
    }
}
