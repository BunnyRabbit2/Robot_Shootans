using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RobotShootans.Engine;

// Outline drawing code was taken from this excellent blog
// http://erikskoglund.wordpress.com/2009/09/10/super-simple-text-outlining-in-xna/

namespace RobotShootans.Entities
{
    /// <summary>A GUI item that will display text</summary>
    public class GUI_TextItem : GameEntity
    {
        /// <summary>The text being displayed</summary>
        protected string _displayText;

        /// <summary>The font to use with the text</summary>
        protected SpriteFont _textFont;

        /// <summary>The position of the text</summary>
        protected Vector2 _position;
        /// <summary>The position of the text</summary>
        public Vector2 Position { get { return _position; } set { _position = value; } }
        /// <summary>The X position of the text</summary>
        public float X { get { return _position.X; } set { _position.X = value; } }
        /// <summary>The Y position of the text</summary>
        public float Y { get { return _position.Y; } set { _position.Y = value; } }

        /// <summary>The origin of the text</summary>
        protected Vector2 _origin;
        /// <summary>The position of the origin of the text</summary>
        protected OriginPosition _originPosition;

        /// <summary>
        /// Sets the origin to draw the image from
        /// </summary>
        public void setOrigin(OriginPosition originIn)
        {
            _originPosition = originIn;
        }

        /// <summary>The scale of the text</summary>
        protected Vector2 _scale;
        /// <summary>Text Scale</summary>
        public Vector2 Scale { get { return _scale; } set { _scale = value; } }
        /// <summary>The X scale of the text</summary>
        public float scaleX { get { return _scale.X; } set { _scale.X = value; } }
        /// <summary>The Y scale of the text</summary>
        public float scaleY { get { return _scale.Y; } set { _scale.Y = value; } }

        Vector2 _textSize;
        /// <summary>
        /// The size of the text
        /// </summary>
        public Vector2 TextSize { get { return _textSize * _scale; } }

        /// <summary>The rotation of the text</summary>
        protected float _rotation;

        /// <summary>Sets the rotation of the text</summary>
        /// <param name="rotationIn"></param>
        public void setRotation(float rotationIn)
        {
            _rotation = rotationIn;
        }

        private Color _textColour;
        /// <summary>Sets the color of the text</summary>
        /// <param name="colorIn"></param>
        public void setColor(Color colorIn)
        {
            _textColour = colorIn;
        }

        /// <summary>Sets whether the outline will be drawn</summary>
        public bool DrawOutline;

        private Color _outlineColour;
        /// <summary>Sets the color of the text</summary>
        /// <param name="colorIn"></param>
        public void setOutlineColor(Color colorIn)
        {
            _outlineColour = colorIn;
        }

        /// <summary>Constructor for the TextItem</summary>
        public GUI_TextItem()
            : base ("GUI_TextItem")
        {
            _displayText = "";
            _textColour = Color.White;
            _origin = Vector2.Zero;
            _scale = Vector2.One;
            _position = Vector2.Zero;
            _rotation = 0f;
            DrawOutline = false;
        }

        /// <summary>Sets the text in the </summary>
        /// <param name="textIn"></param>
        public void setText(string textIn)
        {
            // Stops errors later on
            if (textIn == null)
                textIn = "";

            _displayText = textIn;

            _textSize = _textFont.MeasureString(textIn);

            switch (_originPosition)
            {
                case OriginPosition.TOPLEFT:
                    _origin = new Vector2(0f, 0f);
                    break;
                case OriginPosition.TOPRIGHT:
                    _origin = new Vector2(_textSize.X, 0f);
                    break;
                case OriginPosition.BOTTOMLEFT:
                    _origin = new Vector2(0f, _textSize.Y);
                    break;
                case OriginPosition.BOTTOMRIGHT:
                    _origin = new Vector2(_textSize.X, _textSize.Y);
                    break;
                case OriginPosition.CENTER:
                    _origin = new Vector2(_textSize.X / 2f, _textSize.Y / 2f);
                    break;
            }
        }

        /// <summary>Sets the font of the text item</summary>
        /// <param name="fontIn"></param>
        public void setFont(SpriteFont fontIn)
        {
            if (fontIn != null)
            {
                _textFont = fontIn;
                _loaded = true;
            }
        }

        /// <summary>Draws the text to the screen</summary>
        /// <param name="gameTime"></param>
        /// <param name="sBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            if (DrawOutline)
                DrawTextWithOutline(sBatch, _displayText, _outlineColour, _textColour, (_scale.X + _scale.Y)/2f, _rotation, _position);
            else
                sBatch.DrawString(_textFont, _displayText, _position, _textColour, _rotation, _origin, _scale, SpriteEffects.None, 0f);
        }

        private void DrawTextWithOutline(SpriteBatch sb, string text, Color backColor, Color frontColor, float scale, float rotation, Vector2 position)
        {
            //These 4 draws are the background of the text and each of them have a certain displacement each way.
            sb.DrawString(_textFont, text, position + new Vector2(1 * scale, 1 * scale),
                backColor, rotation, _origin, scale, SpriteEffects.None, 1f);
            sb.DrawString(_textFont, text, position + new Vector2(-1 * scale, -1 * scale),
                backColor, rotation, _origin, scale, SpriteEffects.None, 1f);
            sb.DrawString(_textFont, text, position + new Vector2(-1 * scale, 1 * scale),
                backColor, rotation, _origin, scale, SpriteEffects.None, 1f);
            sb.DrawString(_textFont, text, position + new Vector2(1 * scale, -1 * scale),
                backColor, rotation, _origin, scale, SpriteEffects.None, 1f);
            sb.DrawString(_textFont, text, position,
                frontColor, rotation, _origin, scale, SpriteEffects.None, 1f); 
        }
    }
}
