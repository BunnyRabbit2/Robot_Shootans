using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RobotShootans.Engine;

namespace RobotShootans.Entities
{
    /// <summary>
    /// Loads and displays an image tiled into the chosen Rectangle
    /// </summary>
    public class TiledBackground : GameEntity
    {
        private string _bgLocation;
        private Texture2D _bg;
        private Rectangle _rect;

        /// <summary>
        /// Creates the TiledBackground
        /// </summary>
        /// <param name="location">The location of the background image</param>
        /// <param name="position">The position to display the background</param>
        /// <param name="size">The size of the background</param>
        public TiledBackground(string location, Vector2 position, Vector2 size)
            : base(location)
        {
            _bgLocation = location;
            _rect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        /// <summary>
        /// Creates the TiledBackground
        /// </summary>
        /// <param name="location">The location of the background image</param>
        /// <param name="rect">The destination rectangle of the background</param>
        public TiledBackground(string location, Rectangle rect)
            : base(location)
        {
            _bgLocation = location;
            _rect = rect;
        }

        /// <summary>
        /// Creates the TiledBackground
        /// </summary>
        /// <param name="location">The location of the background image</param>
        /// <param name="X">The X position of the background</param>
        /// <param name="Y">The Y position of the background</param>
        /// <param name="width">The width of the background</param>
        /// <param name="height">The height of the background</param>
        public TiledBackground(string location, int X, int Y, int width, int height)
            : base(location)
        {
            _bgLocation = location;
            _rect = new Rectangle(X, Y, width, height);
        }

        /// <summary>
        /// Loads the background in
        /// </summary>
        public override void Load()
        {
            _bg = Screen.Engine.Content.Load<Texture2D>(_bgLocation);

            _loaded = true;
        }

        /// <summary>
        /// Draws the background
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="sBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            sBatch.Draw(_bg, new Vector2(_rect.X, _rect.Y), _rect, Color.White);
        }
    }
}
