using System;
using System.IO;
using Microsoft.Xna.Framework.Input;
using PixelSynth.Code.IO;
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

        public int CurrentLowerKeysOctave { get; private set; }
        public int CurrentMediumKeysOctave { get; private set; }
        public int CurrentHigherKeysOctave { get; private set; }

        public string AtPreset => cardNames[cardIndex].Replace("preset/", "");
        private string[] cardNames;
        private int cardIndex = 0;

        public KeyController()
        {
            CurrentLowerKeysOctave = 3;
            CurrentMediumKeysOctave = 4;
            CurrentHigherKeysOctave = 5;

            ReloadCardNames();
        }

        public void Update(SoundDriver soundDriver)
        {
            keys = Keyboard.GetState();

            CheckChordAlterators(soundDriver);
            CheckADSR(soundDriver);
            CheckObertonator(soundDriver);

            PlayLowerNotes(soundDriver);
            PlayMediumNotes(soundDriver);
            PlayHigherNotes(soundDriver);

            SwitchCombos(soundDriver);
            SwitchOscillators(soundDriver);

            CheckPresets(soundDriver);

            oldKeys = keys;
        }

        private void CheckPresets(SoundDriver soundDriver)
        {
            if (keys.IsKeyDown(Keys.Left) && oldKeys.IsKeyUp(Keys.Left))
                cardIndex--;
            if (keys.IsKeyDown(Keys.Right) && oldKeys.IsKeyUp(Keys.Right))
                cardIndex++;

            if (cardIndex < 0)
                cardIndex = 0;
            if (cardIndex >= cardNames.Length)
                cardIndex = cardNames.Length - 1;

            if (keys.IsKeyDown(Keys.Up) && oldKeys.IsKeyUp(Keys.Up))
                soundDriver.SetFromCard(CardIO.ReadCard(AtPreset));

            if (keys.IsKeyDown(Keys.Down) && oldKeys.IsKeyUp(Keys.Down))
            {
                CardIO.WriteNewCard(soundDriver);
                ReloadCardNames();
            }
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

        private void SwitchCombos(SoundDriver soundDriver)
        {
            if (keys.IsKeyDown(Keys.D1) && oldKeys.IsKeyUp(Keys.D1))
                soundDriver.SwitchPresetTo(EffectCombo.NoEffect);
            else if (keys.IsKeyDown(Keys.D2) && oldKeys.IsKeyUp(Keys.D2))
                soundDriver.SwitchPresetTo(EffectCombo.SoftReverbClap);
        }

        private void ReloadCardNames()
        {
            cardNames = Directory.GetFiles("preset/");
            cardIndex = 0;
        }

        private void PlayLowerNotes(SoundDriver soundDriver)
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

        private void PlayMediumNotes(SoundDriver soundDriver)
        {
            if (keys.IsKeyDown(Keys.CapsLock) && oldKeys.IsKeyUp(Keys.CapsLock))
            {
                lastOctave = CurrentMediumKeysOctave;
                lastNote = Note.Type.A;
                soundDriver.PlayPacket(Note.Type.A, CurrentMediumKeysOctave);
            }
            if (keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A))
            {
                lastOctave = CurrentMediumKeysOctave;
                lastNote = Note.Type.ASharp;
                soundDriver.PlayPacket(Note.Type.ASharp, CurrentMediumKeysOctave);
            }
            if (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
            {
                lastOctave = CurrentMediumKeysOctave;
                lastNote = Note.Type.B;
                soundDriver.PlayPacket(Note.Type.B, CurrentMediumKeysOctave);
            }
            if (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
            {
                lastOctave = CurrentMediumKeysOctave;
                lastNote = Note.Type.C;
                soundDriver.PlayPacket(Note.Type.C, CurrentMediumKeysOctave);
            }
            if (keys.IsKeyDown(Keys.F) && oldKeys.IsKeyUp(Keys.F))
            {
                lastOctave = CurrentMediumKeysOctave;
                lastNote = Note.Type.CSharp;
                soundDriver.PlayPacket(Note.Type.CSharp, CurrentMediumKeysOctave);
            }
            if (keys.IsKeyDown(Keys.G) && oldKeys.IsKeyUp(Keys.G))
            {
                lastOctave = CurrentMediumKeysOctave;
                lastNote = Note.Type.D;
                soundDriver.PlayPacket(Note.Type.D, CurrentMediumKeysOctave);
            }
            if (keys.IsKeyDown(Keys.H) && oldKeys.IsKeyUp(Keys.H))
            {
                lastOctave = CurrentMediumKeysOctave;
                lastNote = Note.Type.DSharp;
                soundDriver.PlayPacket(Note.Type.DSharp, CurrentMediumKeysOctave);
            }
            if (keys.IsKeyDown(Keys.J) && oldKeys.IsKeyUp(Keys.J))
            {
                lastOctave = CurrentMediumKeysOctave;
                lastNote = Note.Type.E;
                soundDriver.PlayPacket(Note.Type.E, CurrentMediumKeysOctave);
            }
            if (keys.IsKeyDown(Keys.K) && oldKeys.IsKeyUp(Keys.K))
            {
                lastOctave = CurrentMediumKeysOctave;
                lastNote = Note.Type.F;
                soundDriver.PlayPacket(Note.Type.F, CurrentMediumKeysOctave);
            }
            if (keys.IsKeyDown(Keys.L) && oldKeys.IsKeyUp(Keys.L))
            {
                lastOctave = CurrentMediumKeysOctave;
                lastNote = Note.Type.FSharp;
                soundDriver.PlayPacket(Note.Type.FSharp, CurrentMediumKeysOctave);
            }
            if (keys.IsKeyDown(Keys.OemSemicolon) && oldKeys.IsKeyUp(Keys.OemSemicolon))
            {
                lastOctave = CurrentMediumKeysOctave;
                lastNote = Note.Type.G;
                soundDriver.PlayPacket(Note.Type.G, CurrentMediumKeysOctave);
            }
            if (keys.IsKeyDown(Keys.OemQuotes) && oldKeys.IsKeyUp(Keys.OemQuotes))
            {
                lastOctave = CurrentMediumKeysOctave;
                lastNote = Note.Type.GSharp;
                soundDriver.PlayPacket(Note.Type.GSharp, CurrentMediumKeysOctave);
            }
        }

        private void PlayHigherNotes(SoundDriver soundDriver)
        {
            if (keys.IsKeyDown(Keys.Tab) && oldKeys.IsKeyUp(Keys.Tab))
            {
                lastOctave = CurrentHigherKeysOctave;
                lastNote = Note.Type.A;
                soundDriver.PlayPacket(Note.Type.A, CurrentHigherKeysOctave);
            }
            if (keys.IsKeyDown(Keys.Q) && oldKeys.IsKeyUp(Keys.Q))
            {
                lastOctave = CurrentHigherKeysOctave;
                lastNote = Note.Type.ASharp;
                soundDriver.PlayPacket(Note.Type.ASharp, CurrentHigherKeysOctave);
            }
            if (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
            {
                lastOctave = CurrentHigherKeysOctave;
                lastNote = Note.Type.B;
                soundDriver.PlayPacket(Note.Type.B, CurrentHigherKeysOctave);
            }
            if (keys.IsKeyDown(Keys.E) && oldKeys.IsKeyUp(Keys.E))
            {
                lastOctave = CurrentHigherKeysOctave;
                lastNote = Note.Type.C;
                soundDriver.PlayPacket(Note.Type.C, CurrentHigherKeysOctave);
            }
            if (keys.IsKeyDown(Keys.R) && oldKeys.IsKeyUp(Keys.R))
            {
                lastOctave = CurrentHigherKeysOctave;
                lastNote = Note.Type.CSharp;
                soundDriver.PlayPacket(Note.Type.CSharp, CurrentHigherKeysOctave);
            }
            if (keys.IsKeyDown(Keys.T) && oldKeys.IsKeyUp(Keys.T))
            {
                lastOctave = CurrentHigherKeysOctave;
                lastNote = Note.Type.D;
                soundDriver.PlayPacket(Note.Type.D, CurrentHigherKeysOctave);
            }
            if (keys.IsKeyDown(Keys.Y) && oldKeys.IsKeyUp(Keys.Y))
            {
                lastOctave = CurrentHigherKeysOctave;
                lastNote = Note.Type.DSharp;
                soundDriver.PlayPacket(Note.Type.DSharp, CurrentHigherKeysOctave);
            }
            if (keys.IsKeyDown(Keys.U) && oldKeys.IsKeyUp(Keys.U))
            {
                lastOctave = CurrentHigherKeysOctave;
                lastNote = Note.Type.E;
                soundDriver.PlayPacket(Note.Type.E, CurrentHigherKeysOctave);
            }
            if (keys.IsKeyDown(Keys.I) && oldKeys.IsKeyUp(Keys.I))
            {
                lastOctave = CurrentHigherKeysOctave;
                lastNote = Note.Type.F;
                soundDriver.PlayPacket(Note.Type.F, CurrentHigherKeysOctave);
            }
            if (keys.IsKeyDown(Keys.O) && oldKeys.IsKeyUp(Keys.O))
            {
                lastOctave = CurrentHigherKeysOctave;
                lastNote = Note.Type.FSharp;
                soundDriver.PlayPacket(Note.Type.FSharp, CurrentHigherKeysOctave);
            }
            if (keys.IsKeyDown(Keys.P) && oldKeys.IsKeyUp(Keys.P))
            {
                lastOctave = CurrentHigherKeysOctave;
                lastNote = Note.Type.G;
                soundDriver.PlayPacket(Note.Type.G, CurrentHigherKeysOctave);
            }
            if (keys.IsKeyDown(Keys.OemOpenBrackets) && oldKeys.IsKeyUp(Keys.OemOpenBrackets))
            {
                lastOctave = CurrentHigherKeysOctave;
                lastNote = Note.Type.GSharp;
                soundDriver.PlayPacket(Note.Type.GSharp, CurrentHigherKeysOctave);
            }
        }
    }
}
