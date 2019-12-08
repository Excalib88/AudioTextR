using AudioTextR.Core.Extensions;
using AudioTextR.Core.Models;
using AudioTextR.Core.Services;
using AudioTextR.Utils.Converter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using MihaZupan;
using System;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace AudioTextR.Sample.ConsoleTelegramBot
{
    class Program
    {
        private static TelegramBotClient bot;

        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var socksProxy = new HttpToSocks5Proxy(configuration["Proxy:Server"], int.Parse(configuration["Proxy:Port"]));

            bot = new TelegramBotClient(configuration["TelegramApi:Token"], socksProxy);

            StartBot();
            
            Console.ReadLine();
        }

        static void StartBot()
        {
            bot.OnUpdate += OnUpdate;
            bot.StartReceiving();
        }

        public static async void OnUpdate(object sender, UpdateEventArgs e)
        {
            var witAi = new WitAiModel
            {
                BaseAddress = new Uri("https://api.wit.ai/"),
                ServerToken = "4UUIDDVP77PN2WR6P2AMQ2H43V7YHXTR"
            };
            var speechService = new SpeechService(HttpClientHelper.Create(witAi));

            if (e.Update == null) return;

            var message = e.Update.Message;

            if (message?.Type == MessageType.Voice)
            {
                var text = "";
                if (message.Voice.Duration < 20)
                {
                    var audioMessageId = message.Voice.FileId;

                    using (var oggStream = new MemoryStream())
                    {
                        var file = await bot.GetInfoAndDownloadFileAsync(audioMessageId, oggStream);
                        var fileName = file.FileId + ".ogg";
                        await System.IO.File.WriteAllBytesAsync(fileName, oggStream.GetBuffer());

                        var converter = new AudioConverter();
                        using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                        {
                            var wavStream = converter.FromOggToWav(fileName);
                            var result = await speechService.Recognize(wavStream);
                            text = result.Text;
                        }
                    }
                }

                text = text == null || text == "" ? "unrecognized" : text;

                await bot.SendTextMessageAsync(message.Chat.Id, text);
            }
        }
    }
}
