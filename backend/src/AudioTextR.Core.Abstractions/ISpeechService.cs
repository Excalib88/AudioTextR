using AudioTextR.Core.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AudioTextR.Core.Abstractions
{
    public interface ISpeechService
    {
        Task<RecognizeResponse> Recognize(string audioPath);
        Task<RecognizeResponse> Recognize(byte[] binaryAudio);
        Task<RecognizeResponse> Recognize(Stream audioStream);

        Task<T> Synthesize<T>(string text);
    }
}
