using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Sirena.Api.Contracts.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace Sirena.Api.Validation
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _request;

        public ValidationExceptionMiddleware(RequestDelegate request)
        {
            _request = request;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (ValidationException exception)
            {
                context.Response.StatusCode = 400;
                var messages = exception.Errors.Select(x => x.ErrorMessage).ToList();
                var validationFailureResponse = new ValidationFailureResponse
                {
                    Errors = messages
                };


                string jsonStr = JsonConvert.SerializeObject(validationFailureResponse);

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(jsonStr);
            }
        }
    }
}
