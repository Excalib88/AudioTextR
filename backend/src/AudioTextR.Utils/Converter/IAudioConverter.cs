using System.IO;

namespace AudioTextR.Utils.Converter
{
    public interface IAudioConverter
    {
        Stream FromOggToMp3(string path);
        Stream FromOggToWav(string path);
    }
}
