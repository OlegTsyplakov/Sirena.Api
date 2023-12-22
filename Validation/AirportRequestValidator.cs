using FluentValidation;
using Sirena.Api.Contracts.Requests;

namespace Sirena.Api.Validation
{
    public class AirportRequestValidator : AbstractValidator<AirportRequest>
    {
        public AirportRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty().NotNull().Must(y=>y.Length==3).WithMessage("Airport code should be 3 letters.");
        }
    }
}
