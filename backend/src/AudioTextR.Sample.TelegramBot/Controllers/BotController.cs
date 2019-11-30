using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AudioTextR.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AudioTextR.Sample.TelegramBot.Controllers
{
    [Route("bot")]
    [ApiController]
    public class BotController : Controller
    {
        private readonly ITelegramBotClient _client;
        private readonly ISpeechService _speechService;

        public BotController(ITelegramBotClient client, ISpeechService speechService)
        {
            _client = client;
            _speechService = speechService;
        }

        [HttpGet]
        public void Get()
        {
            Console.WriteLine("asdasdsda");
        }

        [HttpPost("update")]
        public async Task Post([FromBody]Update update)
        {
            if (update == null) return;

            var message = update.Message;

            if (message?.Type == MessageType.Audio)
            {
                var text = "";
                if(message.Audio.Duration < 20)
                {
                    var audioMessageId = message.Audio.FileId;

                    using (var audioStream = new MemoryStream())
                    {
                        await _client.GetInfoAndDownloadFileAsync(audioMessageId, new MemoryStream());

                        var result = await _speechService.Recognize(audioStream);
                        text = result.Text;
                    }                        
                }

                await _client.SendTextMessageAsync(message.Chat.Id, text);
            }

        }
    }
}