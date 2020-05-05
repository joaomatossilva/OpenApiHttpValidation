namespace OpenApiHttpValidation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class HttpMessageAdapter : HttpMessage
    {
        private readonly HttpResponseMessage response;
        private readonly HttpRequestMessage request;

        public HttpMessageAdapter(HttpResponseMessage response, HttpRequestMessage request)
        {
            this.response = response;
            this.request = request;
        }

        public override Task<string> RequestPath => Task.FromResult(request.RequestUri.AbsolutePath);

        public override Task<string> RequestMethod => Task.FromResult(request.Method.Method);

        public override Task<string> RequestBody => request.Content.ReadAsStringAsync();

        public override Task<IDictionary<string, string>> RequestHeaders =>
            Task.FromResult((IDictionary<string, string>)request.Headers.ToDictionary(x => x.Key, x => string.Join(",", x.Value)));


        public override Task<int> ResponseStatusCode => Task.FromResult((int)response.StatusCode);

        public override Task<string> ResponseBody => response.Content.ReadAsStringAsync();

        public override Task<IDictionary<string, string>> ResponseHeaders
        {
            get
            {
                var responseHeaders = response.Headers.ToDictionary(x => x.Key, x => string.Join(",", x.Value));
                foreach (var httpContentHeader in response.Content.Headers)
                {
                    if (!responseHeaders.ContainsKey(httpContentHeader.Key))
                    {
                        responseHeaders.Add(httpContentHeader.Key, string.Join(",", httpContentHeader.Value));
                    }
                }

                return Task.FromResult<IDictionary<string, string>>(responseHeaders);
            }
        }
    }
}
