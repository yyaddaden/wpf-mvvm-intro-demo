using Microsoft.EntityFrameworkCore;

namespace temperature_conversion_app_ef_core.Models
{
    class TemperatureConversionDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Conversion> Conversions { get; set; }
        public DbSet<Metric> Metrics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Database=TemperatureConversionAppDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Metric>().HasData(
                new Metric() { MetricId = 1, Title = "Celsius" },
                new Metric() { MetricId = 2, Title = "Fahrenheit" },
                new Metric() { MetricId = 3, Title = "Kelvin" }
            );
        }
    }
}
