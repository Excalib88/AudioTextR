using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AudioTextR.Utils.Converter
{
    public interface IAudioConverter
    {
        Task<byte[]> FromM4aToWav();
        Task<byte[]> FromM4aToMp3();
    }
}
