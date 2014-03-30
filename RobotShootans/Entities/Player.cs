using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotShootans.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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
            _playersheet = Screen.Engine.Content.Load<Texture2D>("images/game/player-sheet");
            _origin = new Vector2(132/2, 140/2);

            _animationFrames = new Rectangle[numberOfFrames];
            _animationFrames[0] = new Rectangle(0, 0, 132, 140);
            _animationFrames[1] = new Rectangle(132, 0, 132, 140);
            _animationFrames[2] = new Rectangle(0, 140, 132, 140);
            _animationFrames[3] = new Rectangle(132, 140, 132, 140);
            _animationFrames[4] = new Rectangle(0, 280, 132, 140);
            _animationFrames[5] = new Rectangle(132, 280, 132, 140);
            _animationFrames[6] = new Rectangle(0, 420, 132, 140);
            _animationFrames[7] = new Rectangle(132, 420, 132, 140);

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
        }
    }
}
