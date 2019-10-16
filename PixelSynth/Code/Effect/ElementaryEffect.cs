namespace PixelSynth.Code.Effect
{
    public static class ElementaryEffect
    {
        static public double[] BitDecimator(double[] packet)
        {
            double[] npacket = (double[])packet.Clone();

            for (int i = 0; i < packet.Length; i++)
            {
                if (i % 2 == 0)
                    npacket[i] *= 120;
                else
                    npacket[i] /= 120;
            }

            return npacket;
        }

        static public double[] BitMediator(double[] packet)
        {
            double[] npacket = (double[])packet.Clone();

            for (int i = 0; i < packet.Length - 1; i++)
            {
                npacket[i + 1] = (npacket[i] + npacket[i + 1]) / 2;
            }

            return npacket;
        }

        static public double[] BitHyperbolicCut(double[] packet)
        {
            double[] npacket = (double[])packet.Clone();

            for (int i = 0; i < packet.Length; i++)
            {
                npacket[i] /= i;
            }

            return npacket;
        }

        static public double[] BitChord(double[] packet1, double[] packet2, double[] packet3)
        {
            double[] npacket = (double[])packet1.Clone();

            for (int i = 0; i < packet1.Length; i++)
            {
                npacket[i] += packet2[i] + packet3[i];
                npacket[i] /= 2;
            }

            return npacket;
        }
    }
}
