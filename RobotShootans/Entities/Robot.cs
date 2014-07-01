using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using RobotShootans.Engine;
using System;

namespace RobotShootans.Entities
{
    /// <summary>Robot class. Might make abstract</summary>
    public class Robot : GameEntity
    {
        ColouredRectangle _displayRect;
        PhysicsBox _displayBox;
        RobotSpawner _spawner;
        float _speed;

        Vector2 _startPos;

        int _robotSize;

        float _maxVelocity, _maxAngleVelocity, _moveImpulse, _angleMoveImpulse;

        /// <summary>Creates the robot and sets the start position</summary>
        /// <param name="positionIn"></param>
        /// /// <param name="spawnerIn"></param>
        public Robot(Vector2 positionIn, RobotSpawner spawnerIn, int sizeIn) : base ("ROBOT")
        {
            _spawner = spawnerIn;
            _robotSize = sizeIn;

            _startPos = positionIn;

            _displayBox = new PhysicsBox();
            _displayRect = new ColouredRectangle(new Rectangle((int)positionIn.X, (int)positionIn.Y, _robotSize, _robotSize), Color.Red);
            _displayRect.setOrigin(OriginPosition.CENTER);
        }

        /// <summary></summary>
        public override void Load()
        {
            // Screen.addEntity(_displayRect);
            Screen.addEntity(_displayBox);
            _speed = Screen.Engine.RenderHeight / 4.0f;

            _displayBox.SetupBox(Screen.PhysicsWorld, _robotSize, _robotSize, false, _startPos, 0.0f, 0.0f, 0.2f, Color.Red);
            // _displayBox.Body.FixedRotation = true;

            _maxVelocity = 3f; // Max velocity at which the robot can move
            _maxAngleVelocity = (float)Math.Sqrt(((double)_maxVelocity * (double)_maxVelocity) / 2.0);

            _moveImpulse = 3f;
            _angleMoveImpulse = (float)Math.Sqrt(((double)_moveImpulse * (double)_moveImpulse) / 2.0);

            _loaded = true;
        }

        /// <summary>Updates the robot and sends it towars the player</summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Vector2 pos1 = _displayBox.Position;
            Vector2 pos2 = _spawner.playerPosition;

            if (pos2 != null)
            {
                float _travelDirection = HelperFunctions.GetBearingBetweenTwoPoints(pos1, pos2);
                _displayBox.setRotation(_travelDirection);

                Vector2 impulse = Vector2.Zero;

                double h = Math.Sqrt(_maxVelocity * _maxVelocity + _maxVelocity * _maxVelocity);
                // double h = Math.Sqrt(_angleMoveImpulse * _angleMoveImpulse + _angleMoveImpulse * _angleMoveImpulse);

                double theta = 0;

                int hori = 0;
                int vert = 0;

                if (_travelDirection >= 0 || _travelDirection < 90)
                {
                    theta = 90 - _travelDirection;
                    hori = 1;
                    vert = 1;
                }
                else if (_travelDirection >= 90 || _travelDirection < 180)
                {
                    theta = _travelDirection - 90;
                    hori = 1;
                    vert = -1;
                }
                else if (_travelDirection >= 180 || _travelDirection < 270)
                {
                    theta = 270 - _travelDirection;
                    hori = -1;
                    vert = -1;
                }
                else if (_travelDirection >= 270 || _travelDirection < 360)
                {
                    theta = _travelDirection - 270;
                    hori = -1;
                    vert = 1;
                }

                float vertI = (float)(Math.Sin(theta) * h) * vert;
                float horiI = (float)(Math.Cos(theta) * h) * hori;

                _displayBox.Body.LinearVelocity = new Vector2(horiI, vertI);
                //_displayBox.Body.ApplyLinearImpulse(new Vector2(horiI, vertI), _displayBox.SimPos);

                // soh cah

                // fucking trig always fucking with my shit

                //if(_displayBox.Body.LinearVelocity.X == 0)
                //{
                //    MathHelper.Clamp(_displayBox.Body.LinearVelocity.Y, -_maxVelocity, _maxVelocity);
                //}
                //else if(_displayBox.Body.LinearVelocity.Y == 0)
                //{
                //    MathHelper.Clamp(_displayBox.Body.LinearVelocity.X, -_maxVelocity, _maxVelocity);
                //}
                //else
                //{
                _displayBox.Body.LinearVelocity = new Vector2(
                    MathHelper.Clamp(_displayBox.Body.LinearVelocity.Y, -_maxAngleVelocity, _maxAngleVelocity),
                    MathHelper.Clamp(_displayBox.Body.LinearVelocity.X, -_maxAngleVelocity, _maxAngleVelocity)
                    );
                //}
            }
        }
    }
}
