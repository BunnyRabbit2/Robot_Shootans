using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Engine
{
    public class GameScreen
    {
        public GameEngine Engine;

        private HashSet<GameComponent> _gameComponents;
        private bool _loaded;
        public bool Loaded { get { return _loaded; } }

        private bool _paused;
        public bool Paused { get { return _paused; } }
        public void Pause() { _paused = true; }
        public void Unpause() { _paused = false; }

        private bool _blockUpdating;
        public bool BlockUpdating { get { return _blockUpdating; } }

        private string _screenName;
        public string ScreenName { get { return _screenName; } }

        private HashSet<GameComponent> _componentsToRemove;
        
        public GameScreen(bool blockUpdating = false)
        {
            _componentsToRemove = new HashSet<GameComponent>();
        }

        public void loadGameScreen()
        {

        }

        public void unloadGameScreen()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch sBatch)
        {

        }

        public void addComponent(GameComponent componentIn, string nameIn = "")
        {

        }

        public void removeComponent(GameComponent componentIn)
        {

        }
    }
}
