using System;
using Microsoft.Xna.Framework.Input;
using PixelSynth.Code.Sound;
using static PixelSynth.Code.SoundDriver;

namespace PixelSynth.Code.Controller
{
    public class KeyController
    {
        public Note.Type lastNote { get; private set; }
        public int lastOctave { get; private set; }

        private KeyboardState keys;
        private KeyboardState oldKeys;

        private const int CurrentLowerKeysOctave = 4;

        public void Update(SoundDriver soundDriver)
        {
            keys = Keyboard.GetState();

            CheckChordAlterators(soundDriver);
            CheckADSR(soundDriver);
            CheckObertonator(soundDriver);
            PlayNotes(soundDriver);
            SwitchPresets(soundDriver);
            SwitchOscillators(soundDriver);

            oldKeys = keys;
        }

        private void CheckObertonator(SoundDriver soundDriver)
        {
            if (keys.IsKeyDown(Keys.OemPlus) && oldKeys.IsKeyUp(Keys.OemPlus))
                soundDriver.SeekObertonator(+1);

            if (keys.IsKeyDown(Keys.OemMinus) && oldKeys.IsKeyUp(Keys.OemMinus))
                soundDriver.SeekObertonator(-1);
        }

        private void CheckADSR(SoundDriver soundDriver)
        {
            if (keys.IsKeyDown(Keys.NumPad5))
                soundDriver.SwitchADSRTo(ADSRMode.NoADSR);
            else if (keys.IsKeyDown(Keys.NumPad7))
                soundDriver.SwitchADSRTo(ADSRMode.Treble);
            else if (keys.IsKeyDown(Keys.NumPad8))
                soundDriver.SwitchADSRTo(ADSRMode.Mellow);
            else if (keys.IsKeyDown(Keys.NumPad9))
                soundDriver.SwitchADSRTo(ADSRMode.Attack);
        }

        private void CheckChordAlterators(SoundDriver soundDriver)
        {
            if (keys.IsKeyDown(Keys.NumPad4))
                soundDriver.SwitchNoteModeTo(NoteMode.MajorChord);
            else if (keys.IsKeyDown(Keys.NumPad6))
                soundDriver.SwitchNoteModeTo(NoteMode.MinorChord);
            else
                soundDriver.SwitchNoteModeTo(NoteMode.Single);
        }

        private void SwitchOscillators(SoundDriver soundDriver)
        {
            if (keys.IsKeyDown(Keys.NumPad1) && oldKeys.IsKeyUp(Keys.NumPad1))
                soundDriver.SwitchBaseWaveTo(BasicWave.Sine);
            else if (keys.IsKeyDown(Keys.NumPad2) && oldKeys.IsKeyUp(Keys.NumPad2))
                soundDriver.SwitchBaseWaveTo(BasicWave.Square);
            else if (keys.IsKeyDown(Keys.NumPad3) && oldKeys.IsKeyUp(Keys.NumPad3))
                soundDriver.SwitchBaseWaveTo(BasicWave.Sawtooth);
        }

        private void SwitchPresets(SoundDriver soundDriver)
        {
            if (keys.IsKeyDown(Keys.D1) && oldKeys.IsKeyUp(Keys.D1))
                soundDriver.SwitchPresetTo(Preset.DefaultWave);
            else if (keys.IsKeyDown(Keys.D2) && oldKeys.IsKeyUp(Keys.D2))
                soundDriver.SwitchPresetTo(Preset.SoftReverbClap);
        }

        private void PlayNotes(SoundDriver soundDriver)
        {
            if (keys.IsKeyDown(Keys.LeftShift) && oldKeys.IsKeyUp(Keys.LeftShift))
            {
                lastOctave = CurrentLowerKeysOctave;
                lastNote = Note.Type.A;
                soundDriver.PlayPacket(Note.Type.A, CurrentLowerKeysOctave);
            }
            if (keys.IsKeyDown(Keys.Z) && oldKeys.IsKeyUp(Keys.Z))
            {
                lastOctave = CurrentLowerKeysOctave;
                lastNote = Note.Type.ASharp;
                soundDriver.PlayPacket(Note.Type.ASharp, CurrentLowerKeysOctave);
            }
            if (keys.IsKeyDown(Keys.X) && oldKeys.IsKeyUp(Keys.X))
            {
                lastOctave = CurrentLowerKeysOctave;
                lastNote = Note.Type.B;
                soundDriver.PlayPacket(Note.Type.B, CurrentLowerKeysOctave);
            }
            if (keys.IsKeyDown(Keys.C) && oldKeys.IsKeyUp(Keys.C))
            {
                lastOctave = CurrentLowerKeysOctave;
                lastNote = Note.Type.C;
                soundDriver.PlayPacket(Note.Type.C, CurrentLowerKeysOctave);
            }
            if (keys.IsKeyDown(Keys.V) && oldKeys.IsKeyUp(Keys.V))
            {
                lastOctave = CurrentLowerKeysOctave;
                lastNote = Note.Type.CSharp;
                soundDriver.PlayPacket(Note.Type.CSharp, CurrentLowerKeysOctave);
            }
            if (keys.IsKeyDown(Keys.B) && oldKeys.IsKeyUp(Keys.B))
            {
                lastOctave = CurrentLowerKeysOctave;
                lastNote = Note.Type.D;
                soundDriver.PlayPacket(Note.Type.D, CurrentLowerKeysOctave);
            }
            if (keys.IsKeyDown(Keys.N) && oldKeys.IsKeyUp(Keys.N))
            {
                lastOctave = CurrentLowerKeysOctave;
                lastNote = Note.Type.DSharp;
                soundDriver.PlayPacket(Note.Type.DSharp, CurrentLowerKeysOctave);
            }
            if (keys.IsKeyDown(Keys.M) && oldKeys.IsKeyUp(Keys.M))
            {
                lastOctave = CurrentLowerKeysOctave;
                lastNote = Note.Type.E;
                soundDriver.PlayPacket(Note.Type.E, CurrentLowerKeysOctave);
            }
            if (keys.IsKeyDown(Keys.OemComma) && oldKeys.IsKeyUp(Keys.OemComma))
            {
                lastOctave = CurrentLowerKeysOctave;
                lastNote = Note.Type.F;
                soundDriver.PlayPacket(Note.Type.F, CurrentLowerKeysOctave);
            }
            if (keys.IsKeyDown(Keys.OemPeriod) && oldKeys.IsKeyUp(Keys.OemPeriod))
            {
                lastOctave = CurrentLowerKeysOctave;
                lastNote = Note.Type.FSharp;
                soundDriver.PlayPacket(Note.Type.FSharp, CurrentLowerKeysOctave);
            }
            if (keys.IsKeyDown(Keys.OemQuestion) && oldKeys.IsKeyUp(Keys.OemQuestion))
            {
                lastOctave = CurrentLowerKeysOctave;
                lastNote = Note.Type.G;
                soundDriver.PlayPacket(Note.Type.G, CurrentLowerKeysOctave);
            }
            if (keys.IsKeyDown(Keys.RightShift) && oldKeys.IsKeyUp(Keys.RightShift))
            {
                lastOctave = CurrentLowerKeysOctave;
                lastNote = Note.Type.GSharp;
                soundDriver.PlayPacket(Note.Type.GSharp, CurrentLowerKeysOctave);
            }
        }
    }
}
