using FluentValidation;
using Sirena.Api.Contracts.Responses;

namespace Sirena.Api.Validation
{
    public class AirportResponseValidator : AbstractValidator<AirportResponse>
    {

        public AirportResponseValidator()
        {
            RuleFor(x => x.Iata).NotEmpty().WithMessage("Iata code should not be empty.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Airport name should not be empty.");
            RuleFor(x => x.Location.Longitude).NotEmpty().WithMessage("Longitude should not be empty.");
            RuleFor(x => x.Location.Latitude).NotEmpty().WithMessage("Latitude should not be empty.");
        }

    }
}
