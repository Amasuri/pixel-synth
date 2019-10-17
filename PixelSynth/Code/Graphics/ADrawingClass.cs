using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Amasuri.Reusable.Graphics
{
    /// <summary>
    /// Defines rules that every drawing class should follow.
    /// </summary>
    abstract public class ADrawingClass
    {
        /// <summary>
        /// Wrapper method for quicker draw.
        /// </summary>
        protected void DrawTexture(SpriteBatch spriteBatch, Texture2D texture, Vector2 pos, int scale, SpriteEffects effects = SpriteEffects.None)
        {
            spriteBatch.Draw(texture, pos * scale, null, Color.White, 0.0f, Vector2.Zero, scale, effects, 0.0f);
        }

        protected void DrawTexturePart(SpriteBatch spriteBatch, Texture2D texture, Vector2 pos, int scale, Rectangle part, SpriteEffects effects = SpriteEffects.None)
        {
            spriteBatch.Draw(texture, pos * scale, part, Color.White, 0.0f, Vector2.Zero, scale, effects, 0.0f);
        }
    }
}
