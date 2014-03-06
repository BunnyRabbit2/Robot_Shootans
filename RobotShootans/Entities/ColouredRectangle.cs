using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotShootans.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RobotShootans.Entities
{
    /// <summary>
    /// An entity that will draw a rectangle of the chosen colour at the chosen position and size
    /// </summary>
    public class ColouredRectangle : GameEntity
    {
        #region variables
        private Texture2D _texture;
        private Rectangle _rectangle;
        private Color _color;

        /// <summary>
        /// The X position of the Rectangle
        /// </summary>
        public int X { get { return _rectangle.X; } set { _rectangle.X = value; } }
        /// <summary>
        /// The Y position of the Rectangle
        /// </summary>
        public int Y { get { return _rectangle.Y; } set { _rectangle.Y = value; } }
        /// <summary>
        /// The position of the Rectangle
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return new Vector2(_rectangle.X, _rectangle.Y);
            }
            set
            {
                _rectangle.X = (int)value.X;
                _rectangle.Y = (int)value.Y;
            }
        }

        /// <summary>
        /// The width of the Rectangle
        /// </summary>
        public int Width { get { return _rectangle.Width; } set { _rectangle.Width = value; } }
        /// <summary>
        /// The Height of the Retangle
        /// </summary>
        public int Height { get { return _rectangle.Height; } set { _rectangle.Height = value; } }
        /// <summary>
        /// The size of the Rectangle
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return new Vector2(_rectangle.Width, _rectangle.Height);
            }
            set
            {
                _rectangle.Width = (int)value.X;
                _rectangle.Height = (int)value.Y;
            }
        }
        #endregion

        /// <summary>
        /// Creates the Rectangle and sets the colour
        /// </summary>
        /// <param name="size">The position and size wanted</param>
        /// <param name="colour">The colour wanted</param>
        public ColouredRectangle(Rectangle size, Color colour)
            : base ("Rect")
        {
            _rectangle = size;
            _color = colour;
        }

        /// <summary>
        /// Creates the texture 2D of the rectangle
        /// </summary>
        public override void Load()
        {
            _texture = new Texture2D(Screen.Engine.Graphics, 1, 1);
            _texture.SetData(new[] { Color.White });

            _loaded = true;
        }

        /// <summary>
        /// Draws the rectangle
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="sBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            sBatch.Draw(_texture, _rectangle, _color);
        }
    }
}
