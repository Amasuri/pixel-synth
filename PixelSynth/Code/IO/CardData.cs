using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PixelSynth.Code.SoundDriver;

namespace PixelSynth.Code.IO
{
    public struct CardData
    {
        public string commentary;
        public ObertoneMode obertone;
        public ADSRMode ADSR;
        public Preset preset;
        public BasicWave wave;
    }
}
