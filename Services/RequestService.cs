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
    public async Task<Airport> GetAirportAsync(AirportRequest airportsRequest)
    {
        var code = airportsRequest.Code.ToUpper();

        if (_cacheService.Contains(code))
        {
            return _cacheService.Get(code);
        }

        var data = await GetAirportDataAsync(code);
        var airportResponse = DeserializeAirportResponse(data);

        await ValidateAirportResponseAsync(airportResponse);
        var airport = AirportResponseToDomainMapper.ToAirport(airportResponse);
        _cacheService.Add(airport);
        return airport;

    }
    public async Task ValidateAirportRequestAsync(AirportRequest airportRequest)
    {
        AirportRequestValidator validator = new AirportRequestValidator();
        await validator.ValidateAndThrowAsync(airportRequest);
    }

    async Task ValidateAirportResponseAsync(AirportResponse airportResponse)
    {
        AirportResponseValidator validator = new AirportResponseValidator();
        await validator.ValidateAndThrowAsync(airportResponse);
    }
    async Task<string> GetAirportDataAsync(string code)
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
    public async Task<KilometersResponse> GetKilometersAsync(AirportsRequest airportsRequest)
    {
        (Airport origin, Airport destination) = await GetOriginDestinationAsync(airportsRequest);
        var kilometersResponse = new KilometersResponse { Kilometers = Helpers.Distance.CalculateKilometers(origin, destination) };

        KilometersValidator validator = new KilometersValidator();
        await validator.ValidateAndThrowAsync(kilometersResponse);

        return kilometersResponse;
    }

    public async Task<MilesResponse> GetMilesAsync(AirportsRequest airportsRequest)
    {
        (Airport origin, Airport destination) = await GetOriginDestinationAsync(airportsRequest);
        var milesResponse = new MilesResponse { Miles = Helpers.Distance.CalculateMiles(origin, destination) };
            
        MilesValidator validator = new MilesValidator();
        await validator.ValidateAndThrowAsync(milesResponse);

        return milesResponse;
    }

    async Task<Tuple<Airport, Airport>> GetOriginDestinationAsync(AirportsRequest airportsRequest)
    {
        var origin = GetAirportAsync(airportsRequest.Origin);
        var destination = GetAirportAsync(airportsRequest.Destination);

        await Task.WhenAll(origin, destination);

        return Tuple.Create(origin.Result, destination.Result);
    }
}
}
