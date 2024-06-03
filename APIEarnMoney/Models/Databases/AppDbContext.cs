using APIEarnMoney.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIEarnMoney.Models.Databases
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public  virtual DbSet<EarnMoneyUser> EarnMoneyUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EarnMoneyUser>().HasKey(e => new { e.GoogleId, e.DeviceId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
