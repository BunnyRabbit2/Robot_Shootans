using Microsoft.Xna.Framework;
using RobotShootans.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Entities
{
    /// <summary>Robot class. Might make abstract</summary>
    public class Robot : GameEntity
    {
        ColouredRectangle _displayRect;
        RobotSpawner _spawner;

        /// <summary>Creates the robot and sets the start position</summary>
        /// <param name="positionIn"></param>
        /// /// <param name="spawnerIn"></param>
        public Robot(Vector2 positionIn, RobotSpawner spawnerIn) : base ("ROBOT")
        {
            _spawner = spawnerIn;
            _displayRect = new ColouredRectangle(new Rectangle((int)positionIn.X, (int)positionIn.Y, 20, 20), Color.Red);
        }

        /// <summary></summary>
        public override void Load()
        {
            Screen.addEntity(_displayRect);

            _loaded = true;
        }

        /// <summary>Updates the robot and sends it towars the player</summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
