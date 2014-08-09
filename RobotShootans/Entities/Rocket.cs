using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using RobotShootans.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Entities
{
    /// <summary>
    /// A subclass of bullet that is a rocket. It explodes
    /// </summary>
    public class Rocket : Bullet
    {
        Sprite _displaySprite;
        SoundEffect _rocketExplode;

        /// <summary>
        /// Creates the rocket
        /// </summary>
        /// <param name="positionIn"></param>
        /// <param name="bearingToTravel"></param>
        public Rocket(Vector2 positionIn, float bearingToTravel)
            : base (positionIn, 20, 100f, bearingToTravel)
        {
            _displaySprite = new Sprite();
        }

        /// <summary>
        /// Loads the rocket and sets it all up
        /// </summary>
        public override void Load()
        {
            _physicsBody = BodyFactory.CreateRectangle(Screen.PhysicsWorld, ConvertUnits.ToSimUnits(_size), ConvertUnits.ToSimUnits(_size), 10f, "ROCKET");
            _physicsBody.Position = ConvertUnits.ToSimUnits(_position);
            _physicsBody.Restitution = 0.0f;
            _physicsBody.Friction = 0.2f;
            _physicsBody.IsStatic = false;
            _physicsBody.Rotation = _travelDirection;

            _vector = HelperFunctions.GetVectorFromBearingAndSpeed(_travelDirection, _speed * 3);

            _vector = ConvertUnits.ToSimUnits(_vector);
            _physicsBody.ApplyLinearImpulse(_vector, _physicsBody.Position);

            // Sets it so the rocket doesn't collide with player
            _physicsBody.IgnoreCollisionWith(Screen.getBodiesByUserData("PLAYER")[0]);

            _angleSpeed = (float)Math.Sqrt(((double)_speed * (double)_speed) / 2.0);

            Screen.addEntity(_displaySprite);
            _displaySprite.DrawOrder = 4;
            _displaySprite.setImage("game/rocket", OriginPosition.CENTER);
            _displaySprite.Position = _position;
            _displaySprite.setRotation(_travelDirection);

            _rocketExplode = Screen.Engine.loadSound("rocket_explode");

            _loaded = true;
        }

        /// <summary>
        /// Explodes the rocket
        /// </summary>
        public void explode()
        {
            Explosion exp = new Explosion(ConvertUnits.ToDisplayUnits(_physicsBody.Position), 2, 3.0f);
            Screen.addEntity(exp);

            var rbts = Screen.getEntityByName("ROBOT");
            for (int i = 0; i < rbts.Count; i++ )
            {
                Robot r = (Robot)rbts[i];

                Vector2 rPos = ConvertUnits.ToDisplayUnits(r.SimPos);

                if (HelperFunctions.GetDistanceBetweenTwoPoints(_position, rPos) < exp.ExpSize)
                    r.kill();
                // find which robots are in range of the explosion and kill them
                // also kill player if they are in range
            }

            _rocketExplode.Play();
        }

        /// <summary>Disposes of the physics body</summary>
        public override void Unload()
        {
            _physicsBody.Dispose();
            Screen.removeEntity(_displaySprite);
        }

        /// <summary></summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _displaySprite.Position = ConvertUnits.ToDisplayUnits(_physicsBody.Position);
            
            _physicsBody.LinearVelocity = new Vector2(
                MathHelper.Clamp(_physicsBody.LinearVelocity.X, -_angleSpeed, _angleSpeed),
                MathHelper.Clamp(_physicsBody.LinearVelocity.Y, -_angleSpeed, _angleSpeed));

            if (_displaySprite.X < 0 || _displaySprite.X > Screen.Engine.RenderWidth ||
               _displaySprite.Y < 0 || _displaySprite.Y > Screen.Engine.RenderHeight)
            {
                Screen.removeEntity(this);
            }
        }
    }
}
