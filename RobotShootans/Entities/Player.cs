using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotShootans.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace RobotShootans.Entities
{
    /// <summary>
    /// The player class
    /// </summary>
    public class Player : GameEntity
    {
        EntityBag _bag;
        Sprite _playerSprite;

        Vector2 _velocity;
        float _speed;

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
            _bag = new EntityBag();
        }

        /// <summary>
        /// Loads the player sheet and sets animation frames
        /// </summary>
        public override void Load()
        {
            int frameWidth = 90;
            int frameHeight = 150;

            _bag.addEntity(_playerSprite, Screen);
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

#if DEBUG
            _debugRect = new ColouredRectangle(new Rectangle((int)_playerSprite.X, (int)_playerSprite.Y, 4, 4), Color.Red);
            _debugRect.Screen = Screen;
            _debugRect.Load();
#endif
            
            _velocity = Vector2.Zero;
            _speed = Screen.Engine.RenderHeight / 3.0f; // The float is the number of seconds to cross the height of the screen

            _loaded = true;
        }

        /// <summary>
        /// Updates player
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _playerSprite.setRotation(HelperFunctions.GetBearingBetweenTwoPoints(_playerSprite.Position, Screen.Engine.GetMousePosition(), false));

            float _deltaSpeed = _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (InputHelper.isKeyPressNew(Keys.W))
                _velocity.Y -= _deltaSpeed;
            if (InputHelper.isKeyUpNew(Keys.W))
                _velocity.Y += _deltaSpeed;
            if (InputHelper.isKeyPressNew(Keys.S))
                _velocity.Y += _deltaSpeed;
            if (InputHelper.isKeyUpNew(Keys.S))
                _velocity.Y -= _deltaSpeed;
            if (InputHelper.isKeyPressNew(Keys.D))
                _velocity.X += _deltaSpeed;
            if (InputHelper.isKeyUpNew(Keys.D))
                _velocity.X -= _deltaSpeed;
            if (InputHelper.isKeyPressNew(Keys.A))
                _velocity.X -= _deltaSpeed;
            if (InputHelper.isKeyUpNew(Keys.A))
                _velocity.X += _deltaSpeed;

            _playerSprite.Position += _velocity;

            if (_velocity != Vector2.Zero)
            {
                _playerSprite.Animation = "WALK";
            }
            else if (_velocity == Vector2.Zero)
            {
                _playerSprite.Animation = "IDLE";
            }

            // Binds the position to within 5% and 95% of the render screen size
            _playerSprite.Position = HelperFunctions.KeepVectorInBounds(_playerSprite.Position,
                (int)(Screen.Engine.RenderWidth * 0.05), (int)(Screen.Engine.RenderWidth * 0.95),
                (int)(Screen.Engine.RenderHeight * 0.05), (int)(Screen.Engine.RenderHeight * 0.95));

#if DEBUG
            _debugRect.X = (int)_playerSprite.Position.X - _debugRect.Width / 2;
            _debugRect.Y = (int)_playerSprite.Position.Y - _debugRect.Height / 2;
#endif

            _bag.Update(gameTime);
        }

        /// <summary>
        /// Draws the player to the screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="sBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            _bag.Draw(gameTime, sBatch);
            //sBatch.Draw(_playersheet, _position, _animationFrames[0], Color.White);

#if DEBUG
            _debugRect.Draw(gameTime, sBatch);
#endif
        }
    }
}
