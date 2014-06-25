using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RobotShootans.Engine;

namespace RobotShootans.Entities
{
    /// <summary>
    /// A splash logo that will fade in, display for some time and then fade out.
    /// </summary>
    public class SplashLogo : GameEntity
    {
        string _logoLocation;
        Texture2D _image;
        Color _colour;

        private int _fadeTimer;
        private float _fadeAlpha;
        private int _fadeTime;
        private int _displayTime;

        private Vector2 _position;
        /// <summary>
        /// The position of the Logo
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
        }

        private Vector2 _origin;

        private bool _firstUpdate;

        /// <summary>
        /// Creates the logo
        /// </summary>
        /// <param name="logoLocation">Location of the Splash image</param>
        /// <param name="logoPosition">Position of the Splash logo</param>
        /// <param name="logoColour">Colour of the Splash Logo</param>
        public SplashLogo(string logoLocation, Vector2 logoPosition, Color logoColour)
            : base(logoLocation)
        {
            _logoLocation = logoLocation;
            _position = logoPosition;
            _colour = logoColour;
            _fadeTime = 1000;
            _displayTime = 5000;
        }

        /// <summary>
        /// Creates the logo
        /// </summary>
        /// <param name="logoLocation">Location of the Splash image</param>
        /// <param name="logoPosition">Position of the Splash logo</param>
        public SplashLogo(string logoLocation, Vector2 logoPosition)
            : base(logoLocation)
        {
            _logoLocation = logoLocation;
            _position = logoPosition;
            _colour = Color.White;
            _fadeTime = 1000;
            _displayTime = 5000;
        }

        /// <summary>
        /// Creates the logo
        /// </summary>
        /// <param name="logoLocation">Location of the Splash image</param>
        /// <param name="logoPosition">Position of the Splash logo</param>
        /// <param name="displayTime">How long to display the Splash logo in milliseconds</param>
        /// <param name="fadeTime">How long the fade in and out takes in milliseconds</param>
        public SplashLogo(string logoLocation, Vector2 logoPosition, int displayTime, int fadeTime)
            : base(logoLocation)
        {
            _logoLocation = logoLocation;
            _position = logoPosition;
            _colour = Color.White;
            _fadeTime = fadeTime;
            _displayTime = displayTime;
        }

        /// <summary>
        /// Loads the logo content and sets thew origin
        /// </summary>
        public override void Load()
        {
            _image = Screen.Engine.Content.Load<Texture2D>(_logoLocation);
            _origin = new Vector2(_image.Width / 2, _image.Height / 2);

            _firstUpdate = true;

            _loaded = true;
        }

        /// <summary>
        /// Sets up the fading in and out
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (_firstUpdate)
            {
                _fadeTimer = 0;
                _firstUpdate = false;
            }

            _fadeTimer += gameTime.ElapsedGameTime.Milliseconds;

            if(_fadeTimer < _fadeTime)
            {
                _fadeAlpha = _fadeTimer / 1000f;
            }
            else if(_fadeTimer > _fadeTime && _fadeTimer < (_displayTime - _fadeTime) )
            {
                _fadeAlpha = 1.0f;
            }
            else if (_fadeTimer > (_displayTime - _fadeTime))
            {
                _fadeAlpha = (_displayTime - _fadeTimer) / 1000f;
            }
        }

        /// <summary>
        /// Draws the logo
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="sbatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch sbatch)
        {
            sbatch.Draw(_image, position: _position, origin: _origin, color: _colour * _fadeAlpha);
        }
    }
}
