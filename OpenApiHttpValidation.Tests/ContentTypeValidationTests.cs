using System;

namespace OpenApiHttpValidation.Tests
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using NUnit.Framework;

    class ContentTypeValidationTests : BaseTest
    {

        [Test]
        public async Task InvalidContentTypeRequestShouldThrowException()
        {
            var httpClient = new HttpClient(new OpenApiValidatorHandler(Document, new OkHttpHandler("{\"status\":\"OK\"}", "application/json")));
            Assert.ThrowsAsync<Exception>(async () => await httpClient.GetAsync("http://localhost/entity/1"));
        }

    }
}
