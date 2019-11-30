using AudioTextR.Core.Abstractions.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AudioTextR.Core.Extensions
{
    public static class HttpClientHelper
    {
        public static HttpClient Create(IRecognizeApiModel recognizeApi)
        {
            var httpClient = new HttpClient();

            httpClient.BaseAddress = recognizeApi.BaseAddress;
            httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", recognizeApi.ServerToken);

            return httpClient;
        }
    }
}
