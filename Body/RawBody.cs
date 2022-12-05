using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace HttpClient
{
    public enum BodyContentType
    {
        Text = 0,
        JavaScript = 1,
        JSON = 2,
        HTML = 3,
        XML = 4
    }

    public class RawBody : HttpRequestBody
    {
        private StringContent _content;

        public RawBody(string content, BodyContentType type) : base()
        {
            string contentType = "text/plain";
            switch (type)
            {
                case BodyContentType.Text:
                    contentType = "text/plain";
                    break;
                case BodyContentType.JavaScript:
                    contentType = "application/javascript";
                    break;
                case BodyContentType.JSON:
                    contentType = "application/json";
                    break;
                case BodyContentType.HTML:
                    contentType = "text/html";
                    break;
                case BodyContentType.XML:
                    contentType = "application/xml";
                    break;
                default:
                    break;
            }
            _content = new StringContent(content, Encoding.UTF8, contentType);
        }

        internal override HttpContent Content => _content;
    }
}
