using Sirena.Api.Domain;
using Sirena.Api.Domain.Common;
using Sirena.Api.Contracts.Responses;

namespace Sirena.Api.Mapping
{
    public static class AirportResponseToDomainMapper
    {
        public static Airport ToAirport(this AirportResponse response)
        {
            return new Airport
            {
    
                IataCode = IataCode.From(response.Iata),
                Name = Name.From(response.Name),
                Longitude = Longitude.From(response.Location.Longitude),
                Latitude = Latitude.From(response.Location.Latitude)

            };
        }

    }
}
