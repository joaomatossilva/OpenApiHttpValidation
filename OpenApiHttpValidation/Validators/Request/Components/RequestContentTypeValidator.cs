namespace OpenApiHttpValidation.Validators.Request.Components
{
    using Microsoft.OpenApi.Models;

    public class RequestContentTypeValidator : IValidator
    {
        public ValidationResult Validate(OpenApiOperation operation, HttpMessage httpMessage)
        {
            //validate if the request sent content-type

            return ValidationResult.Ok();
        }
    }
}
