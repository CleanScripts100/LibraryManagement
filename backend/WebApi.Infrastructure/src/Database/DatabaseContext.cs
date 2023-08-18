using Microsoft.EntityFrameworkCore;
using Npgsql;
using WebApi.Domain.src.Entities;
using WebApi.Domain.src.Enums;

namespace WebApi.Infrastructure.src.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<User> Users {get; set;}
        public DbSet<Book> Books {get; set;}
        public DbSet<Review> Reviews {get; set;}
        public DbSet<Loan> Loans { get; set;}
        public DatabaseContext(IConfiguration configuration)
        {
            _config = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("DefaultConnection"));
            builder.MapEnum<Role>();
            builder.MapEnum<Genre>();
            builder.MapEnum<Gender>();
            builder.MapEnum<LoanStatus>();
            optionsBuilder.UseNpgsql(builder.Build());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)    
        {
            modelBuilder.HasPostgresEnum<Role>();
            modelBuilder.HasPostgresEnum<Genre>();
            modelBuilder.HasPostgresEnum<Gender>();
            modelBuilder.HasPostgresEnum<LoanStatus>();

            modelBuilder.Entity<User>().
            HasIndex(u => u.Email).IsUnique();
        }
    }
}