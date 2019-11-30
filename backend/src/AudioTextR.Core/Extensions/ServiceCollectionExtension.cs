using AudioTextR.Core.Abstractions;
using AudioTextR.Core.Abstractions.Models;
using AudioTextR.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace AudioTextR.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAudioTextR(this IServiceCollection serviceCollection, 
            IRecognizeApiModel recognizeApiModel)
        {
            return serviceCollection
                .AddScoped(sp => HttpClientHelper.Create(recognizeApiModel))
                .AddScoped<ISpeechService, SpeechService>();
        }
    }
}
