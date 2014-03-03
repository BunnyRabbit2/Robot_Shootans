using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Engine
{
    public class GameScreen
    {
        public GameEngine Engine;

        private HashSet<GameComponent> _gameComponents;
        private bool _loaded;
        private bool _paused;
        private bool blockUpdating;

        private string _screenName;
        public string ScreenName
        {
            get { return _screenName; }
        }

        private HashSet<GameComponent> _componentsToRemove;
        
        public GameScreen()
        {

        }

        public void loadGameScreen()
        {

        }
    }
}
