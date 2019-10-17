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
        public ObertoneMode CurrentObertonator { get; private set; }
        public enum ObertoneMode
        {
            NoObertone,
            Basic
        }

        public ADSRMode CurrentADSRMode { get; private set; }
        public enum ADSRMode
        {
            NoADSR,
            Treble,
            Mellow,
            Attack,
        }

        public NoteMode CurrentNoteMode { get; private set; }
        public enum NoteMode
        {
            Single,
            MajorChord,
            MinorChord,
        }

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

        private readonly SoundPlayer packetPlayer;

        private readonly SineOscillator sineOscillator;
        private readonly SquareOscillator squareOscillator;
        private readonly SawToothOscillator sawToothOscillator;

        private const int samplesPerSecond = 44100;

        public SoundDriver()
        {
            packetPlayer = new SoundPlayer();

            sineOscillator = new SineOscillator(samplesPerSecond);
            squareOscillator = new SquareOscillator(samplesPerSecond);
            sawToothOscillator = new SawToothOscillator(samplesPerSecond);

            CurrentPreset = Preset.DefaultWave;
            CurrentBaseWave = BasicWave.Sine;
            CurrentNoteMode = NoteMode.Single;
            CurrentADSRMode = ADSRMode.NoADSR;
            CurrentObertonator = ObertoneMode.NoObertone;
        }

        public void SeekObertonator(int jump)
        {
            CurrentObertonator += jump;

            if (CurrentObertonator < 0)
                CurrentObertonator = ObertoneMode.NoObertone;

            if (CurrentObertonator > ObertoneMode.Basic)
                CurrentObertonator = ObertoneMode.Basic;
        }

        public void SwitchADSRTo(ADSRMode mode)
        {
            CurrentADSRMode = mode;
        }

        public void SwitchNoteModeTo(NoteMode noteMode)
        {
            CurrentNoteMode = noteMode;
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
            ApplyHarmonics(note, octave);
            ChordifyPacketIfApplicable(note, octave);
            ApplyADSR(note, octave);
            WritePacketToMemoryAsWave(packet1, packet1.Length);

            packetPlayer.Stream.Seek(0, SeekOrigin.Begin);
            packetPlayer.Play();
        }

        private void ApplyHarmonics(Note.Type note, int octave)
        {
            IOscillator Oscillator = null;
            Oscillator = SelectOscillator(Oscillator);

            if(CurrentObertonator == ObertoneMode.Basic)
                packet1 = Obertonator.BasicHarmonics(packet1, Oscillator, Note.GetNote(note, octave), samplesPerSecond);
        }

        private void ApplyADSR(Note.Type note, int octave)
        {
            if(CurrentADSRMode == ADSRMode.Treble)
                packet1 = ADSR.HardTreble(packet1);

            if (CurrentADSRMode == ADSRMode.Mellow)
                packet1 = ADSR.Mellow(packet1);

            if (CurrentADSRMode == ADSRMode.Attack)
                packet1 = ADSR.Attack(packet1);
        }

        private void ChordifyPacketIfApplicable(Note.Type note, int octave)
        {
            if (CurrentNoteMode == NoteMode.MajorChord)
            {
                //2nd chord note
                double[] supportPacket1 = new double[samplesPerSecond];

                if (note + 4 > Note.Type.GSharp)
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

            if (CurrentNoteMode == NoteMode.MinorChord)
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
            IOscillator Oscillator = null;
            Oscillator = SelectOscillator(Oscillator);

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
        }

        private IOscillator SelectOscillator(IOscillator Oscillator)
        {
            Oscillator = sineOscillator;

            if (CurrentBaseWave == BasicWave.Sawtooth)
                Oscillator = sawToothOscillator;
            if (CurrentBaseWave == BasicWave.Square)
                Oscillator = squareOscillator;
            return Oscillator;
        }
    }
}
