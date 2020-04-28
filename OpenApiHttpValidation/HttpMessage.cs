using System;
using System.Collections.Generic;
using System.Text;

namespace OpenApiHttpValidation
{
    using System.Threading.Tasks;

    public abstract class HttpMessage
    {
        public virtual Task<string> RequestPath => throw new Exception($"{nameof(RequestPath)} value was not provided");
        public virtual Task<string> RequestMethod => throw new Exception($"{nameof(RequestMethod)} value was not provided");
        public virtual Task<string> RequestBody => throw new Exception($"{nameof(RequestBody)} value was not provided");
        public virtual Task<IDictionary<string, string>> RequestHeaders => throw new Exception($"{nameof(RequestHeaders)} value was not provided");

        public virtual Task<int> ResponseStatusCode => throw new Exception($"{nameof(ResponseStatusCode)} value was not provided");
        public virtual Task<string> ResponseBody => throw new Exception($"{nameof(ResponseBody)} value was not provided");
        public virtual Task<IDictionary<string, string>> ResponseHeaders => throw new Exception($"{nameof(ResponseHeaders)} value was not provided");
    }
}
