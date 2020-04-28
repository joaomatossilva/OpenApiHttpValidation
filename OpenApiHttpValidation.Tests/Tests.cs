using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using NUnit.Framework;

namespace OpenApiHttpValidation.Tests
{
    public class Tests
    {
        private OpenApiDocument document;
        private HttpClient httpClient;

        [SetUp]
        public void Setup()
        {
            document = new OpenApiDocument
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
                                }
                            }
                        }
                    }
                }
            };

            httpClient = new HttpClient(new OpenApiValidatorHandler(document, new OkHttpHandler()));
        }

        [Test]
        public async Task ValidRequestShouldPass()
        {
            await httpClient.PostAsync("http://localhost/entity/1", new StringContent("test"));
            Assert.Pass();
        }

        [Test]
        public async Task InvalidMethodRequestShouldThrowException()
        {
            Assert.ThrowsAsync<Exception>(async () => await httpClient.GetAsync("http://localhost/entity/1"));
        }

        [Test]
        public async Task InvalidPathRequestShouldThrowException()
        {
            Assert.ThrowsAsync<Exception>(async () => await httpClient.PostAsync("http://localhost/someother/1", new StringContent("test")));
        }
    }
}