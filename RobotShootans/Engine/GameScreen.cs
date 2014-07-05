﻿using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>The container for the Entities</summary>
        protected EntityBag _entityBag;
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

        /// <summary>Sets whether the screen is physics enabled or not</summary>
        protected bool _physicsEnabled;
        /// <summary>Does the screen have a physics world?</summary>
        public bool PhysicsEnabled { get { return _physicsEnabled; } }
        /// <summary>The screens physics world</summary>
        protected World _physicsWorld;
        /// <summary>Public access to the screen's physics world</summary>
        public World PhysicsWorld { get { return _physicsWorld; } }
        #endregion

        /// <summary>
        /// Creates the GameScreen object
        /// </summary>
        /// <param name="blockUpdating">Sets if the screen blocks screens under it from updating</param>
        public GameScreen(bool blockUpdating = false)
        {
            _blockUpdating = blockUpdating;
            _entityBag = new EntityBag();
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
                _entityBag.Update(gameTime);
            }
        }

        /// <summary>
        /// Draws all the Entities
        /// </summary>
        /// <param name="gameTime">The GameTime object from the Engine</param>
        /// <param name="sBatch">The SpriteBatch from the Engine used for drawing</param>
        public virtual void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            _entityBag.Draw(gameTime, sBatch);
        }
        #endregion

        #region Entity management
        /// <summary>
        /// Adds an entity to the Screens set
        /// </summary>
        /// <param name="entityIn">Entity to be added</param>
        public void addEntity(GameEntity entityIn)
        {
            _entityBag.addEntity(entityIn, this);
        }

        /// <summary>
        /// Removes an entity passed in
        /// </summary>
        /// <param name="entityIn">The entity to remove</param>
        public void removeEntity(GameEntity entityIn)
        {
            _entityBag.removeEntity(entityIn);
        }

        /// <summary>
        /// Removes an entity by name
        /// </summary>
        /// <param name="entityIn"></param>
        public void removeEntity(string entityIn)
        {
            _entityBag.removeEntity(entityIn);
        }

        /// <summary>Gets all entities with the name given</summary>
        /// <param name="nameIn"></param>
        /// <returns></returns>
        public List<GameEntity> getEntityByName(string nameIn)
        {
            return _entityBag.getEntitiesByName(nameIn);
        }
        #endregion

        /// <summary>Gets all bodies with UserData matching the UserData in</summary>
        /// <param name="UserDataIn"></param>
        /// <returns></returns>
        public List<Body> getBodiesByUserData(object UserDataIn)
        {
            return _physicsWorld.BodyList.Where(b => b.UserData == UserDataIn).ToList<Body>();
        }

        /// <summary>Gets the physics entity with the body id given</summary>
        /// <param name="idIn"></param>
        /// <returns></returns>
        public GameEntity getEntityWithBodyID(int idIn)
        {
            var list = _entityBag.getPhysicsEntities();

            return list.First(e => e.PhysicsBody.BodyId == idIn);
        }
    }
}
