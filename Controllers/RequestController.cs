using Microsoft.AspNetCore.Mvc;
using Sirena.Api.Contracts.Requests;
using Sirena.Api.Contracts.Responses;
using Sirena.Api.Domain;
using Sirena.Api.Domain.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Sirena.Api.Controllers
{
[ApiController]
[ApiVersion("1.0")]
[Route("api/")]


public class RequestController : ControllerBase
{
    private readonly IRequestService _requestService;

    public RequestController(IRequestService requestService)
    {
        _requestService = requestService;
    }


    [HttpGet("airport/{code}")]
    public async Task<Airport> Airport(string code, CancellationToken cancellationToken)
    {
        var airportRequest = new AirportRequest() { Code = code };

        await _requestService.ValidateAirportRequestAsync(airportRequest, cancellationToken);

        return await _requestService.GetAirportAsync(airportRequest, cancellationToken);  
    }
    [HttpPost("miles")]

    public async Task<MilesResponse> Miles([FromBody] AirportsRequest airportsRequest, CancellationToken cancellationToken)
    {
        return await _requestService.GetMilesAsync(airportsRequest, cancellationToken);
    }
    [HttpPost("kilometers")]

    public async Task<KilometersResponse> Kilometers([FromBody] AirportsRequest airportsRequest, CancellationToken cancellationToken)
    {
        return await _requestService.GetKilometersAsync(airportsRequest, cancellationToken);
    }
}
}
