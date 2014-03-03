﻿using Microsoft.Xna.Framework;
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

        protected HashSet<GameEntity> _entities;
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

        protected HashSet<GameEntity> _entitiesToRemove;
        
        public GameScreen(bool blockUpdating = false)
        {
            _entities = new HashSet<GameEntity>();
            _entitiesToRemove = new HashSet<GameEntity>();
        }

        public virtual void loadGameScreen()
        {

        }

        public virtual void unloadGameScreen()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            if(!_paused)
            {
                foreach(GameEntity ge in _entities)
                {
                    if(ge.Loaded)
                        ge.Update(gameTime);
                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            foreach (GameEntity ge in _entities)
            {
                if (ge.Loaded)
                    ge.Draw(gameTime, sBatch);
            }
        }

        public virtual void addEntity(GameEntity entityIn)
        {
            entityIn.Screen = this;
            entityIn.Load();
            _entities.Add(entityIn);
        }

        public virtual void removeEntity(GameEntity entityIn)
        {
            _entitiesToRemove.Add(entityIn);
        }

        public virtual void removeEntity(string entityIn)
        {
            _entitiesToRemove.Add(_entities.FirstOrDefault(e => e.ComponentName == entityIn));
        }

        protected virtual void removeComponenets()
        {
            if(_entitiesToRemove.Count > 0)
            {
                foreach(GameEntity ge in _entitiesToRemove)
                {
                    _entities.Remove(ge);
                }
            }
        }
    }
}
