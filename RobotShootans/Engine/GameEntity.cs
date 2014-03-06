using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Engine
{
    /// <summary>
    /// A Game Entity used in GameScreens
    /// </summary>
    public class GameEntity
    {
        #region variables
        /// <summary>Whether the Entity has loaded or not</summary>
        protected bool _loaded;
        /// <summary>Whether the Entity has loaded or not</summary>
        public bool Loaded { get { return _loaded; } }

        string _entityName;
        /// <summary>The entity name</summary>
        public string EntityName { get { return _entityName; } }

        /// <summary>The screen that owns the Entity</summary>
        public GameScreen Screen;

        private Guid _id;
        /// <summary>The ID of the entity</summary>
        public Guid ID { get { return _id; } }
        
        /// <summary>
        /// Creates the Entity
        /// </summary>
        /// <param name="entityName">The name of the entity</param>
        public GameEntity(string entityName)
        {
            _id = Guid.NewGuid();

            _entityName = entityName;
        }
        #endregion

        #region Virtual functions
        /// <summary>
        /// Load all content for the Entity
        /// </summary>
        public virtual void Load()
        {

        }

        /// <summary>
        /// Unload any non-managed content
        /// </summary>
        public virtual void Unload()
        {

        }

        /// <summary>
        /// Update the entity
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {

        }

        /// <summary>
        /// Draw the Entity
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="sBatch"></param>
        public virtual void Draw(GameTime gameTime, SpriteBatch sBatch)
        {

        }
        #endregion
    }
}
