using FluentValidation;
using Sirena.Api.Contracts.Responses;
using Sirena.Api.Domain;

namespace Sirena.Api.Validation
{
    public class GetAirportResponseValidator : AbstractValidator<AirportResponse>
    {

        public GetAirportResponseValidator()
        {
            RuleFor(x => x.Iata).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Location.Longitude).NotEmpty();
            RuleFor(x => x.Location.Latitude).NotEmpty();
        }

    }
}
