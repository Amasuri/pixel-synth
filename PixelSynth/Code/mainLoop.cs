using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelSynth.Code.Controller;
using PixelSynth.Code.View;

namespace PixelSynth.Code
{
    public class PixelSynth : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private SoundDriver soundDriver;
        private KeyController controller;

        private InfoBar infoBar;

        public PixelSynth()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            soundDriver = new SoundDriver();
            controller = new KeyController();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            infoBar = new InfoBar(this);
        }

        protected override void Update(GameTime gameTime)
        {
            controller.Update(soundDriver);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            infoBar.Draw(spriteBatch, controller, soundDriver);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
