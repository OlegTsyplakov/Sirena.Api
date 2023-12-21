using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using Sirena.Api.Contracts.Requests;
using Sirena.Api.Contracts.Responses;
using Sirena.Api.Domain;
using Sirena.Api.Domain.Services;
using Sirena.Api.Mapping;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sirena.Api.Services
{
    public class RequestService : IRequestService
    {
        private readonly HttpClient _httpClient;
        public RequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Airport> GetAirport(AirportRequest airportsRequest)
        {
    

            try
            {
                
                var responseString = await _httpClient.GetStringAsync(airportsRequest.Code);
                var airportResponse = JsonConvert.DeserializeObject<AirportResponse>(responseString);
                var airport = AirportResponseToDomainMapper.ToAirport(airportResponse);
             
                return airport;
            }
            catch (System.Exception)
            {
                var message = $"Can't get data about {airportsRequest.Code} airport";
                throw new ValidationException(message, new[]
          {
                new ValidationFailure(nameof(AirportRequest), message)
            });
            }

        
        }

        public async Task<KilometersResponse> GetKilometers(AirportsRequest airportsRequest)
        {
            (Airport origin, Airport destination) = await GetOriginDestination(airportsRequest);
            return new KilometersResponse { Kilometers = Helpers.Distance.CalculateKilometers(origin, destination) };
        }

        public async Task<MilesResponse> GetMiles(AirportsRequest airportsRequest)
        {
            (Airport origin, Airport destination) = await GetOriginDestination(airportsRequest);
            return new MilesResponse { Miles=Helpers.Distance.CalculateMiles(origin, destination) };
        }

       async Task<Tuple<Airport, Airport>> GetOriginDestination(AirportsRequest airportsRequest)
        {
            var origin = GetAirport(airportsRequest.Origin);
            var destination = GetAirport(airportsRequest.Destination);

            await Task.WhenAll(origin, destination);
            return Tuple.Create(origin.Result, destination.Result);
        }
    }
}
