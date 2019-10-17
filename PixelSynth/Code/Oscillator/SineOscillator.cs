using System;

namespace PixelSynth.Code.Oscillator
{
    public class SineOscillator : IOscillator
    {
        private readonly double _radiansPerCircle = Math.PI * 2;
        private double _currentFrequency = 2000;
        private readonly double _sampleRate = 44100;

        public SineOscillator(double sampleRate)
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
