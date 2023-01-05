namespace TemperaturePerCity.Models
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string? CityName { get; set; }
        public string? Country { get; set; }
        public string? Continent { get; set; }
        public DateTime CurrentTime { get; set; }
        public double Temperature { get; set; }
    }
}
