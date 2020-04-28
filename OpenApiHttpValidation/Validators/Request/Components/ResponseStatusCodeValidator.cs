namespace OpenApiHttpValidation.Validators.Request.Components
{
    using Microsoft.OpenApi.Models;

    public class ResponseStatusCodeValidator : IValidator
    {
        public ValidationResult Validate(OpenApiOperation operation, HttpMessage httpMessage)
        {
            // validate the status code
            var statusCode = httpMessage.ResponseStatusCode.Result;

            if (!operation.Responses.ContainsKey(statusCode.ToString()))
            {
                return ValidationResult.WithError($"{statusCode} operation {operation.OperationId}");
            }

            return ValidationResult.Ok();
        }
    }
}
