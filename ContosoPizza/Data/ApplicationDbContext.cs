using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        // Định nghĩa các DbSet cho các bảng trong cơ sở dữ liệu của bạn
        public DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<Pizza> MyPizzas { get; set; }
    }

}
