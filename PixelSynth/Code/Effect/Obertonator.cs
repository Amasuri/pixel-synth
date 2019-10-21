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

            double[] harmonics4 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 5, sampleRate, harmonics4);

            double[] harmonics5 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 7, sampleRate, harmonics5);

            for (int i = 0; i < npacket.Length; i++)
            {
                npacket[i] += harmonics1[i] * 0.77f + harmonics2[i] * 0.66f + harmonics3[i] * 0.55f + harmonics4[i] * 0.44f + harmonics5[i] * 0.33f;
            }

            return npacket;
        }

        static public double[] OrganHarmonics(double[] packet, IOscillator oscillator, double fundamentalFreq, int sampleRate)
        {
            double[] npacket = (double[])packet.Clone();

            double[] harmonics1 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 2, sampleRate, harmonics1);

            double[] harmonics2 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 3, sampleRate, harmonics2);

            double[] harmonics3 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 4, sampleRate, harmonics3);

            double[] harmonics4 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 5, sampleRate, harmonics4);

            double[] harmonics5 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 6, sampleRate, harmonics5);

            double[] harmonics6 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 7, sampleRate, harmonics6);

            for (int i = 0; i < npacket.Length; i++)
            {
                npacket[i] += harmonics1[i] * 0.85f + harmonics2[i] * 0.9f + harmonics3[i] * 0.95f + harmonics4[i] * 1.0f + harmonics5[i] * 1.1f + harmonics6[i] * 1.2f;
            }

            return npacket;
        }

        static public double[] DistortionHarmonics(double[] packet, IOscillator oscillator, double fundamentalFreq, int sampleRate)
        {
            double[] npacket = (double[])packet.Clone();

            double[] harmonics1 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq / 2, sampleRate, harmonics1);

            double[] harmonics2 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq / 3, sampleRate, harmonics2);

            double[] harmonics3 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq / 4, sampleRate, harmonics3);

            double[] harmonics4 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 2, sampleRate, harmonics4);

            double[] harmonics5 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 3, sampleRate, harmonics5);

            double[] harmonics6 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 4, sampleRate, harmonics6);

            for (int i = 0; i < npacket.Length; i++)
            {
                npacket[i] += harmonics1[i] * 0.85f + harmonics2[i] * 0.9f + harmonics3[i] * 0.95f + harmonics4[i] * 1.0f + harmonics5[i] * 1.1f + harmonics6[i] * 1.2f;
            }

            return npacket;
        }

        static public double[] GrittyHarmonics(double[] packet, IOscillator oscillator, double fundamentalFreq, int sampleRate)
        {
            double[] npacket = (double[])packet.Clone();

            double[] harmonics1 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 1.2, sampleRate, harmonics1);

            double[] harmonics2 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 1.3, sampleRate, harmonics2);

            double[] harmonics3 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 1.4, sampleRate, harmonics3);

            double[] harmonics4 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 2, sampleRate, harmonics4);

            double[] harmonics5 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 3, sampleRate, harmonics5);

            double[] harmonics6 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 4, sampleRate, harmonics6);

            for (int i = 0; i < npacket.Length; i++)
            {
                npacket[i] +=
                    harmonics1[i] * 0.85f +
                    harmonics2[i] * 0.9f +
                    harmonics3[i] * 0.95f +
                    harmonics4[i] * 1.0f +
                    harmonics5[i] * 1.1f +
                    harmonics6[i] * 1.2f;
            }

            return npacket;
        }

        static public double[] BassyHarmonics(double[] packet, IOscillator oscillator, double fundamentalFreq, int sampleRate)
        {
            double[] npacket = (double[])packet.Clone();

            double[] harmonics1 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 1.5, sampleRate, harmonics1);

            double[] harmonics2 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq / 3, sampleRate, harmonics2);

            double[] harmonics3 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 4.5, sampleRate, harmonics3);

            double[] harmonics4 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq / 2, sampleRate, harmonics4);

            double[] harmonics5 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq * 4, sampleRate, harmonics5);

            double[] harmonics6 = new double[sampleRate];
            PutWaveToSample(oscillator, fundamentalFreq / 6, sampleRate, harmonics6);

            for (int i = 0; i < npacket.Length; i++)
            {
                npacket[i] +=
                    harmonics1[i] * 0.85f +
                    harmonics2[i] * 0.85f +
                    harmonics3[i] * 0.85f +
                    harmonics4[i] * 1.1f +
                    harmonics5[i] * 1.1f +
                    harmonics6[i] * 1.1f;
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
