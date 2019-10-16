using PixelSynth.Code.Oscillator;
using PixelSynth.Code.Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelSynth.Code.Effect
{
    static public class Obertonator
    {
        static public double[] BasicHarmonics(double[] packet, IOscillator oscillator, double fundamentalFreq, int sampleRate )
        {
            double[] npacket = (double[])packet.Clone();

            double[] harmonics1 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 2, sampleRate, harmonics1);

            double[] harmonics2 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 3, sampleRate, harmonics2);

            double[] harmonics3 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 4, sampleRate, harmonics3);

            for (int i = 0; i < npacket.Length; i++)
            {
                npacket[i] += harmonics1[i] * 0.8f + harmonics2[i] * 0.6f + harmonics3[i] * 0.4f;
            }

            return npacket;
        }

        private static void PutWaveToSample(IOscillator oscillator, double frequency, int sampleRate, double[] sample)
        {
            oscillator.SetFrequency(frequency);

            for (int i = 0; i < sampleRate; i++)
            {
                sample[i] = oscillator.GetNext(i);
            }
        }
    }
}
