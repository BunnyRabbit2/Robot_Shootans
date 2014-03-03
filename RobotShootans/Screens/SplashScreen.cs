using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotShootans.Engine;
using RobotShootans.Componenets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RobotShootans.Screens
{
    public class SplashScreen : GameScreen
    {
        private int _splashTimer;

        Texture2D splashBG;

        public override void loadGameScreen()
        {
            addEntity(new SplashLogo("images/funkyrabbit logo", new Vector2(Engine.RenderWidth / 2, Engine.RenderHeight / 2)));

            _splashTimer = 0;

            _loaded = true;
        }

        public override void unloadGameScreen()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            _splashTimer += gameTime.ElapsedGameTime.Milliseconds;

            if(_splashTimer > 5000)
            {
                removeEntity("images/funkyrabbit logo");
                addEntity(new SplashLogo("images/MonogameLogo512x512", new Vector2(Engine.RenderWidth / 2, Engine.RenderHeight / 2)));
            }

            base.Update(gameTime);
        }
    }
}
