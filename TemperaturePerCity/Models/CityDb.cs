using Microsoft.EntityFrameworkCore;


namespace TemperaturePerCity.Models
{
    public class CityDb : DbContext
    {
        public CityDb(DbContextOptions<CityDb> options)
        : base(options) { }

        public DbSet<City> Cities => Set<City>();

        public DbSet<CityDTO> CityDTO { get; set; }
    }
}
