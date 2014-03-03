using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        public int RenderWidth { get { return RENDERWIDTH; } }

        private const int RENDERHEIGHT = 1080;
        public int RenderHeight { get { return RENDERHEIGHT; } }

        private static GameEngine instance;

        private GameEngine()
        {
            _gameName = "Robot Shootans";
            _gameScreens = new HashSet<GameScreen>();
            _screensToRemove = new HashSet<GameScreen>();
            _loaded = false;
        }

        ~GameEngine()
        {
            LogFile.LogStringLine("Closing GameEngine down", LogType.INFO);
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

        private SpriteBatch _spriteBatch;
        private Texture2D _bg;

        private bool _loaded;

        private ContentManager _content;
        public ContentManager Content { get { return _content; } }

        public void Initialise(string gameName, Game game)
        {
            _gameName = gameName;
            _game = game;
            _content = game.Content;

            _resolutionIndependence = new ResolutionIndependentRenderer(_game.GraphicsDevice);

            LogFile.ClearLogFile();
            LogFile.LogStringLine("Started Engine for game: " + _gameName, LogType.INFO);
        }

        public void LoadContent()
        {
            LogFile.LogStringLine("Loading Engine Content", LogType.INFO);

            InitializeResolutionIndependence(_game.GraphicsDevice.Viewport.Width, _game.GraphicsDevice.Viewport.Height);
            _resolutionIndependence.BackgroundColor = Color.Black;

            _bg = _game.Content.Load<Texture2D>("images/background");

            _spriteBatch = new SpriteBatch(_game.GraphicsDevice);

            _loaded = true;
            LogFile.LogStringLine("Done loading content", LogType.INFO);
        }

        private void InitializeResolutionIndependence(int realScreenWidth, int realScreenHeight)
        {
            _resolutionIndependence.SetWidthAndHeight(RENDERWIDTH, RENDERHEIGHT, realScreenWidth, realScreenHeight);
            _resolutionIndependence.Initialize();
        }

        public void pushGameScreen(GameScreen gameScreenIn, bool paused = false)
        {
            if(gameScreenIn.Engine != null)
            {
                LogFile.LogStringLine("Failed to add screen " + gameScreenIn.ScreenName + " to the engine. It already belongs to a parent engine", LogType.ERROR);
                return;
            }

            gameScreenIn.Engine = this;
            gameScreenIn.loadGameScreen();
            if (paused)
                gameScreenIn.Pause();
            _gameScreens.Add(gameScreenIn);
        }

        public void removeGameScreen(GameScreen screenToRemove)
        {
            _screensToRemove.Add(screenToRemove);
        }

        public void removeGameScreen(string screenToRemove)
        {
            GameScreen s = _gameScreens.FirstOrDefault(gs => gs.ScreenName == screenToRemove);
            if (s != null)
                _screensToRemove.Add(s);
            else
                LogFile.LogStringLine("Could not find screen named " + screenToRemove + "in Engine's Game Screens. Nothing will be done", LogType.ERROR);
        }

        private void removeScreens()
        {
            if(_screensToRemove.Count > 0)
            {
                foreach(GameScreen gs in _screensToRemove)
                {
                    _gameScreens.Remove(gs);
                }
                _screensToRemove.Clear();
            }
        }

        public void UnloadContent()
        {
            unloadScreens();
        }

        public void unloadScreens()
        {
            LogFile.LogStringLine("Unloading all screens", LogType.INFO);

            foreach(GameScreen s in _gameScreens)
            {
                s.unloadGameScreen();
            }
            _gameScreens.Clear();

            LogFile.LogStringLine("Finished unloading all screens", LogType.INFO);
        }

        public void Update(GameTime gameTime)
        {
            // TODO: create some sort of input helper

            if(_loaded)
            {
                foreach (GameScreen gs in _gameScreens)
                {
                    if (gs.Loaded)
                        gs.Update(gameTime);

                    if (gs.BlockUpdating)
                        break;
                }

                removeScreens();
            }
            else
            {
                // Just in case for some weird reason it hasn't loaded already
                LoadContent();
            }
        }

        public void Draw(GameTime gameTime)
        {
            _resolutionIndependence.BeginDraw();
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullNone, null, _resolutionIndependence.GetTransformationMatrix());
            _spriteBatch.Draw(_bg, new Vector2(), Color.White);

            foreach(GameScreen gs in _gameScreens)
            {
                if (gs.Loaded)
                    gs.Draw(gameTime, _spriteBatch);
            }

            _spriteBatch.End();
        }
    }
}
