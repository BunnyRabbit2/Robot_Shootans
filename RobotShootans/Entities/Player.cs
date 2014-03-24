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

            _loaded = true;
        }

        /// <summary>
        /// Updates player
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _rotation = HelperFunctions.GetBearingBetweenTwoPoints(_position, Screen.Engine.GetMousePosition(), false);
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
