using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AudioTextR.Utils.Converter
{
    public interface IAudioConverter
    {
        Task<byte[]> FromOggToMp3();
        Stream FromOggToWav(string path);
    }
}
