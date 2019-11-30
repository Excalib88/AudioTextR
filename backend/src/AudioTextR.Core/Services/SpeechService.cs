using AudioTextR.Core.Abstractions;
using AudioTextR.Core.Abstractions.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AudioTextR.Core.Services
{
    public class SpeechService : ISpeechService
    {
        private readonly HttpClient _httpClient;

        public SpeechService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IRecognizeResponse> Recognize(string audioPath)
        {
            using (var fs = File.OpenRead(audioPath))
            {
                var content = new StreamContent(fs);
                content.Headers.ContentType = new MediaTypeHeaderValue("audio/wav");
                var response = await _httpClient.PostAsync("speech?v=20170307", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IRecognizeResponse>(responseContent);
            }
        }

        public Task<IRecognizeResponse> Recognize(byte[] binaryAudio)
        {
            throw new NotImplementedException();
        }

        public async Task<IRecognizeResponse> Recognize(Stream audioStream)
        {
            var content = new StreamContent(audioStream);
            content.Headers.ContentType = new MediaTypeHeaderValue("audio/wav");
            var response = await _httpClient.PostAsync("speech?v=20170307", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IRecognizeResponse>(responseContent);
        }

        public Task<T> Synthesize<T>(string text)
        {
            throw new NotImplementedException();
        }
    }
}
