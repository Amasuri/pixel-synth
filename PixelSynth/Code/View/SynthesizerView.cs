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
    public class SynthesizerView
    {
        private Texture2D baseSprite;
        private Texture2D buttonTileset;

        public SynthesizerView(Game game)
        {
            baseSprite = game.Content.Load<Texture2D>("res/image/base");
            buttonTileset = game.Content.Load<Texture2D>("res/image/buttons");
        }

        public void Draw(SpriteBatch spriteBatch, KeyController controller, SoundDriver soundDriver)
        {
            ;
        }
    }
}
