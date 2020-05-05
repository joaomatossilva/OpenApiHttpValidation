namespace OpenApiHttpValidation.Validators.Request.Components
{
    using System;
    using Microsoft.OpenApi.Models;

    public class ResponseContentTypeValidator : IValidator
    {
        public ValidationResult Validate(OpenApiOperation operation, HttpMessage httpMessage)
        {
            var statusCode = httpMessage.ResponseStatusCode.Result;

            var response = operation.Responses[statusCode.ToString()];
            var contentTypes = response?.Content.Keys;
            if (contentTypes == null || contentTypes.Count == 0)
            {
                //there are no content Types to validate on the openApiOperation
                return ValidationResult.Ok();
            }

            var responseContentType = httpMessage.ResponseHeaders.Result.ContainsKey("Content-Type") ?
                httpMessage.ResponseHeaders.Result["Content-Type"] :
                null;
            if (string.IsNullOrEmpty(responseContentType))
            {
                return ValidationResult.WithError($"No Content-Type found on response for operation {operation.OperationId}");
            }

            //cleanup content-type
            var indexOfSemicolon = responseContentType.IndexOf(";", StringComparison.Ordinal);
            if (indexOfSemicolon > 0)
            {
                responseContentType = responseContentType.Substring(0, indexOfSemicolon);
            }

            if (!contentTypes.Contains(responseContentType))
            {
                return ValidationResult.WithError($"Invalid Content-Type {responseContentType} found on response for operation {operation.OperationId}");
            }

            return ValidationResult.Ok();
        }
    }
}
