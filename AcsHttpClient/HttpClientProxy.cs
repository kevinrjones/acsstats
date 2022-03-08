using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AcsStatsWeb.AcsHttpClient;
using AcsTypes.Error;
using AcsTypes.Json;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AcsHttpClient
{

    public class HttpClientProxy : IHttpClientProxy
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HttpClientProxy> _logger;
        private readonly string _baseUrl;

        public HttpClientProxy(ILogger<HttpClientProxy> logger, HttpClient client, IConfiguration configuration)
        {
            _logger = logger;
            _client = client;
            _configuration = configuration;

            _baseUrl = configuration.GetValue<String>("BaseServicesUrl");

            _logger.LogDebug("Created proxy");
        }

        //todo: Process envelope correctly
        public async Task<Result<T, Error>> GetJsonAsync<T>(string uri) where T : class, new()
        {
            var completeUrl = $"{_baseUrl}/{uri}";
            try
            {
                Envelope<T> res = await _client.GetFromJsonAsync<Envelope<T>>(completeUrl);
                return string.IsNullOrEmpty(res.ErrorMessage) ? res.Result : Result.Failure<T, Error>(res.ErrorMessage);
            }
            catch (Exception e) // Invalid JSON or non-success code
            {
                _logger.LogError(e, "Error calling remote server with: {Url}", uri);
                return  Result.Failure<T, Error>(Errors.HttpError(e));
            }
        }
    }
}