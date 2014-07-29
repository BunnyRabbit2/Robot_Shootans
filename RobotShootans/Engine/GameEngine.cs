using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NVorbis;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RobotShootans.Engine
{
    /// <summary>
    /// The way the game is controlled
    /// </summary>
    public enum ControlType
    {
        /// <summary>Controlled by mouse and keyboard</summary>
        MOUSE_AND_KEYBOARD,
        /// <summary>Controlled by touchscreen</summary>
        TOUCHSCREEN,
        /// <summary>Controlled by Xbox type game pad</summary>
        XINPUT_GAMEPAD
    }

    /// <summary>
    /// A singleton class used for handling all the game screens, input and other such things
    /// </summary>
    public class GameEngine
    {
        #region Constants
        private const int RENDERWIDTH = 1920;
        /// <summary>The Width of the viewport that everything will be rendered to</summary>
        public int RenderWidth { get { return RENDERWIDTH; } }

        private const int RENDERHEIGHT = 1080;
        /// <summary>The Height of the viewport that everything will be rendered to</summary>
        public int RenderHeight { get { return RENDERHEIGHT; } }

        private const int RENDERCENTERX = RENDERWIDTH / 2;
        private const int RENDERCENTERY = RENDERHEIGHT / 2;
        /// <summary>
        /// Gets the center of the screen in render co-ords
        /// </summary>
        public Vector2 RenderOrigin { get { return new Vector2(RENDERCENTERX, RENDERCENTERY); } }
        #endregion

        #region Singleton stuff
        private static GameEngine instance;

        /// <summary>
        /// Private constructor because this will be a Singleton class
        /// </summary>
        private GameEngine()
        {
            _gameName = "Game Engine";
            _gameScreens = new List<GameScreen>();
            _gameScreensToAdd = new List<GameScreen>();
            _screensToRemove = new List<GameScreen>();
            _gameEvents = new List<GameEvent>();
            _gameOptions = new GameOptions();
            _gameOptions.LoadOptions();
            _oldOptions = _gameOptions;

            _loaded = false;
        }

        /// <summary>
        /// GameEngine destructor
        /// </summary>
        ~GameEngine()
        {
            LogFile.LogStringLine("Closing GameEngine down", LogType.INFO);
        }

        /// <summary>
        /// The access to the Singleton of GameEngine
        /// </summary>
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
        #endregion

        #region Variables
        private string _gameName;
        /// <summary> The name of the game</summary>
        public string GameName
        {
            get { return _gameName; }
        }
        private List<GameScreen> _gameScreens;
        private List<GameScreen> _gameScreensToAdd;
        private List<GameScreen> _screensToRemove;

        private List<GameEvent> _gameEvents;
        /// <summary>Registers the Event to the queue</summary>
        /// <param name="eventIn">The event to be registered</param>
        public void registerEvent(GameEvent eventIn) { _gameEvents.Add(eventIn); }

        private ResolutionIndependentRenderer _resolutionIndependence;
        private Game _game;
        private GraphicsDeviceManager _graphicsDeviceManager;

        private SpriteBatch _spriteBatch;
        private Texture2D _bg;

        private bool _loaded;

        GameOptions _gameOptions, _oldOptions;
        /// <summary>Public access to seeing what the options are</summary>
        public GameOptions Options { get { return _gameOptions; } }

        private ContentManager _content;
        /// <summary>The Content Manager owned by the Engine</summary>
        public ContentManager Content { get { return _content; } }

        string _currentSong = "";
        OggStream _song;

        private GraphicsDevice _graphics;
        /// <summary>The GraphicsDevice used by the Engine</summary>
        public GraphicsDevice Graphics { get { return _graphics; } }

        private bool _exitGame;
        /// <summary>
        /// A readonly value for if the game is exiting
        /// </summary>
        public bool ExitGame { get { return _exitGame; } }
        #endregion

        #region Set up and clean up functions
        /// <summary>
        /// Initialises the GameEngine. Must be called before using the GameEngine instance
        /// </summary>
        /// <param name="gameName">The name of the game being run</param>
        /// <param name="game">The Game class that will contain the GameEngine</param>
        /// <param name="gdmIn">The graphics device manager of the game class</param>
        public void Initialise(string gameName, Game game, GraphicsDeviceManager gdmIn)
        {
            _gameName = gameName;
            _game = game;
            _content = game.Content;
            _graphics = game.GraphicsDevice;
            _exitGame = false;
            _graphicsDeviceManager = gdmIn;

            _resolutionIndependence = new ResolutionIndependentRenderer(_graphics);

            InputHelper.InitialiseInputHelper();

            LogFile.ClearLogFile();
            LogFile.LogStringLine("Started Engine for game: " + _gameName, LogType.INFO);
            LogFile.LogStringLine("Version number: " + Assembly.GetEntryAssembly().GetName().Version.ToString());
        }

        /// <summary>
        /// Loads all of the content needed for the GameEngine and initialises some stuff. Must be called before using the GameEngine.
        /// </summary>
        public void LoadContent()
        {
            LogFile.LogStringLine("Loading Engine Content", LogType.INFO);

            SoundEffect.MasterVolume = _gameOptions.SfxVolume / 10f;
            if (!_gameOptions.SfxOn)
                SoundEffect.MasterVolume = 0.0f;

            if (_gameOptions.FullScreen)
            {
                _graphicsDeviceManager.IsFullScreen = true;
                _graphicsDeviceManager.PreferredBackBufferWidth = _graphics.DisplayMode.Width;
                _graphicsDeviceManager.PreferredBackBufferHeight = _graphics.DisplayMode.Height;
                InitializeResolutionIndependence(_graphics.DisplayMode.Width, _graphics.DisplayMode.Height);
            }
            else
            {
                _graphicsDeviceManager.PreferredBackBufferWidth = _gameOptions.WindowWidth;
                _graphicsDeviceManager.PreferredBackBufferHeight = _gameOptions.WindowHeight;
                InitializeResolutionIndependence(_gameOptions.WindowWidth, _gameOptions.WindowHeight);
            }
            _graphicsDeviceManager.ApplyChanges();
            
            _bg = _game.Content.Load<Texture2D>("images/background");

            _spriteBatch = new SpriteBatch(_graphics);

            _loaded = true;
            LogFile.LogStringLine("Done loading content", LogType.INFO);
        }

        /// <summary>
        /// Sets the engine to exit at the end of the current update loop
        /// </summary>
        public void Exit()
        {
            _exitGame = true;
        }

        /// <summary>
        /// Unloads any non-ContentManager content
        /// </summary>
        public void UnloadContent()
        {
            _gameOptions.WriteOptions();
            StopSong();
            unloadScreens();
        }

        /// <summary>
        /// Unloads all screen content and clears the screen set
        /// </summary>
        public void unloadScreens()
        {
            LogFile.LogStringLine("Unloading all screens", LogType.INFO);

            foreach (GameScreen s in _gameScreens)
            {
                s.unloadGameScreen();
            }
            _gameScreens.Clear();

            LogFile.LogStringLine("Finished unloading all screens", LogType.INFO);
        }
        #endregion

        #region Screen Management
        /// <summary>
        /// Sets up or resets the ResolutionIndependence if the screen size has changed.
        /// </summary>
        /// <param name="realScreenWidth">The width of the window to be displayed in</param>
        /// <param name="realScreenHeight">The height of the window to be displayed in</param>
        private void InitializeResolutionIndependence(int realScreenWidth, int realScreenHeight)
        {
            _resolutionIndependence.SetWidthAndHeight(RENDERWIDTH, RENDERHEIGHT, realScreenWidth, realScreenHeight);
            _resolutionIndependence.Initialize();
        }

        /// <summary>
        /// Adds a GameScreen to the Engine
        /// </summary>
        /// <param name="gameScreenIn">The game screen to be added</param>
        /// <param name="paused">Whether the GameScreen is initially paused</param>
        public void pushGameScreen(GameScreen gameScreenIn, bool paused = false)
        {
            if(string.IsNullOrEmpty(gameScreenIn.ScreenName))
            {
                LogFile.LogStringLine("Failed to add screen of type " + gameScreenIn.GetType().ToString() + " to the engine. It's screen name was not set in the constructor", LogType.ERROR);
                return;
            }

            if(gameScreenIn.Engine != null)
            {
                LogFile.LogStringLine("Failed to add screen " + gameScreenIn.ScreenName + " to the engine. It already belongs to a parent engine", LogType.ERROR);
                return;
            }

            if(_gameScreens.FirstOrDefault(gs => gs.ScreenName == gameScreenIn.ScreenName) != null
                || _gameScreensToAdd.FirstOrDefault(gs => gs.ScreenName == gameScreenIn.ScreenName) != null)
            {
                LogFile.LogStringLine("Failed to add screen " + gameScreenIn.ScreenName + " to the engine. There's already a screen with that name there", LogType.ERROR);
                return;
            }

            gameScreenIn.Engine = this;
            gameScreenIn.loadGameScreen();
            if (paused)
                gameScreenIn.Pause();
            _gameScreensToAdd.Add(gameScreenIn);
        }

        /// <summary>
        /// Adds all screens to be added
        /// </summary>
        private void addGameScreens()
        {
            foreach(GameScreen s in _gameScreensToAdd)
            {
                _gameScreens.Add(s);
            }
            _gameScreensToAdd.Clear();
        }

        /// <summary>
        /// Removes the screen passed to the Engine
        /// </summary>
        /// <param name="screenToRemove">The screen to remove</param>
        public void removeGameScreen(GameScreen screenToRemove)
        {
            _screensToRemove.Add(screenToRemove);
        }

        /// <summary>
        /// Removes the screen with the name given from the Engine
        /// </summary>
        /// <param name="screenToRemove">Name of the screen to remove</param>
        public void removeGameScreen(string screenToRemove)
        {
            GameScreen s = _gameScreens.FirstOrDefault(gs => gs.ScreenName == screenToRemove);
            if (s != null)
                _screensToRemove.Add(s);
            else
                LogFile.LogStringLine("Could not find screen named " + screenToRemove + "in Engine's Game Screens. Nothing will be done", LogType.ERROR);
        }

        /// <summary>
        /// Removes all screens set to be removed at the end of the main loop
        /// </summary>
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

        /// <summary>
        /// Checks if the engine contains a screen with the name given
        /// </summary>
        /// <param name="nameIn">The name of the screen to look for</param>
        /// <returns>If the game screen is in the stack</returns>
        public bool containsScreen(string nameIn)
        {
            return _gameScreens.Any(s => s.ScreenName == nameIn);
        }
        #endregion

        #region Main Loop functions
        /// <summary>
        /// Updates all screens if the content is loaded
        /// </summary>
        /// <param name="gameTime">The GameTime object handed in from the Game class</param>
        public void Update(GameTime gameTime)
        {
            InputHelper.UpdateInput();

            if (_loaded)
            {
                // Handle all the events in the list.
                //  Possibly make the event list a queue at some point
                for (int i = 0; i < _gameEvents.Count; i++ )
                {
                    GameEvent e = _gameEvents[i];

                    for (int i2 = _gameScreens.Count - 1; i2 >= 0; i2--)
                    {
                        if (_gameScreens[i2].Loaded)
                            if (_gameScreens[i2].HandleEvent(e))
                            {
                                _gameEvents.Remove(e);
                                i--;
                            }

                        if (_gameScreens[i2].BlockUpdating)
                            break;
                    }
                }

                // Iterate back to front. When hitting a blocking screen, break the loop
                for (int i = _gameScreens.Count - 1; i >= 0; i-- )
                {
                    if (_gameScreens[i].Loaded)
                        _gameScreens[i].Update(gameTime);

                    if (_gameScreens[i].BlockUpdating)
                        break;
                }

                removeScreens();
                addGameScreens();

                checkOptions();

                if (_exitGame)
                    _game.Exit();
            }
            else
            {
                // Just in case for some weird reason it hasn't loaded already
                LoadContent();
            }
        }

        /// <summary>
        /// Sets up the screen to draw and draws all screens
        /// </summary>
        /// <param name="gameTime">The GameTime object handed in from the Game class</param>
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
        #endregion

        #region other stuff
        /// <summary>
        /// Gets render position of the mouse
        /// </summary>
        /// <returns></returns>
        public Vector2 GetMousePosition()
        {
            return _resolutionIndependence.ScaleMouseToScreenCoordinates(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
        }

        private void checkOptions()
        {
            if(_gameOptions.OptionsChanged)
            {
                if(_song != null)
                {
                    _song.Volume = _gameOptions.MusicVolume / 10f;
                }
                if (!_gameOptions.MusicOn)
                    _song.Volume = 0.0f;

                SoundEffect.MasterVolume = _gameOptions.SfxVolume / 10f;
                if (!_gameOptions.SfxOn)
                    SoundEffect.MasterVolume = 0.0f;

                bool applyChanges = false;
                if (_gameOptions.FullScreen && !_oldOptions.FullScreen)
                {
                    _graphicsDeviceManager.IsFullScreen = true;
                    _graphicsDeviceManager.PreferredBackBufferWidth = _graphics.DisplayMode.Width;
                    _graphicsDeviceManager.PreferredBackBufferHeight = _graphics.DisplayMode.Height;
                    InitializeResolutionIndependence(_graphics.DisplayMode.Width, _graphics.DisplayMode.Height);
                    applyChanges = true;
                }
                else if (!_gameOptions.FullScreen && _oldOptions.FullScreen)
                {
                    _graphicsDeviceManager.PreferredBackBufferWidth = _gameOptions.WindowWidth;
                    _graphicsDeviceManager.PreferredBackBufferHeight = _gameOptions.WindowHeight;
                    InitializeResolutionIndependence(_gameOptions.WindowWidth, _gameOptions.WindowHeight);
                    applyChanges = true;
                }
                if (applyChanges)
                    _graphicsDeviceManager.ApplyChanges();

                _gameOptions.OptionsChanged = false;
                _oldOptions = _gameOptions;
            }
        }
        #endregion

        #region Content loading
        /// <summary>
        /// Loads a texture in a safe way and returns the texture asked for or a default if it fails to find it.
        /// Already includes "Content/image/" in the path to load
        /// </summary>
        /// <param name="textureToLoad">The texture to load</param>
        /// <returns></returns>
        public Texture2D loadTexture(string textureToLoad)
        {
            if (File.Exists("Content/images/" + textureToLoad + ".xnb"))
            {
                return Content.Load<Texture2D>("images/" + textureToLoad);
            }
            else
            {
                LogFile.LogStringLine("Failed to load image: " + textureToLoad + ". Nothing loaded", LogType.ERROR);
                return Content.Load<Texture2D>("images/default");
            }
        }

        /// <summary>
        /// Loads a spritefont if able. Returns null if not
        /// </summary>
        /// <param name="fontToLoad"></param>
        /// <returns></returns>
        public SpriteFont loadFont(string fontToLoad)
        {
            if(File.Exists("Content/fonts/" + fontToLoad + ".xnb"))
            {
                return Content.Load<SpriteFont>("fonts/" + fontToLoad);
            }
            else
            {
                LogFile.LogStringLine("Failed to load font: " + fontToLoad + ". Nothing loaded", LogType.ERROR);
                return null;
            }
        }

        /// <summary>
        /// Loads a sound effect is able. Returns null is not
        /// </summary>
        /// <param name="soundToLoad"></param>
        /// <returns></returns>
        public SoundEffect loadSound(string soundToLoad)
        {
            if (File.Exists("Content/sound/" + soundToLoad + ".xnb"))
            {
                return Content.Load<SoundEffect>("sound/" + soundToLoad);
            }
            else
            {
                LogFile.LogStringLine("Failed to load sound: " + soundToLoad + ". Nothing loaded", LogType.ERROR);
                return null;
            }
        }

        /// <summary>
        /// Starts the song given and stops any other song playing
        /// </summary>
        /// <param name="songToStart"></param>
        /// /// <param name="restartSong"></param>
        public void StartSong(string songToStart, bool restartSong = false)
        {
            if (_song != null)
            {
                if (songToStart != _song.SongName || restartSong)
                {
                    StopSong();
                }
                else
                {
                    return;
                }
            }

            if (File.Exists("Content/music/" + songToStart + ".ogg"))
            {
                LogFile.LogStringLine("Starting Song file named " + songToStart);
                _currentSong = songToStart;

                _song = new OggStream("Content/music/" + songToStart + ".ogg");
                _song.Volume = _gameOptions.MusicVolume / 10f;
                if (!_gameOptions.MusicOn)
                    _song.Volume = 0.0f;
                _song.Play();
            }
        }

        /// <summary>
        /// Stops whatever song is currently playing
        /// </summary>
        public void StopSong()
        {
            if (_song != null)
            {
                _song.Stop();
                _song.Dispose();
                _song = null;
            }
        }

        #endregion
    }
}
