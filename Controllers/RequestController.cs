using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Sirena.Api.Contracts.Requests;
using Sirena.Api.Contracts.Responses;
using Sirena.Api.Domain;
using Sirena.Api.Domain.Services;
using Sirena.Api.Validation;
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
        public async Task<Airport> Airport(string code)
        {
            var airportRequest = new AirportRequest() { Code = code };

            AirportRequestValidator validator = new AirportRequestValidator();
           await validator.ValidateAndThrowAsync(airportRequest);

            return await _requestService.GetAirport(airportRequest);  
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
