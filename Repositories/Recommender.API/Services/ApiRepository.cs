using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Recommender.Core.Models;
using Recommender.API.Models;

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
        private readonly int _timeoutDurationInSeconds = 60;

        private readonly ApiLoggingService _loggingService;
        private readonly IHttpClientService _httpClientService;

        public ApiRepository(
            ApiLoggingService loggingService,
            IHttpClientService httpClientService)
        {
            _loggingService = loggingService;
            _httpClientService = httpClientService;
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

            throw new ArgumentException($"The argument '{nameof(url)}' cannot be null or empty.");
        }

        private async Task<IResponseData<T>> SendRequest<T>(
            Uri uri,
            HttpMethod httpMethod,
            string content,
            CancellationToken? cancellationToken)
        {
            var request = new HttpRequestMessageFactory().CreateHttpRequestMessage(uri, httpMethod, content);

            var timeoutInfo = new TimeoutInfo
            {
                Uri = uri,
                Timeout = TimeSpan.FromSeconds(_timeoutDurationInSeconds)
            };

            HttpResponseMessage response;
            var responseContent = string.Empty;

            using (var timer = new ExecutionTimer())
            {
                _loggingService.LogRequestInfo(uri, httpMethod, content);

                response = await ExecuteWithTimeoutAsync(request, timeoutInfo, cancellationToken);
                responseContent = await GetResponseContent(response);

                _loggingService.LogResponseInfo(uri, timer.Elapsed, response, responseContent);
            }

            return response.IsSuccessStatusCode
                ? new ResponseData<T>
                {
                    StatusCode = (int)response.StatusCode,
                    Data = JsonConvert.DeserializeObject<T>(responseContent)
                }
                : new ResponseData<T>
                {
                    StatusCode = (int)response.StatusCode,
                    ErrorMessage = response.ReasonPhrase
                };
        }

        private async Task<HttpResponseMessage> ExecuteWithTimeoutAsync(HttpRequestMessage request, TimeoutInfo timeoutInfo, CancellationToken? cancellationToken)
        {
            return await _httpClientService
                .ExecuteWithTimeoutAsync(SendHttpRequest(request, cancellationToken), timeoutInfo)
                .ConfigureAwait(false);
        }

        private Task<HttpResponseMessage> SendHttpRequest(HttpRequestMessage request, CancellationToken? cancellationToken)
        {
            return _httpClientService.SendHttpRequest(request, cancellationToken);
        }

        private async Task<string> GetResponseContent(HttpResponseMessage response)
        {
            try
            {
                return await _httpClientService.GetResponseContent(response).ConfigureAwait(false);
            }
            catch (InvalidOperationException ex)
            {
                _loggingService.Error(ex.Message);
                throw;
            }
        }
    }
}