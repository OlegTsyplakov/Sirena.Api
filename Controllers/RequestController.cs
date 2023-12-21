using FluentValidation;
using FluentValidation.Results;
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


        //[HttpPost("airport")]
        [HttpGet("airport/{code}")]
        public async Task<Airport> Airport(string code)
        {
            code = code.ToUpper();
            if (code.Length != 3)
            {
                var message = $"Airport code {code} is not 3 letters";
                throw new ValidationException(message, new[]
                {     
                new ValidationFailure(nameof(AirportRequest), message)
                });
            }
            var airport = new AirportRequest() { Code = code };
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
