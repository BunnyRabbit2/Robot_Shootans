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
        protected bool _loaded;
        public bool Loaded { get { return _loaded; } }

        string _componentName;
        public string ComponentName { get { return _componentName; } }

        public GameScreen Screen;

        private Guid _id;
        public Guid ID { get { return _id; } }
        #endregion

        /// <summary>
        /// Creates the Entity
        /// </summary>
        /// <param name="componentName">The name of the component</param>
        public GameEntity(string componentName)
        {
            _id = Guid.NewGuid();

            _componentName = componentName;
        }

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
