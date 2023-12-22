using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace Sirena.Api.Domain.Common
{
    public class Latitude : ValueOf<double, Latitude>
    {
        protected override void Validate()
        {
            if (Value == 0)
            {
                const string message = "Latitude is zero";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(Name), message)
            });
            }
        }
    }
}
