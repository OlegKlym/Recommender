using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Recommender.Core.Models;

namespace Recommender.API.Services
{
    public interface IApiRepository
    {
        Task<IResponseData<T>> MakePostRequest<T>(object request, string url, CancellationToken? cancellationToken = null);
        Task<IResponseData<T>> MakeGetRequest<T>(string url, CancellationToken? cancellationToken = null);
        Task<IResponseData<T>> MakePutRequest<T>(object request, string url, CancellationToken? cancellationToken = null);
        Task<IResponseData<T>> MakeDeleteRequest<T>(string url, CancellationToken? cancellationToken = null);
    }

    public class ApiRepository : IApiRepository
    {
        private readonly ApiLoggingService _loggingService;

        public HttpClient HttpClient { get; }

        public ApiRepository(ApiLoggingService loggingService)
        {
            HttpClient = new HttpClient();

            _loggingService = loggingService;
        }

        public Task<IResponseData<T>> MakePostRequest<T>(object request, string url, CancellationToken? cancellationToken = null)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var uri = MakeRequestUri(url);
            var content = JsonConvert.SerializeObject(request);

            return SendRequest<T>(uri, HttpMethod.Post, content, cancellationToken);
        }

        public Task<IResponseData<T>> MakeGetRequest<T>(string url, CancellationToken? cancellationToken = null)
        {
            return SendRequest<T>(MakeRequestUri(url), HttpMethod.Get, null, cancellationToken);
        }

        public Task<IResponseData<T>> MakePutRequest<T>(object request, string url, CancellationToken? cancellationToken = null)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var uri = MakeRequestUri(url);
            var content = JsonConvert.SerializeObject(request);

            return SendRequest<T>(uri, HttpMethod.Put, content, cancellationToken);
        }

        public Task<IResponseData<T>> MakeDeleteRequest<T>(string url, CancellationToken? cancellationToken = null)
        {
            var uri = MakeRequestUri(url);

            return SendRequest<T>(uri, HttpMethod.Delete, null, cancellationToken);
        }

        private Uri MakeRequestUri(string url)
        {
            if(Uri.TryCreate(Constants.BASE_URL + url, UriKind.Absolute, out var uri))
            {
                return uri;
            }

            throw new ArgumentException("The argument '{0}' cannot be null or empty.", nameof(url));
        }

        private async Task<IResponseData<T>> SendRequest<T>(
            Uri uri,
            HttpMethod httpMethod,
            string content,
            CancellationToken? cancellationToken)
        {
            if(HttpClient == null)
            {
                throw new ArgumentNullException(nameof(HttpClient));
            }

            var request = CreateHttpRequestMessage(uri, httpMethod, content);

            _loggingService.LogRequestInfo(uri, httpMethod, content);

            var stopwatch = Stopwatch.StartNew();
            var response = await SendHttpRequest(request, cancellationToken).ConfigureAwait(false);
            var responseContent = await GetResponseContent(response).ConfigureAwait(false);
            stopwatch.Stop();

            _loggingService.LogResponseInfo(uri, stopwatch.Elapsed, response, responseContent);

            if (response.IsSuccessStatusCode)
            {
                return new ResponseData<T>
                {
                    StatusCode = (int)response.StatusCode,
                    Data = JsonConvert.DeserializeObject<T>(responseContent)
                };
            }

            return new ResponseData<T>
            {
                StatusCode = (int)response.StatusCode,
                ErrorMessage = response.ReasonPhrase
            };
        }

        private HttpRequestMessage CreateHttpRequestMessage(Uri uri, HttpMethod httpMethod, string content)
        {
            return new HttpRequestMessage(httpMethod, uri)
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };
        }

        private async Task<HttpResponseMessage> SendHttpRequest(HttpRequestMessage request, CancellationToken? cancellationToken)
        {
            if (cancellationToken == null)
            {
                return await HttpClient.SendAsync(request).ConfigureAwait(false);
            }

            return await HttpClient.SendAsync(request, cancellationToken.Value).ConfigureAwait(false);
        }

        private async Task<string> GetResponseContent(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}
