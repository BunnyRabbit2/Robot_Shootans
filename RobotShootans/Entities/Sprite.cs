using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotShootans.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;

namespace RobotShootans.Entities
{
    /// <summary>
    /// An enum to make setting the Origin easier
    /// </summary>
    public enum OriginPosition {
        /// <summary>Top Left of the Image</summary>
        TOPLEFT,
        /// <summary>Top Right of the Image</summary>
        TOPRIGHT,
        /// <summary>Bottom Left of the Image</summary>
        BOTTOMLEFT,
        /// <summary>Bottom Right of the Image</summary>
        BOTTOMRIGHT,
        /// <summary>Center of the Image</summary>
        CENTER
    }

    /// <summary>
    /// An displayable image with some retrievable information
    /// </summary>
    public class Sprite : GameEntity
    {
        Texture2D _image;
        Color _color;

        /// <summary>The position of the image</summary>
        protected Vector2 _position;
        /// <summary>The position of the image</summary>
        public Vector2 Position { get { return _position; } set { _position = value; } }
        /// <summary>The X position of the sprite</summary>
        public float X { get { return _position.X; } set { _position.X = value; } }
        /// <summary>The Y position of the sprite</summary>
        public float Y { get { return _position.Y; } set { _position.Y = value; } }

        /// <summary>The origin of the image</summary>
        protected Vector2 _origin;

        /// <summary>The scale of the image</summary>
        protected Vector2 _scale;

        /// <summary>The rotation of the image</summary>
        protected float _rotation;

        string _currentAnimation, _previousAnimation;
        /// <summary>The current animation being used</summary>
        public string Animation
        {
            get { return _currentAnimation; }
            set
            {
                if (_animations.ContainsKey(value))
                    _currentAnimation = value;
            }
        }
        Dictionary<string, Animation> _animations;

        /// <summary>Creates the Sprite object</summary>
        public Sprite()
            : base("SPRITE")
        {
            _color = Color.White;
            _origin = Vector2.Zero;
            _scale = Vector2.One;
            _animations = new Dictionary<string, Animation>();
        }

        /// <summary>Creates the Sprite object</summary>
        public Sprite(string entityName)
            : base(entityName)
        {
            _color = Color.White;
            _origin = Vector2.Zero;
            _scale = Vector2.One;
            _animations = new Dictionary<string, Animation>();
        }

        /// <summary>
        /// Sets the image of the Sprite
        /// </summary>
        /// <param name="imageLocation">The location of the image on the drive</param>
        public void setImage(string imageLocation)
        {
            _image = Screen.Engine.loadTexture(imageLocation);
            _loaded = true;
        }

        /// <summary>
        /// Sets the image of the sprite and it's origin
        /// </summary>
        /// <param name="imageLocation">The location of the image on the drive</param>
        /// <param name="originIn">The origin to use</param>
        public void setImage(string imageLocation, OriginPosition originIn)
        {
            _image = Screen.Engine.loadTexture(imageLocation);
            setOrigin(originIn);
            _loaded = true;
        }

        /// <summary>
        /// Sets the color of the sprite
        /// </summary>
        /// <param name="colorIn"></param>
        public void setColor(Color colorIn)
        {
            _color = colorIn;
        }

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
                    _origin = new Vector2(_image.Width, 0f);
                    break;
                case OriginPosition.BOTTOMLEFT:
                    _origin = new Vector2(0f, _image.Height);
                    break;
                case OriginPosition.BOTTOMRIGHT:
                    _origin = new Vector2(_image.Width, _image.Height);
                    break;
                case OriginPosition.CENTER:
                    _origin = new Vector2(_image.Width/2f, _image.Height / 2f);
                    break;
            }
        }

        /// <summary>
        /// Sets the origin to draw the image from
        /// </summary>
        public void setOrigin(Vector2 originIn)
        {
            _origin = originIn;
        }

        /// <summary>Sets the scale of the sprite</summary>
        /// <param name="scaleIn"></param>
        public void setScale(float scaleIn)
        {
            _scale = new Vector2(scaleIn,scaleIn);
        }

        /// <summary>Sets the X scale</summary>
        /// <param name="scaleXIn"></param>
        public void setScaleX(float scaleXIn)
        {
            _scale.X = scaleXIn;
        }

        /// <summary>Sets the Y scale</summary>
        /// <param name="scaleYIn"></param>
        public void setScaleY(float scaleYIn)
        {
            _scale.Y = scaleYIn;
        }

        /// <summary>Sets the rotation of the image</summary>
        /// <param name="rotationIn"></param>
        public void setRotation(float rotationIn)
        {
            _rotation = rotationIn;
        }

        /// <summary>
        /// Adds an animation to the sprite
        /// </summary>
        /// <param name="nameIn"></param>
        /// <param name="frameTimeIn"></param>
        /// <param name="framesIn"></param>
        public void addAnimation(string nameIn, int frameTimeIn, Rectangle[] framesIn)
        {
            _animations.Add(nameIn, new Animation(nameIn, frameTimeIn, framesIn));
            _currentAnimation = nameIn;
        }

        #region entity functions
        /// <summary>
        /// Loads to sprite texture and sets some private variables
        /// </summary>
        public override void Load()
        {
            // Nothing loaded here. Sprite will not display until the image is set
            _currentAnimation = "";
            _previousAnimation = "";
        }

        /// <summary>
        /// Updates the sprite
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if(_previousAnimation != _currentAnimation)
            {
                if(!string.IsNullOrEmpty(_previousAnimation))
                    _animations[_previousAnimation].stopAnimation();
                if (!_animations[_currentAnimation].Running)
                    _animations[_currentAnimation].startAnimation();
            }

            _previousAnimation = _currentAnimation;
        }

        /// <summary>
        /// Draws the sprite
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="sBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            if(_animations.Count != 0)
                sBatch.Draw(_image, position: _position, sourceRectangle: _animations[_currentAnimation].getCurrentframe(), rotation: _rotation, color: _color, origin: _origin, scale: _scale);
            else
                sBatch.Draw(_image, position: _position, color: _color, origin: _origin, scale: _scale);
        }
        #endregion
    }
}
