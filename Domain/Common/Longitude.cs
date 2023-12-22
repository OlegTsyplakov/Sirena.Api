using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace Sirena.Api.Domain.Common
{
    public class Longitude : ValueOf<double, Longitude>
    {
        protected override void Validate()
        {
            if (Value==0)
            {
                const string message = "Longitude is zero";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(Name), message)
            });
            }
        }

    }
}
