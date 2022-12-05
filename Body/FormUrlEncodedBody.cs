using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace HttpClient
{
    public class FormUrlEncodedBody : HttpRequestBody
    {
        private Dictionary<string, string> _pairs = new Dictionary<string, string>();

        internal override HttpContent Content => new FormUrlEncodedContent(_pairs);

        public void AddParameter(string key, string value)
        {
            _pairs.Add(key, value); 
        }
    }
}
