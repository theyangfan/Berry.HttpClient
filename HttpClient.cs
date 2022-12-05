using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys = System.Net.Http;

namespace HttpClient
{
    public enum HttpMethod
    {
        GET = 0,
        POST = 1,
        PUT = 2,
        DELETE = 3
    }

    public class HttpClient
    {
        protected static readonly Sys.HttpClient _client = new Sys.HttpClient();

        protected HttpMethod _method = HttpMethod.GET;
        protected string _url = string.Empty;
        private string _param_str = string.Empty;
        private Sys.HttpContent _body;

        public HttpClient(string url)
        {
            _url = url;
        }

        public HttpClient(HttpMethod method, string url)
        {
            _method = method;
            _url = url;
        }

        public HttpMethod Method
        {
            get => _method;
            set => _method = value;
        }

        public void AddQueryParameter(string key, object value)
        {
            if (string.IsNullOrEmpty(_param_str))
                _param_str += $"{key}={value}";
            else
                _param_str += $"&{key}={value}";
        }

        public void AddHeader(string name, string value)
        {
            _client.DefaultRequestHeaders.Add(name, value);
        }

        public void SetBody(HttpRequestBody body)
        {
            _body = body.Content;
        }

        public async Task<string> SendAsync()
        {
            try
            {
                string url = !string.IsNullOrEmpty(_param_str) ? $"{_url}?{_param_str}" : _url;
                switch (_method)
                {
                    case HttpMethod.GET:
                        return await _client.GetStringAsync(url);
                    case HttpMethod.POST:
                        Sys.HttpResponseMessage response = await _client.PostAsync(url, _body);
                        response.EnsureSuccessStatusCode();
                        return await response.Content.ReadAsStringAsync();
                    case HttpMethod.PUT:
                        response = await _client.PutAsync(url, _body);
                        response.EnsureSuccessStatusCode();
                        return await response.Content.ReadAsStringAsync();
                    case HttpMethod.DELETE:
                        Sys.HttpRequestMessage request = new Sys.HttpRequestMessage(Sys.HttpMethod.Delete, url) { Content = _body };
                        response = await _client.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        return await response.Content.ReadAsStringAsync();
                    default:
                        break;
                }
                return string.Empty;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<int> DownloadAsync(string filename)
        {
            try
            {
                byte[] bytes = await _client.GetByteArrayAsync(_url);

                Directory.CreateDirectory(new FileInfo(filename).DirectoryName);

                if (File.Exists(filename)) File.Delete(filename);

                using (FileStream file = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    file.Write(bytes, 0, bytes.Length);
                }
                return bytes.Length;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }

    
}
