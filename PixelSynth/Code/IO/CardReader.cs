using System;
using System.IO;

namespace PixelSynth.Code.IO
{
    static public class CardReader
    {
        static public CardData ReadCard(string filename)
        {
            string[] file = File.ReadAllLines("preset/" + filename);

            return new CardData
            {
                commentary = file[0],
                ADSR = (SoundDriver.ADSRMode)Convert.ToInt32( file[1] ),
                obertone = (SoundDriver.ObertoneMode)Convert.ToInt32(file[2]),
                preset = (SoundDriver.Preset)Convert.ToInt32(file[3]),
                wave = (SoundDriver.BasicWave)Convert.ToInt32(file[4]),
            };
        }
    }
}
