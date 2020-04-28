namespace OpenApiHttpValidation.Validators.Request.Components
{
    using Microsoft.OpenApi.Models;

    public class RequestBodyValidator : IValidator
    {
        public ValidationResult Validate(OpenApiOperation operation, HttpMessage httpMessage)
        {
            //validate the request body

            return ValidationResult.Ok();
        }
    }
}
