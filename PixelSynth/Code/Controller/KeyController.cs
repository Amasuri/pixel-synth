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

        public void Update(SoundDriver soundDriver)
        {
            keys = Keyboard.GetState();

            PlayNotes(soundDriver);
            SwitchPresets(soundDriver);

            oldKeys = keys;
        }

        private void SwitchPresets(SoundDriver soundDriver)
        {
            if (keys.IsKeyDown(Keys.D1) && oldKeys.IsKeyUp(Keys.D1))
                soundDriver.SwitchPresetTo(Preset.Default);
            else if (keys.IsKeyDown(Keys.D2) && oldKeys.IsKeyUp(Keys.D2))
                soundDriver.SwitchPresetTo(Preset.Blurp);
        }

        private void PlayNotes(SoundDriver soundDriver)
        {
            if (keys.IsKeyDown(Keys.LeftShift))
            {
                lastOctave = 4;
                lastNote = Note.Type.A;
                soundDriver.PlayPacket(Note.Type.A, 4);
            }
            if (keys.IsKeyDown(Keys.Z))
            {
                lastOctave = 4;
                lastNote = Note.Type.ASharp;
                soundDriver.PlayPacket(Note.Type.ASharp, 4);
            }
            if (keys.IsKeyDown(Keys.X))
            {
                lastOctave = 4;
                lastNote = Note.Type.B;
                soundDriver.PlayPacket(Note.Type.B, 4);
            }
            if (keys.IsKeyDown(Keys.C))
            {
                lastOctave = 4;
                lastNote = Note.Type.C;
                soundDriver.PlayPacket(Note.Type.C, 4);
            }
            if (keys.IsKeyDown(Keys.V))
            {
                lastOctave = 4;
                lastNote = Note.Type.CSharp;
                soundDriver.PlayPacket(Note.Type.CSharp, 4);
            }
            if (keys.IsKeyDown(Keys.B))
            {
                lastOctave = 4;
                lastNote = Note.Type.D;
                soundDriver.PlayPacket(Note.Type.D, 4);
            }
            if (keys.IsKeyDown(Keys.N))
            {
                lastOctave = 4;
                lastNote = Note.Type.DSharp;
                soundDriver.PlayPacket(Note.Type.DSharp, 4);
            }
            if (keys.IsKeyDown(Keys.M))
            {
                lastOctave = 4;
                lastNote = Note.Type.E;
                soundDriver.PlayPacket(Note.Type.E, 4);
            }
            if (keys.IsKeyDown(Keys.OemComma))
            {
                lastOctave = 4;
                lastNote = Note.Type.F;
                soundDriver.PlayPacket(Note.Type.F, 4);
            }
            if (keys.IsKeyDown(Keys.OemPeriod))
            {
                lastOctave = 4;
                lastNote = Note.Type.FSharp;
                soundDriver.PlayPacket(Note.Type.FSharp, 4);
            }
            if (keys.IsKeyDown(Keys.OemQuestion))
            {
                lastOctave = 4;
                lastNote = Note.Type.G;
                soundDriver.PlayPacket(Note.Type.G, 4);
            }
            if (keys.IsKeyDown(Keys.RightShift))
            {
                lastOctave = 4;
                lastNote = Note.Type.GSharp;
                soundDriver.PlayPacket(Note.Type.GSharp, 4);
            }
        }
    }
}
