using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelSynth.Code.Controller;
using PixelSynth.Code.View;

namespace PixelSynth.Code
{
    public class PixelSynth : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public const int Scale = 3;
        static public Vector2 UnscaledWindowSize => new Vector2(400, 300);
        static public Vector2 ScaledWindowSize => UnscaledWindowSize * Scale;

        private SoundDriver soundDriver;
        private KeyController controller;

        private InfoBar infoBar;
        private SynthesizerView synthesizerView;

        public PixelSynth()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = (int)ScaledWindowSize.X;
            graphics.PreferredBackBufferHeight = (int)ScaledWindowSize.Y;
            this.IsMouseVisible = true;

            graphics.ApplyChanges();
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
            synthesizerView = new SynthesizerView(this);
        }

        protected override void Update(GameTime gameTime)
        {
            controller.Update(soundDriver);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightSlateGray);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            synthesizerView.Draw(spriteBatch, controller, soundDriver);
            infoBar.Draw(spriteBatch, controller, soundDriver);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
