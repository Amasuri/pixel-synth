using System;

namespace PixelSynth.Code.Oscillator
{
    public class SawToothOscillator : IOscillator
    {
        private readonly double _radiansPerCircle = Math.PI * 2;
        private double _currentFrequency = 2000;
        private readonly double _sampleRate = 44100;
        private double _detune = 0.0;

        public SawToothOscillator(double sampleRate)
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
            double sinPart = Math.Sin(depthIntoOccilations * _radiansPerCircle);
            sinPart += (depthIntoOccilations - 0.5);
            return sinPart;
        }

        public void SetDetune(double detune)
        {
            _detune = detune;
        }
    }
}
