using Newtonsoft.Json;

namespace TemperaturePerCity.Models
{
    public class CityDTO
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
        public string? CityName { get; set; }
        public string? Country { get; set; }
        public string? Continent { get; set; }
        public DateTime CurrentTime { get; set; }
        public double Temperature { get; set; }
        [JsonProperty(PropertyName = "partitionKey")]
        public string? PartitionKey { get; set; }
    }
}
