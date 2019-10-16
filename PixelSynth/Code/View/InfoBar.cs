using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelSynth.Code.Controller;
using PixelSynth.Code.Sound;
using System;

namespace PixelSynth.Code.View
{
    public class InfoBar
    {
        private SpriteFont font;

        public InfoBar(Game game)
        {
            font = game.Content.Load<SpriteFont>("res/font/PixelFont1");
        }

        public void Draw(SpriteBatch spriteBatch, KeyController controller, SoundDriver soundDriver)
        {
            string noteInfo = String.Format
                (
                    "Note: {0}\nOctave: {1}\nFrequency: {2}\nPreset: {3}\nWave: {4}",
                    controller.lastNote.ToString(),
                    controller.lastOctave.ToString(),
                    Note.GetNote(controller.lastNote, controller.lastOctave).ToString(),
                    soundDriver.CurrentPreset.ToString(),
                    soundDriver.CurrentBaseWave.ToString()
                );

            spriteBatch.DrawString(font, noteInfo, Vector2.Zero, Color.Black);

            string tooltip =
                "LShift - RShift: play\n" +
                "1 - 9: select preset\n" +
                "Num1 - Num3: wave\n";

            spriteBatch.DrawString(font, tooltip, new Vector2(PixelSynth.ScaledWindowSize.X - font.MeasureString(tooltip).X, 0), Color.Black);
        }
    }
}
