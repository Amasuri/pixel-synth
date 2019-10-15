using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PixelSynth.Code.Controller;
using PixelSynth.Code.Sound;
using PixelSynth.Code.View;
using System;
using System.IO;
using System.Media;

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

            infoBar = new InfoBar();
        }

        protected override void UnloadContent()
        {
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

            infoBar.Draw(spriteBatch, controller);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
