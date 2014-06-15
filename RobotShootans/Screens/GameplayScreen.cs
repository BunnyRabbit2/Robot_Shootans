using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotShootans.Engine;
using RobotShootans.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RobotShootans.Screens
{
    /// <summary>
    /// The screen where the game will be played
    /// </summary>
    public class GameplayScreen : GameScreen
    {
        Player _player;
        RobotSpawner _robotSpawner;

        public GameplayScreen(bool blockUpdating = false)
            : base(blockUpdating)
        {

        }

        /// <summary>
        /// Loads the content for the GameplayScreen
        /// </summary>
        public override void loadGameScreen()
        {
            _screenName = "GAMEPLAY SCREEN";

            addEntity( new TiledBackground("images/game/metal-bg", new Rectangle(0,0,1920,1080)) );

            _player = new Player(Engine.RenderOrigin);
            addEntity(_player);

            _robotSpawner = new RobotSpawner();
            addEntity(_robotSpawner);

            addEntity(new Crosshair());

            _loaded = true;
        }

        /// <summary>
        /// Unloads all non-CM content
        /// </summary>
        public override void unloadGameScreen()
        {
            
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

            _robotSpawner.playerPosition = _player.getPosition();

            base.Update(gameTime);
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
    }
}
