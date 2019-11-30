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
        Task<IRecognizeResponse> Recognize(string audioPath);
        Task<IRecognizeResponse> Recognize(byte[] binaryAudio);
        Task<IRecognizeResponse> Recognize(Stream audioStream);

        Task<T> Synthesize<T>(string text);
    }
}
