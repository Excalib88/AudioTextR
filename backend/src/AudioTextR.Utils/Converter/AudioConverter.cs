using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace AudioTextR.Utils.Converter
{
    public class AudioConverter : IAudioConverter
    {
        public Stream FromOggToMp3(string path)
        {
            var finalPath = path.Replace(".ogg", ".mp3");
            Process.Start("./ffmpeg.exe", string.Format($" -i {path} {finalPath}")).WaitForExit();

            return new FileStream(finalPath, FileMode.Open, FileAccess.Read);
        }

        public Stream FromOggToWav(string path)
        {
            var finalPath = path.Replace(".ogg", ".wav");
            Process.Start("./ffmpeg.exe", string.Format($" -i {path} {finalPath}")).WaitForExit();

            return new FileStream(finalPath, FileMode.Open, FileAccess.Read);
        }
    }
}
