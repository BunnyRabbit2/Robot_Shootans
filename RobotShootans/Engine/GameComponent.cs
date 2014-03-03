using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Engine
{
    public class GameComponent
    {
        protected bool _loaded;
        public bool Loaded { get { return _loaded; } }

        string _componentName;
        public string ComponentName { get { return _componentName; } }

        public GameScreen GameScreen;

        public GameComponent(string componentName)
        {
            _componentName = componentName;
        }

        public void Load()
        {

        }

        public void Unload()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch sBatch)
        {

        }
    }
}
