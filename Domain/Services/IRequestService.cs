using Sirena.Api.Contracts.Requests;
using Sirena.Api.Contracts.Responses;
using System.Threading.Tasks;

namespace Sirena.Api.Domain.Services
{
    public interface IRequestService
    {
       Task<Airport> GetAirportAsync(AirportRequest airport);
       Task<MilesResponse> GetMilesAsync(AirportsRequest airports);
       Task<KilometersResponse> GetKilometersAsync(AirportsRequest airports);
       Task ValidateAirportRequestAsync(AirportRequest airportRequest);
    }
}
