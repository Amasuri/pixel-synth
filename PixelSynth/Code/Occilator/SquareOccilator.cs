using System;

namespace PixelSynth.Code.Occilator
{
    public class SquareOccilator : IOccilator
    {
        private double _radiansPerCircle = Math.PI * 2;
        private double _currentFrequency = 2000;
        private double _sampleRate = 44100;

        public SquareOccilator(double sampleRate)
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
            if (depthIntoOccilations > 0.5)
            {
                sinPart += 0.5;
            }
            else
            {
                sinPart -= 0.5;
            }

            return sinPart;
        }
    }
}
