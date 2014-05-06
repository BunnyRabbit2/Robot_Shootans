using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Engine
{
    /// <summary>
    /// An Entity that has a list of Entities it iterates over each loop
    /// </summary>
    public class EntityBag
    {
        /// <summary>The entities owned by the screen</summary>
        protected HashSet<GameEntity> _entities;

        /// <summary>The entities to be removed after updating all entities</summary>
        protected HashSet<GameEntity> _entitiesToRemove;

        /// <summary>Entities to add after updating</summary>
        protected HashSet<GameEntity> _entitiesToAdd;

        /// <summary>
        /// Creates the EntityBag
        /// </summary>
        public EntityBag()
        {
            _entities = new HashSet<GameEntity>();
            _entitiesToRemove = new HashSet<GameEntity>();
            _entitiesToAdd = new HashSet<GameEntity>();
        }

        /// <summary>
        /// Adds an entity to the Screens set
        /// </summary>
        /// <param name="entityIn">Entity to be added</param>
        /// <param name="screenIn">The screen the entity is attached to</param>
        public void addEntity(GameEntity entityIn, GameScreen screenIn)
        {
            entityIn.Screen = screenIn;
            entityIn.Load();
            _entitiesToAdd.Add(entityIn);
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
            if (_entitiesToRemove.Count > 0)
            {
                foreach (GameEntity ge in _entitiesToRemove)
                {
                    _entities.Remove(ge);
                }
            }
            _entitiesToRemove.Clear();
        }

        /// <summary>
        /// Adds all entities set to be added
        /// </summary>
        protected void addEntities()
        {
            if (_entitiesToAdd.Count > 0)
            {
                foreach (GameEntity ge in _entitiesToAdd)
                {
                    _entities.Add(ge);
                }
            }
            _entitiesToAdd.Clear();
        }

        /// <summary>
        /// Updates all entities in the entity bag
        /// </summary>
        /// <param name="gameTimeIn"></param>
        public void Update(GameTime gameTimeIn)
        {
            foreach (GameEntity ge in _entities)
            {
                if (ge.Loaded)
                    ge.Update(gameTimeIn);
            }

            addEntities();
            removeEntities();
        }

        /// <summary>
        /// Draws all entities in the bag
        /// </summary>
        /// <param name="gameTimeIn"></param>
        /// <param name="sBatch"></param>
        public void Draw(GameTime gameTimeIn, SpriteBatch sBatch)
        {
            foreach (GameEntity ge in _entities)
            {
                if (ge.Loaded)
                    ge.Draw(gameTimeIn, sBatch);
            }
        }
    }
}
