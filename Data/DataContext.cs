using Microsoft.EntityFrameworkCore;
using disclodo.Models;

namespace disclodo.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Id).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasDefaultValueSql("now()");
            modelBuilder.Entity<Channel>().Property(c => c.Id).HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Channel>().Property(c => c.CreatedAt).HasDefaultValueSql("now()");
            modelBuilder.Entity<Message>().Property(c => c.CreatedAt).HasDefaultValueSql("now()");
        }

        public DbSet<User> User { get; set; }
        public DbSet<Channel> Channel { get; set; }
        public DbSet<Message> Message { get; set; }
    }
}