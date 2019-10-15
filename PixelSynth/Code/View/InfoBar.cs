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
            string drawn = String.Format
                (
                    "Note: {0}\nOctave: {1}\nFrequency: {2}\nPreset: {3}",
                    controller.lastNote.ToString(),
                    controller.lastOctave.ToString(),
                    Note.GetNote(controller.lastNote, controller.lastOctave).ToString(),
                    soundDriver.CurrentPreset.ToString()
                );

            spriteBatch.DrawString(font, drawn, Vector2.Zero, Color.Black);
        }
    }
}
