using FluentValidation;
using Sirena.Api.Contracts.Requests;
using Sirena.Api.Contracts.Responses;

namespace Sirena.Api.Validation
{
    public class AirportRequestValidator : AbstractValidator<AirportRequest>
    {
        public AirportRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty().NotNull().Must(y=>y.Length==3);

        }
    }
}
