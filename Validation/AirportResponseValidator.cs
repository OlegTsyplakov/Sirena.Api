using FluentValidation;
using Sirena.Api.Contracts.Responses;

namespace Sirena.Api.Validation
{
    public class AirportResponseValidator : AbstractValidator<AirportResponse>
    {

        public AirportResponseValidator()
        {
            RuleFor(x => x.Iata).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Location.Longitude).NotEmpty();
            RuleFor(x => x.Location.Latitude).NotEmpty();
        }

    }
}
