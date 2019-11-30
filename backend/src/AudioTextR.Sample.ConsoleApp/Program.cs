using AudioTextR.Core.Abstractions;
using AudioTextR.Core.Extensions;
using AudioTextR.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AudioTextR.Sample.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddAudioTextR(new WitAiModel(new Uri("https://api.wit.ai/"), "4UUIDDVP77PN2WR6P2AMQ2H43V7YHXTR"))
                .BuildServiceProvider();

            var result = await serviceProvider.GetService<ISpeechService>().Recognize("Recording (2).wav");
            Console.WriteLine(result.Text);
            Console.ReadKey();
        }
    }
}
