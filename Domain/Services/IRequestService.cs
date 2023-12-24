using Sirena.Api.Contracts.Requests;
using Sirena.Api.Contracts.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Sirena.Api.Domain.Services
{
    public interface IRequestService
    {
       Task<Airport> GetAirportAsync(AirportRequest airport, CancellationToken cancellationToken);
       Task<MilesResponse> GetMilesAsync(AirportsRequest airports, CancellationToken cancellationToken);
       Task<KilometersResponse> GetKilometersAsync(AirportsRequest airports, CancellationToken cancellationToken);
       Task ValidateAirportRequestAsync(AirportRequest airportRequest, CancellationToken cancellationToken);
    }
}
