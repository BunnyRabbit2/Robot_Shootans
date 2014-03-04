using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotShootans.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RobotShootans.Entities
{
    public class ColouredRectangle : GameEntity
    {
        private Texture2D _rect;
        private Rectangle _size;
        private Color _color;

        public ColouredRectangle(Rectangle size, Color colour)
            : base ("Rect")
        {
            _size = size;
            _color = colour;
        }

        public override void Load()
        {
            _rect = new Texture2D(Screen.Engine.Graphics, 1, 1);
            _rect.SetData(new[] { Color.White });

            _loaded = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            sBatch.Draw(_rect, _size, _color);
        }
    }
}
