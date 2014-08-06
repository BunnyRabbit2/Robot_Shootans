using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RobotShootans.Engine;

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
        /// The colour of the rectangle
        /// </summary>
        public Color Colour { get { return _color; } set { _color = value; } }

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

        /// <summary>The origin of the image</summary>
        protected Vector2 _origin;

        /// <summary>
        /// Sets the origin to draw the image from
        /// </summary>
        public void setOrigin(OriginPosition originIn)
        {
            switch (originIn)
            {
                case OriginPosition.TOPLEFT:
                    _origin = new Vector2(0f, 0f);
                    break;
                case OriginPosition.TOPRIGHT:
                    _origin = new Vector2(_rectangle.Width, 0f);
                    break;
                case OriginPosition.BOTTOMLEFT:
                    _origin = new Vector2(0f, _rectangle.Height);
                    break;
                case OriginPosition.BOTTOMRIGHT:
                    _origin = new Vector2(_rectangle.Width, _rectangle.Height);
                    break;
                case OriginPosition.CENTER:
                    _origin = new Vector2(_rectangle.Width / 2f, _rectangle.Height / 2f);
                    break;
            }
        }

        /// <summary>The rotation of the image</summary>
        protected float _rotation;

        /// <summary>Sets the rotation of the image</summary>
        /// <param name="rotationIn"></param>
        public void setRotation(float rotationIn)
        {
            _rotation = rotationIn;
        }

        /// <summary>
        /// The alpha of the rectangle
        /// </summary>
        public int Alpha {
            get { return _color.A; }
            set
            {
                _color.A = (byte)MathHelper.Clamp(value, 0, 255);
            }
        }

        #endregion

        /// <summary>
        /// Creates the Rectangle and sets the colour
        /// </summary>
        /// <param name="size">The position and size wanted</param>
        /// <param name="colour">The colour wanted</param>
        /// <param name="originIn">The origin of the rectangle to use. Defaults to top left</param>
        public ColouredRectangle(Rectangle size, Color colour, OriginPosition originIn = OriginPosition.TOPLEFT)
            : base ("Rect")
        {
            _rectangle = size;
            _color = colour;
            setOrigin(originIn);
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
            Rectangle newRect = _rectangle;
            newRect.X -= (int)_origin.X;
            newRect.Y -= (int)_origin.Y;

            sBatch.Draw(_texture, new Vector2(_rectangle.X, _rectangle.Y), newRect, _color, _rotation, _origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
