﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Engine
{
    /// <summary>
    /// A GameScreen that contains a set of entities to iterate through.
    /// </summary>
    public class GameScreen
    {
        #region variables
        /// <summary>The Engine that owns the screen</summary>
        public GameEngine Engine;

        /// <summary>The entities owned by the screen</summary>
        protected HashSet<GameEntity> _entities;
        /// <summary>Whether the screen is loaded or not</summary>
        protected bool _loaded;
        /// <summary>Whether the screen is loaded or not</summary>
        public bool Loaded { get { return _loaded; } }

        /// <summary>If the screen is to update or not</summary>
        protected bool _paused;
        /// <summary>If the screen is to update or not</summary>
        public bool Paused { get { return _paused; } }
        /// <summary>Pauses the screen and stops it from updating</summary>
        public void Pause() { _paused = true; }
        /// <summary>Unpauses the screen and lets it update</summary>
        public void Unpause() { _paused = false; }

        /// <summary>Whether the screen blocks screens under it from updating or not</summary>
        protected bool _blockUpdating;
        /// <summary>Whether the screen blocks screens under it from updating or not</summary>
        public bool BlockUpdating { get { return _blockUpdating; } }

        /// <summary>The screens name</summary>
        protected string _screenName;
        /// <summary>The screens name</summary>
        public string ScreenName { get { return _screenName; } }

        /// <summary>The entities to be removed after updating all entities</summary>
        protected HashSet<GameEntity> _entitiesToRemove;
        #endregion

        /// <summary>
        /// Creates the GameScreen object
        /// </summary>
        /// <param name="blockUpdating">Sets if the screen blocks screens under it from updating</param>
        public GameScreen(bool blockUpdating = false)
        {
            _entities = new HashSet<GameEntity>();
            _entitiesToRemove = new HashSet<GameEntity>();
        }

        #region Virtual functions
        /// <summary>
        /// Loads content for the GameScreen
        /// </summary>
        public virtual void loadGameScreen()
        {

        }

        /// <summary>
        /// Unloads any non-ContentManager content
        /// </summary>
        public virtual void unloadGameScreen()
        {

        }

        /// <summary>
        /// Updates all Entities
        /// </summary>
        /// <param name="gameTime">The GameTime object from the Engine</param>
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

            removeEntities();
        }

        /// <summary>
        /// Draws all the Entities
        /// </summary>
        /// <param name="gameTime">The GameTime object from the Engine</param>
        /// <param name="sBatch">The SpriteBatch from the Engine used for drawing</param>
        public virtual void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            foreach (GameEntity ge in _entities)
            {
                if (ge.Loaded)
                    ge.Draw(gameTime, sBatch);
            }
        }
        #endregion

        #region Entity management
        /// <summary>
        /// Adds an entity to the Screens set
        /// </summary>
        /// <param name="entityIn">Entity to be added</param>
        public void addEntity(GameEntity entityIn)
        {
            entityIn.Screen = this;
            entityIn.Load();
            _entities.Add(entityIn);
        }

        /// <summary>
        /// Removes an entity passed in
        /// </summary>
        /// <param name="entityIn">The entity to remove</param>
        public void removeEntity(GameEntity entityIn)
        {
            _entitiesToRemove.Add(entityIn);
        }

        /// <summary>
        /// Removes an entity by name
        /// </summary>
        /// <param name="entityIn"></param>
        public void removeEntity(string entityIn)
        {
            _entitiesToRemove.Add(_entities.FirstOrDefault(e => e.EntityName == entityIn));
        }

        /// <summary>
        /// Removes all entities set to be removed
        /// </summary>
        protected void removeEntities()
        {
            if(_entitiesToRemove.Count > 0)
            {
                foreach(GameEntity ge in _entitiesToRemove)
                {
                    _entities.Remove(ge);
                }
            }
        }
        #endregion
    }
}
