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
        Texture2D _playersheet;
        Rectangle[] _animationFrames;
        Vector2 _origin;
        Vector2 _position;
        float _rotation;

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
            _position = startPos;
        }

        /// <summary>
        /// Loads the player sheet and sets animation frames
        /// </summary>
        public override void Load()
        {
            int numberOfFrames = 8;
            _playersheet = Screen.Engine.loadTexture("game/player-sheet");

            int frameWidth = 90;
            int frameHeight = 150;

            _origin = new Vector2(frameWidth / 2, frameHeight / 2);

#if DEBUG
            _debugRect = new ColouredRectangle(new Rectangle((int)_position.X, (int)_position.Y, 4, 4), Color.Red);
            _debugRect.Screen = Screen;
            _debugRect.Load();
#endif

            _animationFrames = new Rectangle[numberOfFrames];
            _animationFrames[0] = new Rectangle(0, 0, frameWidth, frameHeight);
            _animationFrames[1] = new Rectangle(frameWidth, 0, frameWidth, frameHeight);
            _animationFrames[2] = new Rectangle(0, frameHeight, frameWidth, frameHeight);
            _animationFrames[3] = new Rectangle(frameWidth, frameHeight, frameWidth, frameHeight);
            _animationFrames[4] = new Rectangle(0, frameHeight*2, frameWidth, frameHeight);
            _animationFrames[5] = new Rectangle(frameWidth, frameHeight*2, frameWidth, frameHeight);
            _animationFrames[6] = new Rectangle(0, frameHeight*3, frameWidth, frameHeight);
            _animationFrames[7] = new Rectangle(frameWidth, frameHeight*3, frameWidth, frameHeight);

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
            _rotation = HelperFunctions.GetBearingBetweenTwoPoints(_position, Screen.Engine.GetMousePosition(), false);

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

            _position += _velocity;

            // Binds the position to within 5% and 95% of the render screen size
            _position = HelperFunctions.KeepVectorInBounds(_position,
                (int)(Screen.Engine.RenderWidth * 0.05), (int)(Screen.Engine.RenderWidth * 0.95),
                (int)(Screen.Engine.RenderHeight * 0.05), (int)(Screen.Engine.RenderHeight * 0.95));

#if DEBUG
            _debugRect.X = (int)_position.X - _debugRect.Width / 2;
            _debugRect.Y = (int)_position.Y - _debugRect.Height / 2;
#endif
        }

        /// <summary>
        /// Draws the player to the screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="sBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            sBatch.Draw(_playersheet, _position, _animationFrames[0], Color.White, _rotation, _origin, 1f, SpriteEffects.None, 0f);
            //sBatch.Draw(_playersheet, _position, _animationFrames[0], Color.White);

#if DEBUG
            _debugRect.Draw(gameTime, sBatch);
#endif
        }
    }
}
