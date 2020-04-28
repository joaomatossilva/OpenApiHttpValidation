namespace OpenApiHttpValidation.Validators.Request.Components
{
    using Microsoft.OpenApi.Models;

    public class ResponseContentTypeValidator : IValidator
    {
        public ValidationResult Validate(OpenApiOperation operation, HttpMessage httpMessage)
        {
            // validate content type

            return ValidationResult.Ok();
        }
    }
}
