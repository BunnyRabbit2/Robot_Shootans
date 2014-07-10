using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RobotShootans.Engine;
using RobotShootans.Entities.Weapons;
using RobotShootans.Screens;
using System;

namespace RobotShootans.Entities
{
    /// <summary>
    /// The player class
    /// </summary>
    public class Player : PhysicsGameEntity
    {
        Sprite _playerSprite;

        float _maxVelocity, _maxAngleVelocity, _moveImpulse, _angleMoveImpulse;

        Weapon _currentWeapon;

#if DEBUG
        ColouredRectangle _debugRect;
#endif

        /// <summary>
        /// Creates the Player object
        /// </summary>
        /// <param name="startPos"></param>
        public Player(Vector2 startPos)
            : base("Player")
        {
            _playerSprite = new Sprite();
            _playerSprite.Position = startPos;
        }

        /// <summary>
        /// Loads the player sheet and sets animation frames
        /// </summary>
        public override void Load()
        {
            if(!Screen.PhysicsEnabled)
            {
                LogFile.LogStringLine("FAILED TO LOAD PLAYER. SCREEN OWNER IS NOT PHYSICS ENABLED", LogType.ERROR);
                return;
            }

            int frameWidth = 90;
            int frameHeight = 150;

            int collisionBoxSize = 45;

            Screen.addEntity(_playerSprite);

            _playerSprite.setImage("game/player-sheet");
            _playerSprite.addAnimation("IDLE", 1000, new Rectangle[] { new Rectangle(0, 0, frameWidth, frameHeight) });
            _playerSprite.addAnimation("WALK", 125,
                new Rectangle[] {
                    new Rectangle(0, 0, frameWidth, frameHeight),
                    new Rectangle(frameWidth, 0, frameWidth, frameHeight),
                    new Rectangle(0, frameHeight, frameWidth, frameHeight),
                    new Rectangle(frameWidth, frameHeight, frameWidth, frameHeight),
                    new Rectangle(0, frameHeight*2, frameWidth, frameHeight),
                    new Rectangle(frameWidth, frameHeight*2, frameWidth, frameHeight)
                });
            _playerSprite.addAnimation("IDLE_GUN", 1000, new Rectangle[] { new Rectangle(0, frameHeight * 3, frameWidth, frameHeight) });
            _playerSprite.addAnimation("SHOOT_GUN", 1000, new Rectangle[] { new Rectangle(frameWidth, frameHeight * 3, frameWidth, frameHeight) });
            _playerSprite.Animation = "IDLE_GUN";
            _playerSprite.setOrigin(new Vector2(frameWidth / 2, frameHeight / 2));
            _playerSprite.DrawOrder = 3;

#if DEBUG
            _debugRect = new ColouredRectangle(
                new Rectangle((int)_playerSprite.X, (int)_playerSprite.Y, collisionBoxSize, collisionBoxSize),
                new Color(Color.Red, 48), OriginPosition.CENTER);
            _debugRect.DrawOrder = 4;
            Screen.addEntity(_debugRect);
#endif

            _maxVelocity = 5f; // The maximum velocity the player can move at
            _maxAngleVelocity = (float)Math.Sqrt(((double)_maxVelocity * (double)_maxVelocity) / 2.0);

            _moveImpulse = 10f;
            _angleMoveImpulse = (float)Math.Sqrt(((double)_moveImpulse * (double)_moveImpulse) / 2.0);

            _physicsBody = BodyFactory.CreateRectangle(Screen.PhysicsWorld, ConvertUnits.ToSimUnits(collisionBoxSize), ConvertUnits.ToSimUnits(collisionBoxSize), 10f, "PLAYER");
            _physicsBody.Restitution = 0f;
            _physicsBody.Friction = 0.2f;
            _physicsBody.Position = ConvertUnits.ToSimUnits(_playerSprite.Position);
            _physicsBody.IsStatic = false;

            _physicsBody.OnCollision += onCollision;

            _loaded = true;
        }

        /// <summary></summary>
        public override void Unload()
        {
            _physicsBody.Dispose();
            Screen.removeEntity(_debugRect);
            Screen.removeEntity(_playerSprite);
            Screen.removeEntity(_currentWeapon);
        }

        private bool onCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
#if DEBUG
            LogFile.LogStringLine(fixtureA.Body.UserData.ToString() + " collides with " + fixtureB.Body.UserData.ToString());
#endif
            // If the thing that collides with the player is a robot, GAME OVER MAN, GAME OVER!
            if (fixtureB.Body.UserData.ToString() == "ROBOT")
            {
                Screen.Engine.removeGameScreen(Screen);
                GUI_HUD h = (GUI_HUD)Screen.getEntityByName("HUD")[0];
                Screen.Engine.pushGameScreen(new GameOverScreen(true, h.Score));
            }

            return true;
        }

