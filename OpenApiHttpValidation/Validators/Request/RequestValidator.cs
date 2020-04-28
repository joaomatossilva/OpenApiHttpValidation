namespace OpenApiHttpValidation.Validators.Request
{
    using System.Linq;
    using Components;
    using Microsoft.OpenApi.Models;

    public class RequestValidator : IValidator
    {
        private readonly IValidator[] validators = new IValidator[] 
        {
            new UrlValidator(),
            new RequestHeadersValidator(),
            new RequestContentTypeValidator(),
            new RequestBodyValidator(),
            new ResponseContentTypeValidator(),
            new ResponseBodyValidator(),
            new ResponseStatusCodeValidator()
        };

        public ValidationResult Validate(OpenApiOperation operation, HttpMessage httpMessage)
        {
            var results = validators.Select(x => x.Validate(operation, httpMessage)).ToList();
            return new AggregateValidationResult(results);
        }

    }
}
