using ContosoPizza.Helprs;
using ContosoPizza.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        // Định nghĩa các DbSet cho các bảng trong cơ sở dữ liệu của bạn
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<Pizza> MyPizzas { get; set; }

    }

}
