using AudioTextR.Core.Abstractions;
using AudioTextR.Core.Services;
using NUnit.Framework;
using AudioTextR.Core.Extensions;
using AudioTextR.Core.Models;
using System;

namespace AudioTestR.UnitTests
{
    [TestFixture]
    public class SpeechServiceTests
    {
        private readonly ISpeechService _speechService;

        public SpeechServiceTests()
        {
            var recognizeApi = new WitAiModel
            {
                ServerToken = "apikey",
                BaseAddress = new Uri("https://api.wit.ai/")
            };
            _speechService = new SpeechService(HttpClientHelper.Create(recognizeApi));
        }

        [Test]
        public void RecognizeAudio_ShouldText()
        {
            // arrange
            var recognizedText = "ночь улица фонарь аптека бессмысленный и тусклый свет живи ещё хоть четверть века все будет так исхода нет";
            var binaryWav = "";

            // act
            var actual = _speechService.Recognize(binaryWav);

            // assert
            Assert.AreEqual(recognizedText, actual);
        }
    }
}
