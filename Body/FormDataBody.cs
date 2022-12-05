using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace HttpClient
{
    public class FormDataBody : HttpRequestBody
    {
        private MultipartFormDataContent _content;

        public FormDataBody() : base()
        {
            _content = new MultipartFormDataContent();
            // 去除boundary两边的引号
            //string boundaryValue = _content.Headers.ContentType.Parameters.Where(h => h.Name == "boundary").FirstOrDefault().Value;
            //_content.Headers.ContentType.Parameters.Where(h => h.Name == "boundary").FirstOrDefault().Value = boundaryValue.Replace("\"", "");
        }

        internal override HttpContent Content => _content;

        public void AddParameter(string key, string value)
        {
            StringContent param = new StringContent(value, Encoding.UTF8);
            _content.Add(param, $"\"{key}\"");
        }

        public void AddFile(string name, string filename)
        {
            ByteArrayContent file = new ByteArrayContent(File.ReadAllBytes(filename));
            file.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = $"\"{name}\"",
                FileName = $"\"{new FileInfo(filename).Name}\""
            };
            string ext = new FileInfo(filename).Extension;
            if (ext == ".json")
            {
                file.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
            else if (ext == ".zip")
            {
                file.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
            }
            else if (ext == ".docx")
            {
                file.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            }
            else if (ext == ".jpg" || ext == ".jpeg")
            {
                file.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            }
            else if (ext == ".png")
            {
                file.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            }
            else
            {
                file.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            }
            _content.Add(file);
        }

    }
}
