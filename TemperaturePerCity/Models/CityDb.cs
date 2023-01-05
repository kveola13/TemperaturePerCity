using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TemperaturePerCity.Models;

namespace TemperaturePerCity.Models
{
    public class CityDb : DbContext
    {
        public CityDb(DbContextOptions<CityDb> options)
        : base(options) { }

        public DbSet<City> Cities => Set<City>();

        public DbSet<TemperaturePerCity.Models.CityDTO> CityDTO { get; set; }
    }
}
