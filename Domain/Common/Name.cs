using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace Sirena.Api.Domain.Common
{
    public class Name : ValueOf<string, Name>
    {
        protected override void Validate()
        {
            if (Value == string.Empty)
            {
                const string message = "Airport name cannot be empty";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(Name), message)
            });
            }
        }

    }
}
