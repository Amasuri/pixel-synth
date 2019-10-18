using System;
using System.IO;

namespace PixelSynth.Code.IO
{
    static public class CardIO
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

        static public void WriteNewCard(SoundDriver soundDriver)
        {
            string data =
                "No description\n" +
                ((int)soundDriver.CurrentADSRMode).ToString() + "\n" +
                ((int)soundDriver.CurrentObertonator).ToString() + "\n" +
                ((int)soundDriver.CurrentPreset).ToString() + "\n" +
                ((int)soundDriver.CurrentBaseWave).ToString();

            string filename = Guid.NewGuid().ToString().Substring(0, 8) + ".card";

            File.WriteAllText("preset/" + filename, data);
        }
    }
}
