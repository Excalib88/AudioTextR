﻿using AudioTextR.Core.Abstractions;
using AudioTextR.Core.Extensions;
using AudioTextR.Core.Models;
using AudioTextR.Core.Services;
using AudioTextR.Utils.Converter;
using MihaZupan;
using System;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AudioTextR.Sample.ConsoleTelegramBot
{
    class Program
    {
        private static TelegramBotClient bot;

        static void Main(string[] args)
        {
            var socksProxy = new HttpToSocks5Proxy("212.109.223.75", 8083);

            bot = new TelegramBotClient("907186419:AAHQrTqp3uN9Dpz_IjZbiDleWpDFDyX9re4", socksProxy);

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
                            var wavStream = converter.FromOggToWav(fs);
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
