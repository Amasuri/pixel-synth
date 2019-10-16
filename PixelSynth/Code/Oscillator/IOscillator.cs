using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelSynth.Code.Oscillator
{
    public interface IOscillator
    {
        double GetNext(int sampleNumberInSecond);

        void SetFrequency(double value);
    }

    internal interface IDetuneable
    {
        void SetDetune(double maxDetune);
    }
}
