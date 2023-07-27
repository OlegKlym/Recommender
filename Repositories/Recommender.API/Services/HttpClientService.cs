using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Recommender.API.Models;

namespace Recommender.API.Services
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> SendHttpRequest(HttpRequestMessage request, CancellationToken? cancellationToken);
        Task<string> GetResponseContent(HttpResponseMessage response);
        Task<HttpResponseMessage> ExecuteWithTimeoutAsync(Task<HttpResponseMessage> task, TimeoutInfo timeoutInfo);
    }

    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiLoggingService _loggingService;

        public HttpClientService(
            HttpClient httpClient,
            ApiLoggingService loggingService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _loggingService = loggingService;
        }

        public Task<HttpResponseMessage> SendHttpRequest(HttpRequestMessage request, CancellationToken? cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return cancellationToken.HasValue
                ? _httpClient.SendAsync(request, cancellationToken.Value)
                : _httpClient.SendAsync(request);
        }

        public async Task<string> GetResponseContent(HttpResponseMessage response)
        {
            if (response.Content == null)
            {
                throw new InvalidOperationException("Response content is missing.");
            }

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        public async Task<HttpResponseMessage> ExecuteWithTimeoutAsync(Task<HttpResponseMessage> task, TimeoutInfo timeoutInfo)
        {
            using (var cts = new CancellationTokenSource(timeoutInfo.Timeout))
            {
                var timeoutTask = Task.Delay(timeoutInfo.Timeout, cts.Token);

                var completedTask = await Task.WhenAny(task, timeoutTask) as Task<HttpResponseMessage>;
                if (completedTask == timeoutTask)
                {
                    _loggingService.LogTimeoutInfo(timeoutInfo);

                    return new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.RequestTimeout
                    };
                }

                return await completedTask;
            }
        }
    }
}