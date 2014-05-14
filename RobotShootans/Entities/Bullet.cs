using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotShootans.Engine;

namespace RobotShootans.Entities
{
    /// <summary>
    /// Bullet class. A form of coloured rectangle with a physics body
    /// </summary>
    public class Bullet : ColouredRectangle
    {
        private float _speed;
        private Vector2 _vector;
        private Vector2 _position;

        /// <summary>
        /// Creates the bullet and sets the vector
        /// </summary>
        public Bullet(Vector2 positionIn, int sizeIn, float speedIn, float bearingToTravel)
            : base(new Rectangle((int)positionIn.X, (int)positionIn.Y, sizeIn, sizeIn), Color.Yellow)
        {
            _position = positionIn;
            _speed = speedIn;
            _vector = HelperFunctions.GetVectorFromBearingAndSpeed(bearingToTravel, speedIn);
        }

        /// <summary>
        /// Creates the bullet and sets the vector
        /// </summary>
        public Bullet(int xIn, int yIn, int sizeIn, float speedIn, float bearingToTravel)
            : base(new Rectangle(xIn, yIn, sizeIn, sizeIn), Color.Yellow)
        {
            _position = new Vector2(xIn, yIn);
            _speed = speedIn;
            _vector = HelperFunctions.GetVectorFromBearingAndSpeed(bearingToTravel, speedIn);
        }

        /// <summary>
        /// Creates the bullet and sets the vector
        /// </summary>
        public Bullet(Vector2 positionIn, int sizeIn, float speedIn, float bearingToTravel, Color colorIn)
            : base(new Rectangle((int)positionIn.X, (int)positionIn.Y, sizeIn, sizeIn), colorIn)
        {
            _position = positionIn;
            _speed = speedIn;
            _vector = HelperFunctions.GetVectorFromBearingAndSpeed(bearingToTravel, speedIn);
        }

        /// <summary>
        /// Creates the bullet and sets the vector
        /// </summary>
        public Bullet(int xIn, int yIn, int sizeIn, float speedIn, float bearingToTravel, Color colorIn)
            : base(new Rectangle(xIn, yIn, sizeIn, sizeIn), colorIn)
        {
            _position = new Vector2(xIn, yIn);
            _speed = speedIn;
            _vector = HelperFunctions.GetVectorFromBearingAndSpeed(bearingToTravel, speedIn);
        }

        /// <summary>
        /// Loads the bullet and sets a tag so they are easier to find
        /// </summary>
        public override void Load()
        {
            base.Load();

            // TODO: Add a physics body to this when I get that done
            addTag("BULLET");

            _loaded = true;
        }

        /// <summary>
        /// Adds the vector to the position.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _position.X += _vector.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _position.Y += _vector.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

            X = (int)_position.X;
            Y = (int)_position.Y;

            if(X > Screen.Engine.RenderWidth * 1.1 || X < -10
                || Y < -10 || Y > Screen.Engine.RenderHeight * 1.1)
            {
                Screen.removeEntity(this);
            }
        }
    }
}
