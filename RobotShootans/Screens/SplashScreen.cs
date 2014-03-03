using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotShootans.Engine;
using RobotShootans.Componenets;
using Microsoft.Xna.Framework;

namespace RobotShootans.Screens
{
    public class SplashScreen : GameScreen
    {
        public override void loadGameScreen()
        {
            addEntity(new SplashLogo("images/funkyrabbit logo", new Vector2(Engine.RenderWidth / 2, Engine.RenderHeight / 2)));

            _loaded = true;
        }

        public override void unloadGameScreen()
        {
            
        }
    }
}
