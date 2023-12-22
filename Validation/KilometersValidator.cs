using FluentValidation;
using Sirena.Api.Contracts.Responses;

namespace Sirena.Api.Validation
{
    public class KilometersValidator : AbstractValidator<KilometersResponse>
    {
        public KilometersValidator()
        {
            RuleFor(x => x.Kilometers).Must(y => y > 0);
        }
    }
}
