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
using RobotShootans.Screens;
#endregion

namespace RobotShootans
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class RobotShootans : Game
    {
        private const int WINDOWWIDTH = 960;
        private const int WINDOWHEIGHT = 600;

        GraphicsDeviceManager _graphics;
        GameEngine _engine;

        private bool _firstUpdate;

        public RobotShootans()
            : base()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _graphics.PreferredBackBufferWidth = WINDOWWIDTH;
            _graphics.PreferredBackBufferHeight = WINDOWHEIGHT;

            _engine = GameEngine.Instance;

            _firstUpdate = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _engine.Initialise("Robot Shootans", this);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _engine.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            _engine.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            PlayerIndex currentPlayer = PlayerIndex.One;

            // get first connected player
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
                currentPlayer = PlayerIndex.One;
            else if (GamePad.GetState(PlayerIndex.Two).IsConnected)
                currentPlayer = PlayerIndex.Two;
            else if (GamePad.GetState(PlayerIndex.Three).IsConnected)
                currentPlayer = PlayerIndex.Three;
            else if (GamePad.GetState(PlayerIndex.Four).IsConnected)
                currentPlayer = PlayerIndex.Four;

            if (GamePad.GetState(currentPlayer).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_firstUpdate)
            {
                _engine.pushGameScreen(new SplashScreen());
                _firstUpdate = false;
            }
            _engine.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            _engine.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
