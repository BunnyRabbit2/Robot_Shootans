using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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

        bool _isDead = false;

        int _robotSize;
        int _health = 1;
        int _robotType;

        float _maxVelocity, _maxAngleVelocity, _moveImpulse, _angleMoveImpulse;

        SoundEffect _robotKilled;

        /// <summary>Creates the robot and sets the start position</summary>
        /// <param name="positionIn"></param>
        /// <param name="spawnerIn"></param>
        /// <param name="sizeIn"></param>
        /// /// <param name="robotType">0 = normal, 1 = BIGnSLOW, 2 = fast, 3 = explodey</param>
        public Robot(Vector2 positionIn, RobotSpawner spawnerIn, int sizeIn, int robotType = 0) : base ("ROBOT")
        {
            _spawner = spawnerIn;
            _robotSize = sizeIn;
            _robotType = robotType;
            
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
            _displayRect.DrawOrder = 1;
            Screen.addEntity(_displayRect);

            _maxVelocity = 3f; // Max velocity at which the robot can move
            _maxAngleVelocity = (float)Math.Sqrt(((double)_maxVelocity * (double)_maxVelocity) / 2.0);

            _moveImpulse = 10f;
            _angleMoveImpulse = (float)Math.Sqrt(((double)_moveImpulse * (double)_moveImpulse) / 2.0);

            _robotKilled = Screen.Engine.loadSound("Robot_killed");

            if(_robotType == 1)
            {
                _displayRect.Colour = Color.Green;
                _maxVelocity = 1.5f;
                _maxAngleVelocity = (float)Math.Sqrt(((double)_maxVelocity * (double)_maxVelocity) / 2.0);
                _health = 3;
            }
            else if (_robotType == 2)
            {
                _displayRect.Colour = Color.Blue;
                _maxVelocity = 4.5f;
                _maxAngleVelocity = (float)Math.Sqrt(((double)_maxVelocity * (double)_maxVelocity) / 2.0);
            }
            else if (_robotType == 3)
            {
                _displayRect.Colour = Color.Yellow;
                _maxVelocity = 3f;
                _maxAngleVelocity = (float)Math.Sqrt(((double)_maxVelocity * (double)_maxVelocity) / 2.0);
            }

            _loaded = true;
        }

        private bool onCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            if (!_isDead)
            {
                // If the thing that collides with the player is a robot, GAME OVER MAN, GAME OVER!
                if (fixtureB.Body.UserData.ToString() == "BULLET" || fixtureB.Body.UserData.ToString() == "ROCKET")
                {
                    _health--;

                    if (_health < 1)
                    {
                        kill();

                        int _scored = 0;
                        switch(_robotType)
                        {
                            case 0:
                                _scored = 10;
                                break;
                            case 1:
                                _scored = 25;
                                break;
                            case 2:
                                _scored = 25;
                                break;
                            case 3:
                                _scored = 15;
                                break;
                        }
                        Screen.Engine.registerEvent(new GameEvent(EventType.SCORE_CHANGED, _scored));
                    }

                    if (fixtureB.Body.UserData.ToString() == "ROCKET")
                    {
                        Rocket r = (Rocket)Screen.getEntityWithBodyID(fixtureB.Body.BodyId);
                        r.explode();
                    }

                    Screen.removeEntity(Screen.getEntityWithBodyID(fixtureB.Body.BodyId));
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Kill the robot
        /// </summary>
        public void kill()
        {
            if (_robotType == 3)
            {
                Screen.addEntity(new Explosion(ConvertUnits.ToDisplayUnits(_physicsBody.Position), 1, 2.0f));
                var rbts = Screen.getEntityByName("ROBOT");
                for (int i = 0; i < rbts.Count; i++)
                {
                    Robot r = (Robot)rbts[i];

                    Vector2 rPos = ConvertUnits.ToDisplayUnits(r.SimPos);

                    //if (HelperFunctions.GetDistanceBetweenTwoPoints(ConvertUnits.ToDisplayUnits(_physicsBody.Position), rPos) < 46)
                    //    r.kill();
                    // find which robots are in range of the explosion and kill them
                    // also kill player if they are in range
                }
                var player = (Player)Screen.getEntityByName("Player")[0];
                if (HelperFunctions.GetDistanceBetweenTwoPoints(ConvertUnits.ToDisplayUnits(_physicsBody.Position), ConvertUnits.ToDisplayUnits(player.SimPos)) < 46)
                    player.damage();
            }
            else
            {
                Screen.addEntity(new Explosion(ConvertUnits.ToDisplayUnits(_physicsBody.Position), 3));
            }
            Screen.removeEntity(this);
            _isDead = true;
            _robotKilled.Play();
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
