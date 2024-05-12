using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FTsR.Models;

namespace FTsR.Data
{
    public class DataDbContext : DbContext
    {
        public DataDbContext (DbContextOptions<DataDbContext> options)
            : base(options)
        {
        }

        public DbSet<FTsR.Models.DataModel> DataModel { get; set; } = default!;
    }
}
