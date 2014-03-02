#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using RobotShootans.Engine;
#endregion

namespace RobotShootans
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class RobotShootans : Game
    {
        private const int RENDERWIDTH = 1920;
        private const int RENDERHEIGHT = 1080;

        private const int WINDOWWIDTH = 960;
        private const int WINDOWHEIGHT = 600;

        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        Texture2D _bg;

        private ResolutionIndependentRenderer _resolutionIndependence;

        public RobotShootans()
            : base()
        {
            _graphics = new GraphicsDeviceManager(this);
            _resolutionIndependence = new ResolutionIndependentRenderer(this);
            Content.RootDirectory = "Content";

            _graphics.PreferredBackBufferWidth = WINDOWWIDTH;
            _graphics.PreferredBackBufferHeight = WINDOWHEIGHT;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            InitializeResolutionIndependence(_graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height);

            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _bg = Content.Load<Texture2D>("images/background");
        }

        private void InitializeResolutionIndependence(int realScreenWidth, int realScreenHeight)
        {
            _resolutionIndependence.SetWidthAndHeight(RENDERWIDTH, RENDERHEIGHT, realScreenWidth, realScreenHeight);
            _resolutionIndependence.Initialize();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            _resolutionIndependence.BeginDraw();
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, _resolutionIndependence.GetTransformationMatrix());
            _spriteBatch.Draw(_bg, new Vector2(), Color.White);
            _spriteBatch.End();

            Viewport check = _graphics.GraphicsDevice.Viewport;

            base.Draw(gameTime);
        }

    }
}
