using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using RobotShootans.Engine;
using System;

namespace RobotShootans.Entities
{
    /// <summary>
    /// Bullet class. A form of coloured rectangle with a physics body
    /// </summary>
    public class Bullet : PhysicsGameEntity
    {
        /// <summary></summary>
        protected float _speed, _angleSpeed, _travelDirection;
        /// <summary></summary>
        protected int _size;
        /// <summary></summary>
        protected Vector2 _vector, _position;
        /// <summary></summary>
        protected Color _bulletColour;

        /// <summary></summary>
        protected ColouredRectangle _displayRect;

        /// <summary>
        /// Creates the bullet and sets the vector
        /// </summary>
        public Bullet(Vector2 positionIn, int sizeIn, float speedIn, float bearingToTravel)
            : base("BULLET")
        {
            _position = positionIn;
            _speed = speedIn;
            _size = sizeIn;
            _travelDirection = bearingToTravel;
            _bulletColour = Color.Yellow;
        }

        /// <summary>
        /// Creates the bullet and sets the vector
        /// </summary>
        public Bullet(int xIn, int yIn, int sizeIn, float speedIn, float bearingToTravel)
            : base("BULLET")
        {
            _position = new Vector2(xIn, yIn);
            _speed = speedIn;
            _size = sizeIn;
            _travelDirection = bearingToTravel;
            _bulletColour = Color.Yellow;
        }

        /// <summary>
        /// Creates the bullet and sets the vector
        /// </summary>
        public Bullet(Vector2 positionIn, int sizeIn, float speedIn, float bearingToTravel, Color colorIn)
            : base("BULLET")
        {
            _position = positionIn;
            _speed = speedIn;
            _size = sizeIn;
            _travelDirection = bearingToTravel;
            _bulletColour = colorIn;
        }

        /// <summary>
        /// Creates the bullet and sets the vector
        /// </summary>
        public Bullet(int xIn, int yIn, int sizeIn, float speedIn, float bearingToTravel, Color colorIn)
            : base("BULLET")
        {
            _position = new Vector2(xIn, yIn);
            _speed = speedIn;
            _size = sizeIn;
            _travelDirection = bearingToTravel;
            _bulletColour = colorIn;
        }

        /// <summary>
        /// Loads the bullet and sets a tag so they are easier to find
        /// </summary>
        public override void Load()
        {
            _physicsBody = BodyFactory.CreateRectangle(Screen.PhysicsWorld, ConvertUnits.ToSimUnits(_size), ConvertUnits.ToSimUnits(_size), 10f, "BULLET");
            _physicsBody.Position = ConvertUnits.ToSimUnits(_position);
            _physicsBody.Restitution = 0.0f;
            _physicsBody.Friction = 0.2f;
            _physicsBody.IsStatic = false;

            _vector = HelperFunctions.GetVectorFromBearingAndSpeed(_travelDirection, _speed * 3);

            _vector = ConvertUnits.ToSimUnits(_vector);
            _physicsBody.ApplyLinearImpulse(_vector, _physicsBody.Position);

            // Sets it so bullets don't collide with player
            _physicsBody.IgnoreCollisionWith(Screen.getBodiesByUserData("PLAYER")[0]);

            _angleSpeed = (float)Math.Sqrt(((double)_speed * (double)_speed) / 2.0);

            _displayRect = new ColouredRectangle(new Rectangle((int)_position.X, (int)_position.Y, _size, _size), _bulletColour, OriginPosition.CENTER);
            _displayRect.DrawOrder = 2;
            Screen.addEntity(_displayRect);

            _loaded = true;
        }

        /// <summary>Disposes of the physics body</summary>
        public override void Unload()
        {
            _physicsBody.Dispose();
            Screen.removeEntity(_displayRect);
        }

        /// <summary></summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _displayRect.Position = ConvertUnits.ToDisplayUnits(_physicsBody.Position);

            _physicsBody.LinearVelocity = new Vector2(
                MathHelper.Clamp(_physicsBody.LinearVelocity.X, -_angleSpeed, _angleSpeed),
                MathHelper.Clamp(_physicsBody.LinearVelocity.Y, -_angleSpeed, _angleSpeed));

            if(_displayRect.X < 0 || _displayRect.X > Screen.Engine.RenderWidth ||
               _displayRect.Y < 0 || _displayRect.Y > Screen.Engine.RenderHeight)
            {
                Screen.removeEntity(this);
            }
        }
    }
}
