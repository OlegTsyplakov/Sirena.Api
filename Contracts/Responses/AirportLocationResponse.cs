using Newtonsoft.Json;


namespace Sirena.Api.Contracts.Responses
{
    public class AirportLocationResponse
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }
        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }
}
