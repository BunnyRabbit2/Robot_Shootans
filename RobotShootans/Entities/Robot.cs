using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using RobotShootans.Engine;
using System;

namespace RobotShootans.Entities
{
    /// <summary>Robot class. Might make abstract</summary>
    public class Robot : PhysicsGameEntity
    {
        ColouredRectangle _displayRect;
        RobotSpawner _spawner;

        Vector2 _startPos;

        int _robotSize;

        float _maxVelocity, _maxAngleVelocity, _moveImpulse, _angleMoveImpulse;

        /// <summary>Creates the robot and sets the start position</summary>
        /// <param name="positionIn"></param>
        /// <param name="spawnerIn"></param>
        /// <param name="sizeIn"></param>
        public Robot(Vector2 positionIn, RobotSpawner spawnerIn, int sizeIn) : base ("ROBOT")
        {
            _spawner = spawnerIn;
            _robotSize = sizeIn;

            _startPos = positionIn;
        }

        /// <summary></summary>
        public override void Load()
        {
            if (!Screen.PhysicsEnabled)
            {
                LogFile.LogStringLine("FAILED TO LOAD PLAYER. SCREEN OWNER IS NOT PHYSICS ENABLED", LogType.ERROR);
                return;
            }

            _physicsBody = BodyFactory.CreateRectangle(Screen.PhysicsWorld, ConvertUnits.ToSimUnits(_robotSize), ConvertUnits.ToSimUnits(_robotSize), 10f, "ROBOT");
            _physicsBody.Restitution = 0f;
            _physicsBody.Friction = 0.2f;
            _physicsBody.Position = ConvertUnits.ToSimUnits(_startPos);
            _physicsBody.IsStatic = false;
            _physicsBody.FixedRotation = true;

            _physicsBody.OnCollision += onCollision;

            _displayRect = new ColouredRectangle(new Rectangle((int)_startPos.X, (int)_startPos.Y, _robotSize, _robotSize), Color.Red);
            _displayRect.setOrigin(OriginPosition.CENTER);
            Screen.addEntity(_displayRect);

            _maxVelocity = 3f; // Max velocity at which the robot can move
            _maxAngleVelocity = (float)Math.Sqrt(((double)_maxVelocity * (double)_maxVelocity) / 2.0);

            _moveImpulse = 10f;
            _angleMoveImpulse = (float)Math.Sqrt(((double)_moveImpulse * (double)_moveImpulse) / 2.0);

            _loaded = true;
        }

        private bool onCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            // If the thing that collides with the player is a robot, GAME OVER MAN, GAME OVER!
            if (fixtureB.Body.UserData.ToString() == "BULLET")
            {
                Screen.removeEntity(this);
                //fixtureB.Body.BodyId
                Screen.removeEntity(Screen.getEntityWithBodyID(fixtureB.Body.BodyId));
            }

            return true;
        }

        /// <summary></summary>
        public override void Unload()
        {
            _physicsBody.Dispose();
            Screen.removeEntity(_displayRect);
        }

        /// <summary>Updates the robot and sends it towars the player</summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Vector2 pos1 = _displayRect.Position;
            Vector2 pos2 = _spawner.playerPosition;

            if (pos2 != null)
            {
                float _travelDirection = HelperFunctions.GetBearingBetweenTwoPoints(pos1, pos2, false);
                _physicsBody.Rotation = _travelDirection;
                _displayRect.setRotation(_travelDirection);

                _travelDirection = MathHelper.ToDegrees(_travelDirection);

                Vector2 impulse = Vector2.Zero;

                // double h = Math.Sqrt(_maxVelocity * _maxVelocity + _maxVelocity * _maxVelocity);
                double h = Math.Sqrt(_angleMoveImpulse * _angleMoveImpulse + _angleMoveImpulse * _angleMoveImpulse);

                double theta = 0;

                int hori = 0;
                int vert = 0;

                if (_travelDirection >= 0 || _travelDirection < 90)
                {
                    theta = 90 - _travelDirection;
                    hori = 1;
                    vert = -1;
                }
                else if (_travelDirection >= 90 || _travelDirection < 180)
                {
                    theta = _travelDirection - 90;
                    hori = 1;
                    vert = 1;
                }
                else if (_travelDirection >= 180 || _travelDirection < 270)
                {
                    theta = 270 - _travelDirection;
                    hori = -1;
                    vert = 1;
                }
                else if (_travelDirection >= 270 || _travelDirection < 360)
                {
                    theta = _travelDirection - 270;
                    hori = -1;
                    vert = -1;
                }

                theta = MathHelper.ToRadians((float)theta);

                float vertI = (float)(Math.Sin(theta) * h) * vert;
                float horiI = (float)(Math.Cos(theta) * h) * hori;

                _physicsBody.LinearVelocity = new Vector2(
                    MathHelper.Clamp(horiI, -_maxAngleVelocity, _maxAngleVelocity),
                    MathHelper.Clamp(vertI, -_maxAngleVelocity, _maxAngleVelocity)
                    );
            }

            _displayRect.Position = ConvertUnits.ToDisplayUnits(_physicsBody.Position);
        }
    }
}
