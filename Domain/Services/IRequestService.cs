﻿using Sirena.Api.Contracts.Requests;
using Sirena.Api.Contracts.Responses;
using System.Threading.Tasks;

namespace Sirena.Api.Domain.Services
{
    public interface IRequestService
    {
       Task<Airport> GetAirport(AirportRequest airport);
       Task<MilesResponse> GetMiles(AirportsRequest airports);
       Task<KilometersResponse> GetKilometers(AirportsRequest airports);
       Task ValidateAirportRequest(AirportRequest airportRequest);
    }
}
