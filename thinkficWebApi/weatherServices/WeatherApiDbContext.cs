using Microsoft.EntityFrameworkCore;
using weatherServices.Models;

namespace weatherServices
{
    public class WeatherApiDbContext : DbContext
    {
        public DbSet<WeatherEntity> Weather { get; set; }
        public WeatherApiDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
