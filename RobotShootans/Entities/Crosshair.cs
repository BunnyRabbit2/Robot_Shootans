using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Entities
{
    /// <summary>
    /// A crosshair that always displays at the mouse co-ordiantes
    /// </summary>
    public class Crosshair : Sprite
    {
        /// <summary>
        /// Creates the crosshair object
        /// </summary>
        public Crosshair()
            : base ("CROSSHAIR")
        {
        }

        /// <summary>Loads the default crosshair image</summary>
        public override void Load()
        {
            setImage("game/crosshair");
            setOrigin(OriginPosition.CENTER);
            setScale(0.45f);
        }

        /// <summary>
        /// Sets the position of the crosshair to the mouse co-ordinates
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _position = Screen.Engine.GetMousePosition();
        }
    }
}
