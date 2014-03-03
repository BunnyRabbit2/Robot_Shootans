using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotShootans.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RobotShootans.Componenets
{
    public class SplashLogo : GameEntity
    {
        string _logoLocation;
        Texture2D _image;
        Color _colour;

        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
        }

        private Vector2 _origin;

        public SplashLogo(string logoLocation, Vector2 logoPosition, Color logoColour)
            : base("SPLASH_LOGO")
        {
            _logoLocation = logoLocation;
            _position = logoPosition;
            _colour = logoColour;
        }

        public SplashLogo(string logoLocation, Vector2 logoPosition)
            : base("SPLASH_LOGO")
        {
            _logoLocation = logoLocation;
            _position = logoPosition;
            _colour = Color.White;
        }

        public override void Load()
        {
            _image = Screen.Engine.Content.Load<Texture2D>(_logoLocation);
            _origin = new Vector2(_image.Width / 2, _image.Height / 2);

            _loaded = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch sbatch)
        {
            sbatch.Draw(_image, position: _position, origin: _origin, color: _colour);
        }
    }
}
