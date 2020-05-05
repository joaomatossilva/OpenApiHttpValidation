using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OpenApiHttpValidation.Tests
{
    using System.Text;

    public class OkHttpHandler : HttpMessageHandler
    {
        private readonly string content;
        private readonly string contentType;

        public OkHttpHandler(string content = "OK", string contentType = "text/plain")
        {
            this.content = content;
            this.contentType = contentType;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(content, Encoding.UTF8, contentType),
            };
            return Task.FromResult(response);
        }
    }
}
