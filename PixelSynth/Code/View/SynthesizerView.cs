using Amasuri.Reusable.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelSynth.Code.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelSynth.Code.View
{
    public class SynthesizerView : ADrawingClass
    {
        private readonly SpriteFont font;
        private readonly Color fontColor = new Color(81, 196, 63);

        private readonly Texture2D baseSprite;
        private readonly Texture2D buttonTileset;
        private readonly Vector2 synthsPos;

        public SynthesizerView(Game game)
        {
            font = game.Content.Load<SpriteFont>("res/font/PixelFont1");
            baseSprite = game.Content.Load<Texture2D>("res/image/base");
            buttonTileset = game.Content.Load<Texture2D>("res/image/buttons");

            synthsPos = (PixelSynth.UnscaledWindowSize - baseSprite.Bounds.Size.ToVector2()) / 2;
        }

        public void Draw(SpriteBatch spriteBatch, KeyController controller, SoundDriver soundDriver)
        {
            //Layout
            DrawTexture(spriteBatch, baseSprite, synthsPos, PixelSynth.Scale);

            //Three octaves of keys
            int lowerTone = -1; int mediumTone = -1; int higherTone = -1;
            if (controller.lastOctave == controller.CurrentLowerKeysOctave)
                lowerTone = (int)controller.lastNote;
            if (controller.lastOctave == controller.CurrentMediumKeysOctave)
                mediumTone = (int)controller.lastNote;
            if (controller.lastOctave == controller.CurrentHigherKeysOctave)
                higherTone = (int)controller.lastNote;

            DrawOctaveAt(spriteBatch, synthsPos + new Vector2(6, 158), lowerTone);
            DrawOctaveAt(spriteBatch, synthsPos + new Vector2(6, 95), mediumTone);
            DrawOctaveAt(spriteBatch, synthsPos + new Vector2(6, 32), higherTone);

            //Panels & buttons
            DrawOscillatorPanel(spriteBatch, soundDriver, synthsPos + new Vector2(118, 38));
            DrawADSRPanel(spriteBatch, soundDriver, synthsPos + new Vector2(158, 66));
            DrawPresetPanel(spriteBatch, soundDriver, synthsPos + new Vector2(110, 66));
            DrawScreenInfo(spriteBatch, soundDriver, controller, synthsPos + new Vector2(6, 6));
            DrawChordIndicators(spriteBatch, soundDriver, synthsPos + new Vector2(26, 228));
            DrawOctaveSliders(spriteBatch, controller, synthsPos + new Vector2(6, 75));
        }

        private void DrawOctaveSliders(SpriteBatch spriteBatch, KeyController controller, Vector2 pos)
        {
            Point startSeek;

            startSeek = new Point(16 * (controller.CurrentHigherKeysOctave - 1), 6);
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(0, 0), PixelSynth.Scale, new Rectangle(startSeek, new Point(96, 6)));

            startSeek = new Point(16 * (controller.CurrentMediumKeysOctave - 1), 6);
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(0, 63), PixelSynth.Scale, new Rectangle(startSeek, new Point(96, 6)));

            startSeek = new Point(16 * (controller.CurrentLowerKeysOctave - 1), 6);
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(0, 126), PixelSynth.Scale, new Rectangle(startSeek, new Point(96, 6)));
        }

        private void DrawChordIndicators(SpriteBatch spriteBatch, SoundDriver soundDriver, Vector2 pos)
        {
            for (int i = 0; i < 4; i++)
            {
                if (i != (int)soundDriver.CurrentNoteMode - 1)
                    DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(40 * i, 0), PixelSynth.Scale, new Rectangle(new Point(0, 258), new Point(12, 13)));
                else
                    DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(40 * i, 0), PixelSynth.Scale, new Rectangle(new Point(0, 244), new Point(12, 13)));
            }
        }

        private void DrawScreenInfo(SpriteBatch spriteBatch, SoundDriver soundDriver, KeyController controller, Vector2 pos)
        {
            string note = String.Format
                (
                    "{0} {1}\n\n{2}",
                    controller.lastNote.ToString(),
                    controller.lastOctave.ToString(),
                    soundDriver.CurrentObertonator.ToString()
                ).Replace("Sharp ", "#");

            spriteBatch.DrawString(font, note, (pos + new Vector2()) * PixelSynth.Scale, fontColor);
        }

        private void DrawPresetPanel(SpriteBatch spriteBatch, SoundDriver soundDriver, Vector2 pos)
        {
            for (int i = 0; i < 8; i++)
            {
                if (i != (int)soundDriver.CurrentPreset)
                    DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(0, 16 * i), PixelSynth.Scale, new Rectangle(new Point(19 * i, 148), new Point(18, 15)));
                else
                    DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(0, 16 * i), PixelSynth.Scale, new Rectangle(new Point(19 * i, 164), new Point(18, 15)));
            }

            for (int i = 8; i < 16; i++)
            {
                if (i != (int)soundDriver.CurrentPreset)
                    DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(20, 16 * i - 128), PixelSynth.Scale, new Rectangle(new Point(19 * i, 148), new Point(18, 15)));
                else
                    DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(20, 16 * i - 128), PixelSynth.Scale, new Rectangle(new Point(19 * i, 164), new Point(18, 15)));
            }
        }

        private void DrawADSRPanel(SpriteBatch spriteBatch, SoundDriver soundDriver, Vector2 pos)
        {
            for (int i = 0; i < 8; i++)
            {
                if(i != (int)soundDriver.CurrentADSRMode)
                    DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(0, 16 * i), PixelSynth.Scale, new Rectangle(new Point(19 * i, 148), new Point(18, 15)));
                else
                    DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(0, 16 * i), PixelSynth.Scale, new Rectangle(new Point(19 * i, 164), new Point(18, 15)));
            }
        }

        private void DrawOscillatorPanel(SpriteBatch spriteBatch, SoundDriver soundDriver, Vector2 pos)
        {
            if(soundDriver.CurrentBaseWave == SoundDriver.BasicWave.Sine)
            {
                DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(0, 0), PixelSynth.Scale, new Rectangle(new Point(0, 228), new Point(16, 15)));
                DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(18, 0), PixelSynth.Scale, new Rectangle(new Point(17, 212), new Point(16, 15)));
                DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(36, 0), PixelSynth.Scale, new Rectangle(new Point(34, 212), new Point(16, 15)));
            }
            else if (soundDriver.CurrentBaseWave == SoundDriver.BasicWave.Square)
            {
                DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(0, 0), PixelSynth.Scale, new Rectangle(new Point(0, 212), new Point(16, 15)));
                DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(18, 0), PixelSynth.Scale, new Rectangle(new Point(17, 228), new Point(16, 15)));
                DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(36, 0), PixelSynth.Scale, new Rectangle(new Point(34, 212), new Point(16, 15)));
            }
            else if (soundDriver.CurrentBaseWave == SoundDriver.BasicWave.Sawtooth)
            {
                DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(0, 0), PixelSynth.Scale, new Rectangle(new Point(0, 212), new Point(16, 15)));
                DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(18, 0), PixelSynth.Scale, new Rectangle(new Point(17, 212), new Point(16, 15)));
                DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(36, 0), PixelSynth.Scale, new Rectangle(new Point(34, 228), new Point(16, 15)));
            }
        }

        private void DrawOctaveAt(SpriteBatch spriteBatch, Vector2 pos, int pressedTone)
        {
            //White keys
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(0, 0), PixelSynth.Scale, new Rectangle(new Point(19, 38), new Point(14, 32)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(13, 0), PixelSynth.Scale, new Rectangle(new Point(34, 38), new Point(14, 32)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(26, 0), PixelSynth.Scale, new Rectangle(new Point(19, 38), new Point(14, 32)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(39, 0), PixelSynth.Scale, new Rectangle(new Point(49, 38), new Point(14, 32)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(52, 0), PixelSynth.Scale, new Rectangle(new Point(34, 38), new Point(14, 32)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(65, 0), PixelSynth.Scale, new Rectangle(new Point(19, 38), new Point(14, 32)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(78, 0), PixelSynth.Scale, new Rectangle(new Point(0, 38), new Point(18, 32)));

            //Black keys
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(9, -1), PixelSynth.Scale, new Rectangle(new Point(0, 104), new Point(9, 21)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(35, -1), PixelSynth.Scale, new Rectangle(new Point(0, 104), new Point(9, 21)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(48, -1), PixelSynth.Scale, new Rectangle(new Point(0, 104), new Point(9, 21)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(74, -1), PixelSynth.Scale, new Rectangle(new Point(0, 104), new Point(9, 21)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(87, -1), PixelSynth.Scale, new Rectangle(new Point(0, 104), new Point(9, 21)));

            //Any pressed keys
        }
    }
}
