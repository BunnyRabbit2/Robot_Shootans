using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotShootans.Engine;
using RobotShootans.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RobotShootans.Screens
{
    public class SplashScreen : GameScreen
    {
        private int _splashTimer;
        private bool _firstUpdate, _addedFirstScreen, _addedSecondScreen;

        public override void loadGameScreen()
        {
            addEntity(new ColouredRectangle(new Rectangle(0, 0, 1920, 1080), Color.DarkGray));
            
            _firstUpdate = true;

            _addedFirstScreen = false;
            _addedSecondScreen = false;

            _loaded = true;
        }

        public override void unloadGameScreen()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (_firstUpdate)
            {
                _splashTimer = 0;
                _firstUpdate = false;
            }

            _splashTimer += gameTime.ElapsedGameTime.Milliseconds;

            if(_splashTimer > 1000 && !_addedFirstScreen)
            {
                addEntity(new SplashLogo("images/funkyrabbit logo", new Vector2(Engine.RenderWidth / 2, Engine.RenderHeight / 2)));
                _addedFirstScreen = true;
            }
            else if(_splashTimer > 6000 && !_addedSecondScreen)
            {
                removeEntity("images/funkyrabbit logo");
                addEntity(new SplashLogo("images/MonogameLogo512x512", new Vector2(Engine.RenderWidth / 2, Engine.RenderHeight / 2)));
                _addedSecondScreen = true;
            }
            else if(_splashTimer > 11000)
            {
                Engine.removeGameScreen(this);
            }

            base.Update(gameTime);
        }
    }
}
