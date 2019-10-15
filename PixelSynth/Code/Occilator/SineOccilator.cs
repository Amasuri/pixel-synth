using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelSynth.Code.Occilator
{
    public class SineOccilator : IOccilator
    {
        private double _radiansPerCircle = Math.PI * 2;
        private double _currentFrequency = 2000;
        private double _sampleRate = 44100;

        public SineOccilator(double sampleRate)
        {
            _sampleRate = sampleRate;
        }

        public void SetFrequency(double value)
        {
            _currentFrequency = value;
        }

        public double GetNext(int sampleNumberInSecond)
        {
            double samplesPerOccilation = (_sampleRate / _currentFrequency);
            double depthIntoOccilations = (sampleNumberInSecond % samplesPerOccilation) / samplesPerOccilation;
            return Math.Sin(depthIntoOccilations * _radiansPerCircle);
        }
    }
}
