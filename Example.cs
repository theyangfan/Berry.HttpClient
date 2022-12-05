using System;
using System.Text;
using System.Threading.Tasks;

namespace HttpClient
{
    internal class Example
    {
        public async static Task GETExample()
        {
            HttpClient client = new HttpClient(HttpMethod.GET, "http://127.0.0.1:8000");
            client.AddQueryParameter("key", "value");
            Console.WriteLine(await client.SendAsync());
        }

        public async static Task POSTFormDataExample()
        {
            HttpClient client = new HttpClient(HttpMethod.POST, "http://127.0.0.1:8000");
            FormDataBody body = new FormDataBody();
            body.AddParameter("key", "value");
            body.AddFile("name", "file.txt");
            client.SetBody(body);
            Console.WriteLine(await client.SendAsync());
        }

        public async static Task POSTFormUrlEncodedExample()
        {
            HttpClient client = new HttpClient(HttpMethod.POST, "http://127.0.0.1:8000");
            FormUrlEncodedBody body = new FormUrlEncodedBody();
            body.AddParameter("key", "value");
            client.SetBody(body);
            Console.WriteLine(await client.SendAsync());
        }

        public async static Task POSTRawDataExample()
        {
            HttpClient client = new HttpClient(HttpMethod.POST, "http://127.0.0.1:8000");
            RawBody body = new RawBody("plain text", BodyContentType.Text);
            client.SetBody(body);
            Console.WriteLine(await client.SendAsync());
        }

        public async static Task DownloadExample()
        {
            HttpClient client = new HttpClient("http://bing.com");
            Console.WriteLine(await client.DownloadAsync(@"D:\result.html"));
        }
    }
}
