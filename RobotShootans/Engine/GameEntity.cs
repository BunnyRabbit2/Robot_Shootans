using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Engine
{
    public class GameEntity
    {
        protected bool _loaded;
        public bool Loaded { get { return _loaded; } }

        string _componentName;
        public string ComponentName { get { return _componentName; } }

        public GameScreen Screen;

        public GameEntity(string componentName)
        {
            _componentName = componentName;
        }

        public virtual void Load()
        {

        }

        public virtual void Unload()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sBatch)
        {

        }
    }
}
