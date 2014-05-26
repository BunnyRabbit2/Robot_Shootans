using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RobotShootans.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        /// <summary>The scale of the text</summary>
        protected Vector2 _scale;
        /// <summary>Text Scale</summary>
        public Vector2 Scale { get { return _scale; } set { _scale = value; } }
        /// <summary>The X scale of the text</summary>
        public float scaleX { get { return _scale.X; } set { _scale.X = value; } }
        /// <summary>The Y scale of the text</summary>
        public float scaleY { get { return _scale.Y; } set { _scale.Y = value; } }


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

        /// <summary>Constructor for the TextItem</summary>
        public GUI_TextItem()
            : base ("GUI_TextItem")
        {
            _textColour = Color.White;
            _origin = Vector2.Zero;
            _scale = Vector2.One;
            _position = Vector2.Zero;
            _rotation = 0f;
        }

        /// <summary>Sets the text in the </summary>
        /// <param name="textIn"></param>
        public void setText(string textIn)
        {
            _displayText = textIn;
            // create new origin
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
            sBatch.DrawString(_textFont, _displayText, _position, _textColour, _rotation, _origin, _scale, SpriteEffects.None, 0f);
        }
    }
}
