using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using Sirena.Api.Contracts.Requests;
using Sirena.Api.Contracts.Responses;
using Sirena.Api.Domain;
using Sirena.Api.Domain.Services;
using Sirena.Api.Mapping;
using Sirena.Api.Validation;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sirena.Api.Services
{
public class RequestService : IRequestService
{
    private readonly HttpClient _httpClient;
    private readonly ICacheService _cacheService;
    public RequestService(HttpClient httpClient, ICacheService cacheService)
    {
        _httpClient = httpClient;
        _cacheService = cacheService;
    }
    public async Task<Airport> GetAirport(AirportRequest airportsRequest)
    {
        var code = airportsRequest.Code.ToUpper();

        if (_cacheService.Contains(code))
        {
            return _cacheService.Get(code);
        }

            var data = await GetAirportData(code);
            var airportResponse = DeserializeAirportResponse(data);

        try
        {
                
            AirportResponseValidator validator = new AirportResponseValidator();
            await validator.ValidateAndThrowAsync(airportResponse);


            var airport = AirportResponseToDomainMapper.ToAirport(airportResponse);
            _cacheService.Add(airport);
            return airport;
        }
        catch (ValidationException e)
        {
            throw new ValidationException(e.Message, new[]
            {
            new ValidationFailure(nameof(AirportResponse), e.Message)
            });
        }

    }
    async Task<string> GetAirportData(string code)
    {
        try
        {
            return await _httpClient.GetStringAsync(code);
        }
        catch (HttpRequestException e)
        {
            throw new ValidationException(e.Message, new[]
            {
            new ValidationFailure(nameof(AirportRequest),$"{e.Message} On airport code {code}"  )
            });
        }
    }
    AirportResponse DeserializeAirportResponse(string data)
    {
        try
        {
            return JsonConvert.DeserializeObject<AirportResponse>(data);
        }
        catch (JsonReaderException e)
        {
            throw new ValidationException(e.Message, new[]
            {
            new ValidationFailure(nameof(AirportResponse),e.Message)
            });
        }
    }
    public async Task<KilometersResponse> GetKilometers(AirportsRequest airportsRequest)
    {
        (Airport origin, Airport destination) = await GetOriginDestination(airportsRequest);
        var kilometersResponse = new KilometersResponse { Kilometers = Helpers.Distance.CalculateKilometers(origin, destination) };

        KilometersValidator validator = new KilometersValidator();
        await validator.ValidateAndThrowAsync(kilometersResponse);

        return kilometersResponse;
    }

    public async Task<MilesResponse> GetMiles(AirportsRequest airportsRequest)
    {
        (Airport origin, Airport destination) = await GetOriginDestination(airportsRequest);
        var milesResponse = new MilesResponse { Miles = Helpers.Distance.CalculateMiles(origin, destination) };
            
        MilesValidator validator = new MilesValidator();
        await validator.ValidateAndThrowAsync(milesResponse);

        return milesResponse;
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
