using System;
using System.Net.Http;
using System.Text;

namespace Recommender.API.Services
{
    public class HttpRequestMessageFactory
    {
        public HttpRequestMessage CreateHttpRequestMessage(Uri uri, HttpMethod httpMethod, string content)
        {
            return new HttpRequestMessage(httpMethod, uri)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
        }
    }
}
