using class_work.Configuration;
using class_work.Models;
using Microsoft.EntityFrameworkCore;

namespace class_work.Context
{
    public class DataBaseContext:DbContext
    {
        public DbSet<Product> Products { get; set; }

        private readonly IConfiguration _configuration;

        public DataBaseContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DataBaseContext(IConfiguration configuration)
        {

            _configuration = configuration;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString, options =>
            {
                options.EnableRetryOnFailure(
                    maxRetryCount: 5, // Number of retry attempts
                    maxRetryDelay: TimeSpan.FromSeconds(30), // Delay between retries
                    errorNumbersToAdd: null // List of additional error numbers to consider transient
                );
            });

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
