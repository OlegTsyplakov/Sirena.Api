using FluentValidation;
using Sirena.Api.Contracts.Responses;

namespace Sirena.Api.Validation
{
    public class MilesValidator : AbstractValidator<MilesResponse>
    {
        public MilesValidator()
        {
            RuleFor(x => x.Miles).Must(y => y > 0).WithMessage("Miles shoud be greater than 0.");
        }
    }
}
