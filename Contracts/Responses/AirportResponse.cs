using Newtonsoft.Json;


namespace Sirena.Api.Contracts.Responses
{
    public class AirportResponse
    {
        [JsonProperty("iata")]
        public string Iata { get; set; } = string.Empty;
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty("city")]
        public string City { get; set; } = string.Empty;
        [JsonProperty("city_iata")]
        public string CityIata { get; set; } = string.Empty;
        [JsonProperty("country")]
        public string Country { get; set; } = string.Empty;
        [JsonProperty("country_iata")]
        public string CountryIata { get; set; } = string.Empty;
        [JsonProperty("location")]
        public AirportLocationResponse Location { get; set; }
        [JsonProperty("rating")]
        public int Rating { get; set; }
        [JsonProperty("hubs")]
        public int Hubs { get; set; }
        [JsonProperty("timezone_region_name")]
        public string TimezoneRegionName { get; set; } = string.Empty;
        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }
}
