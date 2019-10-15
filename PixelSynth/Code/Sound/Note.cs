using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelSynth.Code.Sound
{
    static public class Note
    {
        public enum Type
        {
            A,
            ASharp,
            B,
            C,
            CSharp,
            D,
            DSharp,
            E,
            F,
            FSharp,
            G,
            GSharp,
        }

        /// <summary>
        /// Reference for the fourth octave. Any note from it is doubled or divided by two.
        /// </summary>
        private struct NoteFrequency
        {
            internal const int DefaultOctave = 4;
            internal const double A = 440;
            internal const double ASharp = 466.16;
            internal const double B = 493.88;
            internal const double C = 523.25;
            internal const double CSharp = 554.36;
            internal const double D = 587.32;
            internal const double DSharp = 622.26;
            internal const double E = 659.26;
            internal const double F = 698.46;
            internal const double FSharp = 739.98;
            internal const double G = 784.00;
            internal const double GSharp = 830.60;
        }

        static public double GetNote(Type note, int octave = 4)
        {
            //Calculating octave difference
            double octaveMod = 1;
            int octaveDiff = octave - NoteFrequency.DefaultOctave;

            //Getting the octave modifier, since every note up an octave is twice the one under it
            if (octaveDiff > 0)
                octaveMod = octaveMod * Math.Pow(1 * 2, octaveDiff);
            else if (octaveDiff < 0)
                octaveMod = octaveMod / Math.Pow(1 * 2, Math.Abs( octaveDiff ));

            //Getting note frequency
            switch (note)
            {
                case Type.A:
                    return NoteFrequency.A * octaveMod;

                case Type.ASharp:
                    return NoteFrequency.ASharp * octaveMod;

                case Type.B:
                    return NoteFrequency.B * octaveMod;

                case Type.C:
                    return NoteFrequency.C * octaveMod;

                case Type.CSharp:
                    return NoteFrequency.CSharp * octaveMod;

                case Type.D:
                    return NoteFrequency.D * octaveMod;

                case Type.DSharp:
                    return NoteFrequency.DSharp * octaveMod;

                case Type.E:
                    return NoteFrequency.E * octaveMod;

                case Type.F:
                    return NoteFrequency.F * octaveMod;

                case Type.FSharp:
                    return NoteFrequency.FSharp * octaveMod;

                case Type.G:
                    return NoteFrequency.G * octaveMod;

                case Type.GSharp:
                    return NoteFrequency.GSharp * octaveMod;

                default:
                    throw new Exception("Note not found: " + note.ToString() + " - Architectural error. Check the code!");
            }
        }
    }
}
