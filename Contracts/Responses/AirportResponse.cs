namespace Sirena.Api.Contracts.Responses
{
    public class AirportResponse
    {
        public string Iata { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string CityIata { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string CountryIata { get; set; } = string.Empty;
        public AirportLocationResponse Location { get; set; }
        public int Rating { get; set; }
        public int Hubs { get; set; }
        public string TimezoneRegionName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
