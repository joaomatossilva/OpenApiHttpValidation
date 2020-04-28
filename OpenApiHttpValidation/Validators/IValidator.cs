namespace OpenApiHttpValidation.Validators
{
    using Microsoft.OpenApi.Models;

    public interface IValidator
    {
        ValidationResult Validate(OpenApiOperation operation, HttpMessage httpMessage);
    }
}