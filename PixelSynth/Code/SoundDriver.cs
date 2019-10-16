﻿using PixelSynth.Code.Effect;
using PixelSynth.Code.Occilator;
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

        private SineOccilator sineOccilator;
        private SquareOccilator squareOccilator;
        private SawToothOccilator sawToothOccilator;

        private const int samplesPerSecond = 44100;

        public SoundDriver()
        {
            packetPlayer = new SoundPlayer();

            sineOccilator = new SineOccilator(samplesPerSecond);
            squareOccilator = new SquareOccilator(samplesPerSecond);
            sawToothOccilator = new SawToothOccilator(samplesPerSecond);

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
            GetBasicPacketFromOccilator(note, octave);
            ModifyGeneratedPacket();
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

        private void GetBasicPacketFromOccilator(Note.Type note, int octave)
        {
            IOccilator occilator = sineOccilator;

            if (CurrentBaseWave == BasicWave.Sawtooth)
                occilator = sawToothOccilator;
            if (CurrentBaseWave == BasicWave.Square)
                occilator = squareOccilator;

            occilator.SetFrequency(Note.GetNote(note, octave));

            packet1 = new double[samplesPerSecond];
            for (int i = 0; i < samplesPerSecond; i++)
            {
                packet1[i] = occilator.GetNext(i);
            }
        }

        private void ModifyGeneratedPacket()
        {
            if (CurrentPreset == Preset.SoftReverbClap)
            {
                packet1 = ElementaryEffect.BitDecimator(packet1);
                packet1 = ElementaryEffect.BitMediator(packet1);
                packet1 = ElementaryEffect.BitHyperbolicCut(packet1);
            }
        }
    }
}
