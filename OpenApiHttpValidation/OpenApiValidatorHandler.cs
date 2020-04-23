using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace OpenApiHttpValidation
{
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
            var operationType = ToOperationType(request.Method);

            foreach (var openApiPath in _openApiDocument.Paths)
            {
                if (IsUrlMatch(url, openApiPath.Key))
                {
                    if (!openApiPath.Value.Operations.ContainsKey(operationType))
                    {
                        continue;
                    }
                    var openApiOperation = openApiPath.Value.Operations[operationType];

                    //validate parameters

                    //validate responses

                    return response;
                }
            }

            throw new Exception("operation not present on the api specs");
        }

        private bool IsUrlMatch(string path, string openApiPath)
        {
            //Very, I mean Very! naive validation of the path
            var cleanOpenApiPath = Regex.Replace(openApiPath, "{.*}", @"\d|(^([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})$)");
            var match = Regex.Match(path, cleanOpenApiPath);
            return match.Success;
        }

        private OperationType ToOperationType(HttpMethod method) => 
            (OperationType)Enum.Parse(typeof(OperationType), method.Method, true);
    }
}
