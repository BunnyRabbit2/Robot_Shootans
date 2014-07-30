#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
        GraphicsDeviceManager _graphics;
        GameEngine _engine;

        private bool _firstUpdate;

        /// <summary>
        /// Creates the RobotShootans game object
        /// </summary>
        public RobotShootans()
            : base()
        {
            _graphics = new GraphicsDeviceManager(this);

            _engine = GameEngine.Instance;

            Content.RootDirectory = "Content";

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
            _engine.Initialise("Robot Shootans", this, _graphics);
            
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

            if (InputHelper.isButtonPressNew(Buttons.Back, currentPlayer))
                Exit();

#if DEBUG
            if (InputHelper.isKeyPressNew(Keys.F2))
            {
                if (_engine.containsScreen("PHYSICS TEST SCREEN"))
                    _engine.removeGameScreen("PHYSICS TEST SCREEN");
                else
                    _engine.pushGameScreen(new PhysicsTestScreen(true));
            }
            if (InputHelper.isKeyPressNew(Keys.F3))
            {
                if (_engine.containsScreen("PHYSICS TEST SCREEN TWO"))
                    _engine.removeGameScreen("PHYSICS TEST SCREEN TWO");
                else
                    _engine.pushGameScreen(new PhysicsTestScreenTwo(true));
            }
            if (InputHelper.isKeyPressNew(Keys.F12))
            {
                if (_engine.containsScreen("GAME OVER SCREEN"))
                    _engine.removeGameScreen("GAME OVER SCREEN");
                else
                    _engine.pushGameScreen(new GameOverScreen(true));
            }
#endif

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
