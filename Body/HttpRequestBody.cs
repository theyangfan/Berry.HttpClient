using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace HttpClient
{
    public abstract class HttpRequestBody
    {
        internal abstract HttpContent Content { get; }
    }
}
