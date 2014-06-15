using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotShootans.Engine;
using RobotShootans.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RobotShootans.Screens
{
    /// <summary>
    /// Displays the Splash Logos. Best to use as the first screen shown.
    /// </summary>
    public class SplashScreen : GameScreen
    {
        private int _splashTimer;
        private int _displayTime; // How long to display each logo for in milliseconds
        private int _fadeTime; // How long to fade each logo in/out in milliseconds
        private int _waitTime; // Time to wait before displaying logos
        private bool _firstUpdate, _addedFirstScreen, _addedSecondScreen, _addedThirdScreen;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blockUpdating"></param>
        public SplashScreen(bool blockUpdating = false)
            : base(blockUpdating)
        {

        }

        /// <summary>
        /// Loads the background and sets variables.
        /// </summary>
        public override void loadGameScreen()
        {
            _screenName = "SPLASH SCREEN";

            addEntity(new ColouredRectangle(new Rectangle(0, 0, 1920, 1080), Color.DarkGray));
            
            _firstUpdate = true;

            _addedFirstScreen = false;
            _addedSecondScreen = false;
            _addedThirdScreen = false;

            _displayTime = 5000;
            _fadeTime = 1000;
            _waitTime = 2000;

            _loaded = true;
        }

        /// <summary>
        /// Unloads non-managed content
        /// </summary>
        public override void unloadGameScreen()
        {
            
        }

        /// <summary>
        /// Sorts out displaying the logos properly
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (_firstUpdate)
            {
                _splashTimer = 0;
                _firstUpdate = false;
            }

            _splashTimer += gameTime.ElapsedGameTime.Milliseconds;

            if(Keyboard.GetState().IsKeyDown(Keys.Space))
                _splashTimer = 100000; // Should be enough to skip anything but the longest splash screens

            // Waits two seconds before doing anything because reasons.
            if(_splashTimer > _waitTime && !_addedFirstScreen)
            {
                addEntity(new SplashLogo("images/funkyrabbit logo", new Vector2(Engine.RenderWidth / 2, Engine.RenderHeight / 2), _displayTime, _fadeTime));
                _addedFirstScreen = true;
            }
            else if(_splashTimer > _waitTime + _displayTime && !_addedSecondScreen)
            {
                removeEntity("images/funkyrabbit logo");
                addEntity(new SplashLogo("images/MonogameLogo512x512", new Vector2(Engine.RenderWidth / 2, Engine.RenderHeight / 2), _displayTime, _fadeTime));
                _addedSecondScreen = true;
            }
            else if (_splashTimer > _waitTime + _displayTime * 2 && !_addedThirdScreen)
            {
                removeEntity("images/MonogameLogo512x512");
                addEntity(new SplashLogo("images/farseer-logo-512px", new Vector2(Engine.RenderWidth / 2, Engine.RenderHeight / 2), _displayTime, _fadeTime));
                _addedThirdScreen = true;
            }
            else if(_splashTimer > _waitTime + (_displayTime*3))
            {
                Engine.removeGameScreen(this);
                Engine.pushGameScreen(new GameplayScreen());
            }

            base.Update(gameTime);
        }
    }
}
