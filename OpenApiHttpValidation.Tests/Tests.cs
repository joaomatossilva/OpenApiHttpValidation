using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using NUnit.Framework;

namespace OpenApiHttpValidation.Tests
{
    public abstract class BaseTest
    {
        protected OpenApiDocument Document { get; private set; }

        [SetUp]
        public void Setup()
        {
            Document = new OpenApiDocument
            {
                Paths = new OpenApiPaths()
                {
                    {
                        "/entity/{id}", new OpenApiPathItem
                        {
                            Operations = new Dictionary<OperationType, OpenApiOperation>()
                            {
                                {
                                    OperationType.Post, new OpenApiOperation()
                                    {
                                        Responses = new OpenApiResponses()
                                        {
                                            { "200", new OpenApiResponse()
                                                {
                                                    Content = new Dictionary<string, OpenApiMediaType>()
                                                    {
                                                        { "text/plain", new OpenApiMediaType() }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

        }
    }
}