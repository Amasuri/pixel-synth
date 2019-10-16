using PixelSynth.Code.Effect;
using PixelSynth.Code.Oscillator;
using PixelSynth.Code.Sound;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace PixelSynth.Code
{
    public class SoundDriver
    {
        public Preset CurrentPreset { get; private set; }
        public enum Preset
        {
            DefaultWave,
            SoftReverbClap,
            MajorChord,
            MinorChord,
        }

        public BasicWave CurrentBaseWave { get; private set; }
        public enum BasicWave
        {
            Sine,
            Square,
            Sawtooth,
        }

        private double[] packet1;
        private double[] packet2;

        private SoundPlayer packetPlayer;

        private SineOscillator sineOscillator;
        private SquareOscillator squareOscillator;
        private SawToothOscillator sawToothOscillator;

        private const int samplesPerSecond = 44100;

        public SoundDriver()
        {
            packetPlayer = new SoundPlayer();

            sineOscillator = new SineOscillator(samplesPerSecond);
            squareOscillator = new SquareOscillator(samplesPerSecond);
            sawToothOscillator = new SawToothOscillator(samplesPerSecond);

            CurrentPreset = Preset.DefaultWave;
            CurrentBaseWave = BasicWave.Sine;
        }

        public void SwitchPresetTo(Preset preset)
        {
            CurrentPreset = preset;
        }

        public void SwitchBaseWaveTo(BasicWave wave)
        {
            CurrentBaseWave = wave;
        }

        public void PlayPacket(Note.Type note, int octave)
        {
            GetBasicPacketFromOscillator(note, octave, ref packet1);
            ModifyGeneratedPacket(note, octave);
            WritePacketToMemoryAsWave(packet1, packet1.Length);

            packetPlayer.Stream.Seek(0, SeekOrigin.Begin);
            packetPlayer.Play();
        }

        /// <summary>
        /// Writes sampled data to memory and plays it.
        /// </summary>
        private void WritePacketToMemoryAsWave(double[] sampleData, long sampleCount)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            int RIFF = 0x46464952;
            int WAVE = 0x45564157;
            int formatChunkSize = 16;
            int headerSize = 8;
            int format = 0x20746D66;
            short formatType = 1;
            short tracks = 2;
            short bitsPerSample = 16;
            short frameSize = (short)(tracks * ((bitsPerSample + 7) / 8));
            int bytesPerSecond = samplesPerSecond * frameSize;
            int waveSize = 4;
            int data = 0x61746164;
            int samples = (int)sampleCount;
            int dataChunkSize = samples * frameSize;
            int fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;
            writer.Write(RIFF);
            writer.Write(fileSize);
            writer.Write(WAVE);
            writer.Write(format);
            writer.Write(formatChunkSize);
            writer.Write(formatType);
            writer.Write(tracks);
            writer.Write(samplesPerSecond);
            writer.Write(bytesPerSecond);
            writer.Write(frameSize);
            writer.Write(bitsPerSample);
            writer.Write(data);
            writer.Write(dataChunkSize);

            double sample_l;
            short sl;
            for (int i = 0; i < sampleCount; i++)
            {
                sample_l = sampleData[i] * 30000.0;
                if (sample_l < -32767.0f) { sample_l = -32767.0f; }
                if (sample_l > 32767.0f) { sample_l = 32767.0f; }
                sl = (short)sample_l;
                stream.WriteByte((byte)(sl & 0xff));
                stream.WriteByte((byte)(sl >> 8));
                stream.WriteByte((byte)(sl & 0xff));
                stream.WriteByte((byte)(sl >> 8));
            }

            packetPlayer.Stream = new MemoryStream();

            stream.Position = 0;
            stream.CopyTo(packetPlayer.Stream);

            //FileStream fstream = new FileStream("test.wav", FileMode.Create);
            //stream.Position = 0;
            //stream.CopyTo(fstream);

            writer.Close();
            stream.Close();
        }

        private void GetBasicPacketFromOscillator(Note.Type note, int octave, ref double[] packet)
        {
            IOscillator Oscillator = sineOscillator;

            if (CurrentBaseWave == BasicWave.Sawtooth)
                Oscillator = sawToothOscillator;
            if (CurrentBaseWave == BasicWave.Square)
                Oscillator = squareOscillator;

            Oscillator.SetFrequency(Note.GetNote(note, octave));

            packet = new double[samplesPerSecond];
            for (int i = 0; i < samplesPerSecond; i++)
            {
                packet[i] = Oscillator.GetNext(i);
            }
        }

        private void ModifyGeneratedPacket(Note.Type note, int octave)
        {
            if (CurrentPreset == Preset.SoftReverbClap)
            {
                packet1 = ElementaryEffect.BitDecimator(packet1);
                packet1 = ElementaryEffect.BitMediator(packet1);
                packet1 = ElementaryEffect.BitHyperbolicCut(packet1);
            }

            if(CurrentPreset == Preset.MajorChord)
            {
                //2nd chord note
                double[] supportPacket1 = new double[samplesPerSecond];

                if(note + 4 > Note.Type.GSharp)
                    GetBasicPacketFromOscillator(note + 4 - 12, 5, ref supportPacket1);
                else
                    GetBasicPacketFromOscillator(note + 4, 4, ref supportPacket1);

                //3rd chord note
                double[] supportPacket2 = new double[samplesPerSecond];

                if (note + 7 > Note.Type.GSharp)
                    GetBasicPacketFromOscillator(note + 7 - 12, 5, ref supportPacket2);
                else
                    GetBasicPacketFromOscillator(note + 7, 4, ref supportPacket2);

                packet1 = ElementaryEffect.TriWaveAddition(packet1, supportPacket1, supportPacket2);
            }

            if (CurrentPreset == Preset.MinorChord)
            {
                //2nd chord note
                double[] supportPacket1 = new double[samplesPerSecond];

                if (note + 3 > Note.Type.GSharp)
                    GetBasicPacketFromOscillator(note + 3 - 12, 5, ref supportPacket1);
                else
                    GetBasicPacketFromOscillator(note + 3, 4, ref supportPacket1);

                //3rd chord note
                double[] supportPacket2 = new double[samplesPerSecond];

                if (note + 7 > Note.Type.GSharp)
                    GetBasicPacketFromOscillator(note + 7 - 12, 5, ref supportPacket2);
                else
                    GetBasicPacketFromOscillator(note + 7, 4, ref supportPacket2);

                packet1 = ElementaryEffect.TriWaveAddition(packet1, supportPacket1, supportPacket2);
            }
        }
    }
}
