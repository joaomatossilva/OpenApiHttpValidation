using System;

namespace OpenApiHttpValidation.Tests
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using NUnit.Framework;

    class UrlValidationTests : BaseTest
    {
        [Test]
        public async Task ValidRequestShouldPass()
        {
            var httpClient = new HttpClient(new OpenApiValidatorHandler(Document, new OkHttpHandler()));
            await httpClient.PostAsync("http://localhost/entity/1", new StringContent("test"));
            Assert.Pass();
        }

        [Test]
        public async Task InvalidMethodRequestShouldThrowException()
        {
            var httpClient = new HttpClient(new OpenApiValidatorHandler(Document, new OkHttpHandler()));
            Assert.ThrowsAsync<Exception>(async () => await httpClient.GetAsync("http://localhost/entity/1"));
        }

        [Test]
        public async Task InvalidPathRequestShouldThrowException()
        {
            var httpClient = new HttpClient(new OpenApiValidatorHandler(Document, new OkHttpHandler()));
            Assert.ThrowsAsync<Exception>(async () => await httpClient.PostAsync("http://localhost/someother/1", new StringContent("test")));
        }
    }
}
