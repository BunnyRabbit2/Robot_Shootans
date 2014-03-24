using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotShootans.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RobotShootans.Entities
{
    /// <summary>
    /// The player class
    /// </summary>
    public class Player : GameEntity
    {
        Texture2D _playersheet;

        /// <summary>
        /// Creates the Player object
        /// </summary>
        /// <param name="startPos"></param>
        public Player(Vector2 startPos)
            : base("Player")
        {

        }

        /// <summary>
        /// Loads the player sheet and sets animation frames
        /// </summary>
        public override void Load()
        {
            _playersheet = Screen.Engine.Content.Load<Texture2D>("images/game/player-sheet");
        }
    }
}
