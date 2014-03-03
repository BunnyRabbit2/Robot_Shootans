using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Engine
{
    /// <summary>
    /// A singleton class used for handling all the game screens, input and other such things
    /// </summary>
    public class GameEngine
    {
        private const int RENDERWIDTH = 1920;
        private const int RENDERHEIGHT = 1080;

        private static GameEngine instance;

        private GameEngine()
        {
            _gameName = "Robot Shootans";
            _gameScreens = new HashSet<GameScreen>();
            _screensToRemove = new HashSet<GameScreen>();
        }

        public static GameEngine Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new GameEngine();
                }
                return instance;
            }
        }

        private string _gameName;
        public string GameName
        {
            get { return _gameName; }
        }
        private HashSet<GameScreen> _gameScreens;
        private HashSet<GameScreen> _screensToRemove;

        private ResolutionIndependentRenderer _resolutionIndependence;
        private Game _game;

        SpriteBatch _spriteBatch;
        Texture2D _bg;

        public void Initialise(string gameName, Game game)
        {
            _gameName = gameName;
            _game = game;

            _resolutionIndependence = new ResolutionIndependentRenderer(_game.GraphicsDevice);

            LogFile.ClearLogFile();
            LogFile.LogStringLine("Started Engine for game: " + _gameName, LogType.INFO);
        }

        public void LoadContent()
        {
            LogFile.LogStringLine("Loading Engine Content", LogType.INFO);

            InitializeResolutionIndependence(_game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height);

            _bg = _game.Content.Load<Texture2D>("images/background");

            _spriteBatch = new SpriteBatch(_game.GraphicsDevice);
        }

        private void InitializeResolutionIndependence(int realScreenWidth, int realScreenHeight)
        {
            _resolutionIndependence.SetWidthAndHeight(RENDERWIDTH, RENDERHEIGHT, realScreenWidth, realScreenHeight);
            _resolutionIndependence.Initialize();
        }

        public void pushGameScreen(GameScreen gameScreenIn)
        {
            if(gameScreenIn.Engine != null)
            {
                LogFile.LogStringLine("Failed to add screen " + gameScreenIn.ScreenName + " to the engine. It already belongs to a parent engine", LogType.ERROR);
                return;
            }

            gameScreenIn.Engine = this;
            gameScreenIn.loadGameScreen();
            _gameScreens.Add(gameScreenIn);
        }

        public void removeGameScreen(GameScreen screenToRemove)
        {

        }

        public void removeGameScreen(string screenToRemove)
        {

        }

        public void unloadScreens()
        {

        }

        public void Update(GameTime gameTime)
        {
            // TODO: create some sort of input helper
        }

        public void Draw(GameTime gameTime)
        {
            _resolutionIndependence.BeginDraw();
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, _resolutionIndependence.GetTransformationMatrix());
            _spriteBatch.Draw(_bg, new Vector2(), Color.White);
            _spriteBatch.End();
        }
    }
}
