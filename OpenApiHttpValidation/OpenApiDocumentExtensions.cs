namespace OpenApiHttpValidation
{
    using System;
    using System.Text.RegularExpressions;
    using Microsoft.OpenApi.Models;

    public static class OpenApiDocumentExtensions
    {
        public static OpenApiOperation FindOperation(this OpenApiDocument openApiDocument, string url, string method)
        {
            var operationType = ToOperationType(method);
            foreach (var openApiPath in openApiDocument.Paths)
            {
                if (!IsUrlMatch(url, openApiPath.Key))
                {
                    continue;
                }

                if (!openApiPath.Value.Operations.ContainsKey(operationType))
                {
                    continue;
                }

                return openApiPath.Value.Operations[operationType];
            }

            return null;
        }

        private static bool IsUrlMatch(string path, string openApiPath)
        {
            //Very, I mean Very! naive validation of the path
            var cleanOpenApiPath = Regex.Replace(openApiPath, "{.*}", @"\d|(^([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})$)");
            var match = Regex.Match(path, cleanOpenApiPath);
            return match.Success;
        }

        private static OperationType ToOperationType(string method) =>
            (OperationType)Enum.Parse(typeof(OperationType), method, true);
    }
}
