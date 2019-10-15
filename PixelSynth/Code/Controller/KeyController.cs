using Microsoft.Xna.Framework.Input;
using PixelSynth.Code.Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelSynth.Code.Controller
{
    public class KeyController
    {
        private KeyboardState keys;
        private KeyboardState oldKeys;

        public void Update(SoundDriver soundDriver)
        {
            keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.LeftShift))
                soundDriver.PlayPacket(Note.Type.A, 4);
            if (keys.IsKeyDown(Keys.Z))
                soundDriver.PlayPacket(Note.Type.ASharp, 4);
            if (keys.IsKeyDown(Keys.X))
                soundDriver.PlayPacket(Note.Type.B, 4);
            if (keys.IsKeyDown(Keys.C))
                soundDriver.PlayPacket(Note.Type.C, 4);
            if (keys.IsKeyDown(Keys.V))
                soundDriver.PlayPacket(Note.Type.CSharp, 4);
            if (keys.IsKeyDown(Keys.B))
                soundDriver.PlayPacket(Note.Type.D, 4);
            if (keys.IsKeyDown(Keys.N))
                soundDriver.PlayPacket(Note.Type.DSharp, 4);
            if (keys.IsKeyDown(Keys.M))
                soundDriver.PlayPacket(Note.Type.E, 4);
            if (keys.IsKeyDown(Keys.OemComma))
                soundDriver.PlayPacket(Note.Type.F, 4);
            if (keys.IsKeyDown(Keys.OemPeriod))
                soundDriver.PlayPacket(Note.Type.FSharp, 4);
            if (keys.IsKeyDown(Keys.OemQuestion))
                soundDriver.PlayPacket(Note.Type.G, 4);
            if (keys.IsKeyDown(Keys.RightShift))
                soundDriver.PlayPacket(Note.Type.GSharp, 4);

            oldKeys = keys;
        }
    }
}
