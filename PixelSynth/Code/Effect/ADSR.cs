using Microsoft.Xna.Framework;
using PixelSynth.Code.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelSynth.Code.Effect
{
    static public class ADSR
    {
        private const int ADSRLength = 10;

        private static readonly double[] PresetTreble = new double[ADSRLength]
        {
            0.5d, 1.0d, 0.8d, 0.4d, 0.2d, 0.1d, 0.05d, 0.025d, 0.001d, 0.0d
        };

        private static readonly double[] PresetSoft = new double[ADSRLength]
        {
            0.33d, 0.66d, 0.88d, 1.0d, 0.9d, 0.7d, 0.05d, 0.3d, 0.1d, 0.0d
        };

        private static readonly double[] PresetAttack = new double[ADSRLength]
        {
            1.0d, 0.5d, 0.0d, 0.0d, 0.0d, 0.0d, 0.0d, 0.0d, 0.0d, 0.0d
        };

        static public double[] HardTreble(double[] packet)
        {
            double[] npacket = (double[])packet.Clone();

            //Apply ADSR
            for (int iPacket = 0; iPacket < packet.Length; iPacket++)
            {
                int iCurve = Maths.Map(iPacket, 0, packet.Length - 1, 0, ADSRLength - 1);

                npacket[iPacket] *= PresetTreble[iCurve];
            }

            return npacket;
        }

        static public double[] SoftTreble(double[] packet)
        {
            double[] npacket = (double[])packet.Clone();

            //Apply ADSR
            for (int iPacket = 0; iPacket < packet.Length; iPacket++)
            {
                int iCurve = Maths.Map(iPacket, 0, packet.Length - 1, 0, ADSRLength - 1);

                npacket[iPacket] *= PresetSoft[iCurve];
            }

            //Soften
            for (int i = 0; i < 100; i++)
            {
                npacket = ElementaryEffect.BitMediator(npacket);
            }

            //Volume up
            for (int i = 0; i < npacket.Length; i++)
            {
                npacket[i] *= 1.5f;
            }

            return npacket;
        }

        static public double[] Attack(double[] packet)
        {
            const int AttackCutoffIndex = 300;

            double[] npacket = (double[])packet.Clone();

            //Apply ADSR
            for (int iPacket = 0; iPacket < packet.Length; iPacket++)
            {
                int iCurve = Maths.Map(iPacket, 0, packet.Length - 1, 0, ADSRLength - 1);

                npacket[iPacket] *= PresetAttack[iCurve];
            }

            //Soften - except for attack
            double[] nonAttackPiece = (double[])npacket.Clone();
            for (int i = 0; i < 100; i++)
            {
                nonAttackPiece = ElementaryEffect.BitMediator(npacket);
            }
            for (int i = AttackCutoffIndex; i < npacket.Length; i++)
            {
                npacket[i] = nonAttackPiece[i];
            }

            //Volume up
            for (int i = AttackCutoffIndex; i < npacket.Length; i++)
            {
                npacket[i] *= 1.5f;
            }

            //Volume down the attack
            for (int i = 0; i <= AttackCutoffIndex; i++)
            {
                npacket[i] /= 1.5f;

                npacket[i] = npacket[i] / (AttackCutoffIndex - i + 1);
            }

            return npacket;
        }
    }
}
