using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RobotShootans.Engine;
using RobotShootans.Entities;
using RobotShootans.Entities.Weapons;

namespace RobotShootans.Screens
{
    /// <summary>
    /// The screen where the game will be played
    /// </summary>
    public class GameplayScreen : GameScreen
    {
        Player _player;
        RobotSpawner _robotSpawner;
        PowerUpSpawner _powerUpSpawner;

        int _startTimer; // Used to start the game after a few seconds have passed
        bool _firstUpdate; // Used for stuff

        bool _gameStarted;

        /// <summary>
        /// Creates the GameplayScreen
        /// </summary>
        /// <param name="blockUpdating">sets whether the screen blocks updating of screens below it</param>
        public GameplayScreen(bool blockUpdating = false)
            : base(blockUpdating)
        {
            _screenName = "GAMEPLAY SCREEN";
        }

        /// <summary>
        /// Loads the content for the GameplayScreen
        /// </summary>
        public override void loadGameScreen()
        {
            _physicsWorld = new World(Vector2.Zero);

            TiledBackground bg = new TiledBackground("images/game/metal-bg", new Rectangle(0, 0, 1920, 1080));
            bg.DrawOrder = 0;
            addEntity(bg);

            _physicsEnabled = true;

            _firstUpdate = true;
            _gameStarted = false;

            _loaded = true;
        }

        /// <summary>
        /// Unloads all non-CM content
        /// </summary>
        public override void unloadGameScreen()
        {
            
        }

        /// <summary>
        /// Handles the events. Sorts out restarting the game and game over
        /// </summary>
        /// <param name="eventIn"></param>
        /// <returns></returns>
        public override bool HandleEvent(GameEvent eventIn)
        {
            bool returnV = false;

            if(eventIn.EventType == EventType.LIFE_LOST)
            {
                removeEntity(_player);
                removeEntity(_robotSpawner);
                removeEntity(_powerUpSpawner);
                removeEntity("CROSSHAIR");
                _startTimer = 0;
                _gameStarted = false;
                returnV = true;
            }
            else if(eventIn.EventType == EventType.GAME_OVER)
            {
                Engine.removeGameScreen(this);
                Engine.pushGameScreen(new GameOverScreen(true, eventIn.ChangeInt));
                returnV = true;
            }

            if (base.HandleEvent(eventIn))
                returnV = true;

            return returnV;
        }

        /// <summary>
        /// Updates the gameplay screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if(InputHelper.isKeyPressNew(Keys.Escape))
            {
                Engine.Exit();
            }

            if (_firstUpdate)
            {
                Engine.StopSong();
                Engine.StartSong("DST-TowerDefenseTheme_1");

                addEntity(new GUI_HUD());

                _startTimer = 0;
                _firstUpdate = false;
            }

            if(_startTimer > 2000 && !_gameStarted)
            {
                startGame();
            }
            
            if(_gameStarted)
            {
#if DEBUG
                if (InputHelper.isKeyPressNew(Keys.V))
                {
                    Engine.registerEvent(new GameEvent(EventType.WEAPON_CHANGED, new RocketLauncher()));
                }
                else if (InputHelper.isKeyPressNew(Keys.M))
                {
                    Engine.registerEvent(new GameEvent(EventType.WEAPON_CHANGED, new MachineGun()));
                }
                else if (InputHelper.isKeyPressNew(Keys.N))
                {
                    Engine.registerEvent(new GameEvent(EventType.WEAPON_CHANGED, new Shotgun()));
                }
                else if (InputHelper.isKeyPressNew(Keys.B))
                {
                    Engine.registerEvent(new GameEvent(EventType.WEAPON_CHANGED, new Pistol()));
                }
#endif

                _robotSpawner.playerPosition = _player.getPosition();
                _powerUpSpawner.PlayerPosition = _player.getPosition();
            }

            base.Update(gameTime);

            _physicsWorld.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            _startTimer += gameTime.ElapsedGameTime.Milliseconds;
        }

        /// <summary>
        /// Draws the gameplay screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="sBatch"></param>
        public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch sBatch)
        {
            base.Draw(gameTime, sBatch);
        }

        private void startGame()
        {
            _player = new Player(Engine.RenderOrigin);
            addEntity(_player);

            _robotSpawner = new RobotSpawner();
            addEntity(_robotSpawner);

            _powerUpSpawner = new PowerUpSpawner();
            addEntity(_powerUpSpawner);

            addEntity(new Crosshair());

            _gameStarted = true;
        }
    }
}