        /// <summary>
        /// Handles an event
        /// </summary>
        /// <param name="eventIn"></param>
        /// <returns></returns>
        public override bool HandleEvent(GameEvent eventIn)
        {
            if(eventIn.EventType == EventType.WEAPON_CHANGED)
            {
                if (eventIn.UserData is Weapon)
                {
                    changeWeapon((Weapon)eventIn.UserData);
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// Updates player
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            float bearing = HelperFunctions.GetBearingBetweenTwoPoints(_playerSprite.Position, Screen.Engine.GetMousePosition(), false);
            _playerSprite.setRotation(bearing);
            _physicsBody.Rotation = bearing;

            int vertDir = 0;
            int horiDir = 0;
            Vector2 linearImpulse = Vector2.Zero;

            if (InputHelper.isKeyDown(Keys.W))
                vertDir--;
            if (InputHelper.isKeyDown(Keys.S))
                vertDir++;
            if (InputHelper.isKeyDown(Keys.D))
                horiDir++;
            if (InputHelper.isKeyDown(Keys.A))
                horiDir--;

            if (vertDir == 0)
            {
                if (horiDir == 1)
                    linearImpulse.X = _moveImpulse;
                else if (horiDir == -1)
                    linearImpulse.X = -_moveImpulse;
            }
            else
            {
                if (horiDir != 0)
                {
                    if (vertDir == 1)
                    {
                        if (horiDir == 1)
                            linearImpulse = new Vector2(_angleMoveImpulse, _angleMoveImpulse);
                        else if (horiDir == -1)
                            linearImpulse = new Vector2(-_angleMoveImpulse, _angleMoveImpulse);
                    }
                    else if (vertDir == -1)
                    {
                        if (horiDir == 1)
                            linearImpulse = new Vector2(_angleMoveImpulse, -_angleMoveImpulse);
                        else if (horiDir == -1)
                            linearImpulse = new Vector2(-_angleMoveImpulse, -_angleMoveImpulse);
                    }
                }
                else
                {
                    if (vertDir == 1)
                        linearImpulse.Y = _moveImpulse;
                    else if (vertDir == -1)
                        linearImpulse.Y = -_moveImpulse;
                }
            }

            if (linearImpulse != Vector2.Zero)
            {
                _physicsBody.ApplyLinearImpulse(linearImpulse, _physicsBody.Position);

                if (horiDir == 0)
                    _physicsBody.LinearVelocity = new Vector2(0f, MathHelper.Clamp(_physicsBody.LinearVelocity.Y, -_maxVelocity, _maxVelocity));
                else if (vertDir == 0)
                    _physicsBody.LinearVelocity = new Vector2(MathHelper.Clamp(_physicsBody.LinearVelocity.X, -_maxVelocity, _maxVelocity), 0f);
                else
                    _physicsBody.LinearVelocity = new Vector2(
                        MathHelper.Clamp(_physicsBody.LinearVelocity.X, -_maxAngleVelocity, _maxAngleVelocity),
                        MathHelper.Clamp(_physicsBody.LinearVelocity.Y, -_maxAngleVelocity, _maxAngleVelocity));
            }
            else
            {
                _physicsBody.LinearVelocity = Vector2.Zero;
            }

            // Binds the position to within 5% and 95% of the render screen size
            _physicsBody.Position = ConvertUnits.ToSimUnits(
                HelperFunctions.KeepVectorInBounds(ConvertUnits.ToDisplayUnits(_physicsBody.Position),
                (int)(Screen.Engine.RenderWidth * 0.05), (int)(Screen.Engine.RenderWidth * 0.95),
                (int)(Screen.Engine.RenderHeight * 0.05), (int)(Screen.Engine.RenderHeight * 0.95)));

            _playerSprite.Position = ConvertUnits.ToDisplayUnits(_physicsBody.Position);

#if DEBUG
            _debugRect.Position = ConvertUnits.ToDisplayUnits(_physicsBody.Position);
            _debugRect.setRotation(_physicsBody.Rotation);
#endif

            if (InputHelper.isKeyDown(Keys.Space))
            {
                if (_currentWeapon != null)
                {
                    _currentWeapon.shoot(_playerSprite.Position, bearing);
                    if (_currentWeapon.Ammo == 0)
                    {
                        changeWeapon(new Pistol());
                    }
                }
                else
                {
                    changeWeapon(new Pistol());
                }
            }

#if DEBUG
            _debugRect.X = (int)_playerSprite.Position.X;
            _debugRect.Y = (int)_playerSprite.Position.Y;
#endif
        }

        /// <summary>
        /// Draws the player to the screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="sBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
#if DEBUG
            _debugRect.Draw(gameTime, sBatch);
#endif
        }

        /// <summary>Gets position of the player</summary>
        /// <returns></returns>
        public Vector2 getPosition()
        {
            return _playerSprite.Position;
        }

        private void changeWeapon(Weapon weaponIn)
        {
            if(_currentWeapon != null)
                Screen.removeEntity(_currentWeapon);
            _currentWeapon = new Pistol();
            Screen.Engine.registerEvent(new GameEvent(EventType.SET_AMMO, _currentWeapon.Ammo));
            Screen.addEntity(_currentWeapon);
        }
    }
}
