using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PixelSynth.Code.Sound;
using System;
using System.IO;
using System.Media;

namespace PixelSynth.Code
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class PixelSynth : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private SoundDriver soundDriver;

        public PixelSynth()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            soundDriver = new SoundDriver();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            var keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.Z))
                soundDriver.PlayPacket(Note.Type.C, 4);
            if (keys.IsKeyDown(Keys.X))
                soundDriver.PlayPacket(Note.Type.D, 4);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
