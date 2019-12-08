using NAudio.Vorbis;
using NAudio.Wave;
using NVorbis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AudioTextR.Utils.Converter
{
    public class AudioConverter : IAudioConverter
    {
        public Task<byte[]> FromOggToMp3()
        {
            throw new NotImplementedException();
        }

        public Stream FromOggToWav(string path)
        {
            var finalPath = path.Replace(".ogg", ".wav");
            Process.Start("./ffmpeg.exe", string.Format($" -i {path} {finalPath}")).WaitForExit();

            return new FileStream(finalPath, FileMode.Open, FileAccess.Read);

            //using (var vorbis = new VorbisWaveReader("damir123.ogg"))
            //{
            //    WaveFileWriter.CreateWaveFile("damir.wav", vorbis);

            //    using (var fs = new FileStream("damir.wav", FileMode.Open, FileAccess.Read))
            //    {
            //        return fs;
            //    }
            //}
        }
    }
}
