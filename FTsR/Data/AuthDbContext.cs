using FTsR.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FTsR.Data
{
    public class AuthDbContext : IdentityDbContext<UserModel>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
    }

}
