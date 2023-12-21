using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sirena.Api.Contracts.Requests;
using Sirena.Api.Contracts.Responses;
using Sirena.Api.Domain;
using Sirena.Api.Domain.Services;
using System.Collections.Generic;
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


        [HttpPost("airport")]

        public async Task<Airport> Airport([FromBody] AirportRequest airport)
        {
            return await _requestService.GetAirport(airport);
        }
        [HttpPost("miles")]

        public async Task<MilesResponse> Miles([FromBody] AirportsRequest airportsRequest)
        {
            return await _requestService.GetMiles(airportsRequest);
        }
        [HttpPost("kilometers")]

        public async Task<KilometersResponse> Kilometers([FromBody] AirportsRequest airportsRequest)
        {
            return await _requestService.GetKilometers(airportsRequest);
        }
    }
}
