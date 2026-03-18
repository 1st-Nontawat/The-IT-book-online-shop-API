using Microsoft.EntityFrameworkCore;
using TheITBookOnlineShop.Models.Entities;

namespace TheITBookOnlineShop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // ต้องมีแค่อย่างละ 1 บรรทัด ห้ามซ้ำครับ 👇
        public DbSet<User> Users { get; set; }
        public DbSet<UserLike> UserLikes { get; set; }
    }
}