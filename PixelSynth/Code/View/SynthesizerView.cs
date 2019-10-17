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
        private Texture2D baseSprite;
        private Texture2D buttonTileset;
        private readonly Vector2 synthsPos;

        public SynthesizerView(Game game)
        {
            baseSprite = game.Content.Load<Texture2D>("res/image/base");
            buttonTileset = game.Content.Load<Texture2D>("res/image/buttons");

            synthsPos = (PixelSynth.UnscaledWindowSize - baseSprite.Bounds.Size.ToVector2()) / 2;
        }

        public void Draw(SpriteBatch spriteBatch, KeyController controller, SoundDriver soundDriver)
        {
            //Layout
            DrawTexture(spriteBatch, baseSprite, synthsPos, PixelSynth.Scale);

            //Octaves
            DrawOctaveAt(spriteBatch, synthsPos + new Vector2(6, 158), -1);
            DrawOctaveAt(spriteBatch, synthsPos + new Vector2(6, 95), -1);
            DrawOctaveAt(spriteBatch, synthsPos + new Vector2(6, 32), -1);
        }

        private void DrawOctaveAt(SpriteBatch spriteBatch, Vector2 pos, int pressedTone)
        {
            //White keys
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(0, 0), PixelSynth.Scale, new Rectangle(new Point(0, 38), new Point(18, 32)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(17, 0), PixelSynth.Scale, new Rectangle(new Point(19, 38), new Point(14, 32)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(30, 0), PixelSynth.Scale, new Rectangle(new Point(34, 38), new Point(14, 32)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(43, 0), PixelSynth.Scale, new Rectangle(new Point(49, 38), new Point(14, 32)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(56, 0), PixelSynth.Scale, new Rectangle(new Point(19, 38), new Point(14, 32)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(69, 0), PixelSynth.Scale, new Rectangle(new Point(34, 38), new Point(14, 32)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(82, 0), PixelSynth.Scale, new Rectangle(new Point(19, 38), new Point(14, 32)));

            //Black keys
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(0, -1), PixelSynth.Scale, new Rectangle(new Point(0, 104), new Point(9, 21)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(13, -1), PixelSynth.Scale, new Rectangle(new Point(0, 104), new Point(9, 21)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(39, -1), PixelSynth.Scale, new Rectangle(new Point(0, 104), new Point(9, 21)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(52, -1), PixelSynth.Scale, new Rectangle(new Point(0, 104), new Point(9, 21)));
            DrawTexturePart(spriteBatch, buttonTileset, pos + new Vector2(78, -1), PixelSynth.Scale, new Rectangle(new Point(0, 104), new Point(9, 21)));

            //Any pressed keys
        }
    }
}
