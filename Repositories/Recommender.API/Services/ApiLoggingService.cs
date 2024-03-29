﻿using System;
using System.Net.Http;
using Recommender.Core.Services;

namespace Recommender.API.Services
{
    public class ApiLoggingService
    {
        private readonly ILoggingService _loggingService;

        public ApiLoggingService(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public void Info(string message)
        {
            _loggingService.Info(message);
        }

        public void LogRequestInfo(Uri uri, HttpMethod httpMethod, string content)
        {
            _loggingService.Info($"[API] Requesting {httpMethod} of {uri} {content}");
        }

        public void LogResponseInfo(Uri uri, TimeSpan elapsedTime, HttpResponseMessage response, string responseContent)
        {
            _loggingService.Info($"[API] Complete {uri}, was running for {elapsedTime}");
            _loggingService.Info($"[API] {(int)response.StatusCode}({response.StatusCode})| {uri} {responseContent}");
        }
    }
}
