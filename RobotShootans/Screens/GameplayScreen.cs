using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotShootans.Engine;
using RobotShootans.Entities;
using Microsoft.Xna.Framework;

namespace RobotShootans.Screens
{
    /// <summary>
    /// The screen where the game will be played
    /// </summary>
    public class GameplayScreen : GameScreen
    {

        /// <summary>
        /// Loads the content for the GameplayScreen
        /// </summary>
        public override void loadGameScreen()
        {
            _screenName = "GAMEPLAY SCREEN";

            addEntity( new TiledBackground("images/game/bg", new Rectangle(0,0,1920,1080)) );
            _loaded = true;
        }
    }
}
