using FluentValidation;
using FluentValidation.Results;
using System;
using ValueOf;

namespace Sirena.Api.Domain.Common
{
    public class IataCode : ValueOf<string, IataCode>
    {

        protected override void Validate()
        {
            if (Value == string.Empty)
            {
                const string message = "Iata code cannot be empty";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(IataCode), message)
            });
            }
        }

    }
}
