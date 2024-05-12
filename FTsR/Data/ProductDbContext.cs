using FTsR.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class ProductDbContext : DbContext
{
    public DbSet<Product> Product { get; set; }

    public ProductDbContext(DbContextOptions<ProductDbContext> options)
    : base(options)
    {
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=FTsR;Integrated Security=True;Connect Timeout=30;Encrypt=False");
    //}
}
