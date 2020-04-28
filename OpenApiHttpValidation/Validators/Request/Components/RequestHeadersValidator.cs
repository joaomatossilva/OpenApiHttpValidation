namespace OpenApiHttpValidation.Validators.Request.Components
{
    using Microsoft.OpenApi.Models;

    public class RequestHeadersValidator : IValidator
    {
        public ValidationResult Validate(OpenApiOperation operation, HttpMessage httpMessage)
        {
            // validate id the request message has every required header

            return ValidationResult.Ok();
        }
    }
}
