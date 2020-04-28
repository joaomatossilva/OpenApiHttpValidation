using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace OpenApiHttpValidation
{
    using System.Linq;
    using Validators.Request;

    public class OpenApiValidatorHandler : DelegatingHandler
    {
        private readonly OpenApiDocument _openApiDocument;

        public OpenApiValidatorHandler(OpenApiDocument openApiDocument, HttpMessageHandler innerHandler)
        : base(innerHandler)
        {
            _openApiDocument = openApiDocument;
        }

        public OpenApiValidatorHandler(OpenApiDocument openApiDocument)
            : this(openApiDocument, new HttpClientHandler())
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            var url = request.RequestUri.AbsolutePath;
            var operation = _openApiDocument.FindOperation(url, request.Method.Method);
            if (operation == null)
            {
                throw new Exception("operation not present on the api specs");
            }

            var validator = new RequestValidator();
            var validation = validator.Validate(operation, new HttpMessageAdapter(response, request));
            if (validation.Errors.Any())
            {
                throw new Exception("validation failed");
            }

            return response;
        }




    }
}
