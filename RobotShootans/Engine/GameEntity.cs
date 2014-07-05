using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

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

        private HashSet<string> _tags;
        
        /// <summary>
        /// Creates the Entity
        /// </summary>
        /// <param name="entityName">The name of the entity</param>
        public GameEntity(string entityName)
        {
            _id = Guid.NewGuid();

            _tags = new HashSet<string>();

            DrawOrder = 0;

            _entityName = entityName;
        }

        /// <summary>Draw Order for the entity. Defaults to 0</summary>
        public int DrawOrder;
        #endregion

        #region Tag Management
        /// <summary>
        /// Adds a tag to the entity
        /// </summary>
        /// <param name="tagToAdd">The tag to add to the entity</param>
        public void addTag(string tagToAdd)
        {
            if (_tags.Contains(tagToAdd))
                LogFile.LogStringLine("Entity " + _entityName + " (ID:" + _id + ") already contains tag: " + tagToAdd, LogType.INFO);
            else
                _tags.Add(tagToAdd);
        }

        /// <summary>
        /// Checks if the entity contains the tag
        /// </summary>
        /// <param name="tagToCheck">The tag to check</param>
        /// <returns></returns>
        public bool containsTag(string tagToCheck)
        {
            return _tags.Contains(tagToCheck);
        }

        /// <summary>
        /// Removes a tag from the entity
        /// </summary>
        /// <param name="tagToRemove">The tag to remove</param>
        public void removeTag(string tagToRemove)
        {
            _tags.Remove(tagToRemove);
        }

        /// <summary>
        /// Clears all tags from the entity
        /// </summary>
        public void clearTags()
        {
            _tags.Clear();
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
