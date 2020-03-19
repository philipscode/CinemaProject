using Microsoft.EntityFrameworkCore;

namespace Rambler.WebApi.Cinema.Models
{
    /// <summary>
    /// Класс для работы с БД
    /// </summary>
    public class CinemaContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionType> SessionTypes { get; set; }

        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderSession>().HasKey(os => new {os.OrderId, os.SessionId});
        }
    }
}