
namespace Sirena.Api.Contracts.Requests
{
    public class AirportsRequest
    {
        public AirportRequest Origin { get; set; }
        public AirportRequest Destination { get; set; }
    }
}
