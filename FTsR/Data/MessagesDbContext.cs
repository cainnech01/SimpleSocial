using FTsR.Models;
using Microsoft.EntityFrameworkCore;

namespace FTsR.Data
{
    public class MessagesDbContext : DbContext
    {
        public MessagesDbContext(DbContextOptions<MessagesDbContext> options) : base(options)
        {
        }

        public DbSet<MessageModel> Messages { get; set; }
    }
}
