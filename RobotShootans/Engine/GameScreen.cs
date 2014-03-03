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

        protected HashSet<GameComponent> _gameComponents;
        protected bool _loaded;
        public bool Loaded { get { return _loaded; } }

        protected bool _paused;
        public bool Paused { get { return _paused; } }
        public void Pause() { _paused = true; }
        public void Unpause() { _paused = false; }

        protected bool _blockUpdating;
        public bool BlockUpdating { get { return _blockUpdating; } }

        protected string _screenName;
        public string ScreenName { get { return _screenName; } }

        protected HashSet<GameComponent> _componentsToRemove;
        
        public GameScreen(bool blockUpdating = false)
        {
            _gameComponents = new HashSet<GameComponent>();
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
            if(!_paused)
            {
                foreach(GameComponent gc in _gameComponents)
                {
                    gc.Update(gameTime);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            foreach (GameComponent gc in _gameComponents)
            {
                gc.Draw(gameTime, sBatch);
            }
        }

        public void addComponent(GameComponent componentIn)
        {
            componentIn.GameScreen = this;
            componentIn.Load();
            _gameComponents.Add(componentIn);
        }

        public void removeComponent(GameComponent componentIn)
        {
            _componentsToRemove.Add(componentIn);
        }

        protected void removeComponenets()
        {
            if(_componentsToRemove.Count > 0)
            {
                foreach(GameComponent gc in _componentsToRemove)
                {
                    _gameComponents.Remove(gc);
                }
            }
        }
    }
}
