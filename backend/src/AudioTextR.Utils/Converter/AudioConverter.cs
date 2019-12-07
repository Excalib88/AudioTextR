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

        public Stream FromOggToWav(Stream stream)
        {
            
            //Process.Start("./ffmpeg.ex..e", string.Format(" -i {0} {1}", @"D:\Project\AudioTextR\backend\src\AudioTextR.Sample.ConsoleTelegramBot\bin\Debug\netcoreapp2.2\file1313.ogg", 
            //    @"D:\Project\AudioTextR\backend\src\AudioTextR.Sample.ConsoleTelegramBot\bin\Debug\netcoreapp2.2\final.wav")).WaitForExit();
            return new MemoryStream();
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
