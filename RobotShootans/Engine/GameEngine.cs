using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Engine
{
    /// <summary>
    /// A singleton class used for handling all the game screens, input and other such things
    /// </summary>
    public class GameEngine
    {
        private static GameEngine instance;

        private GameEngine()
        {
            _gameName = "Robot Shootans";
            _gameScreens = new HashSet<GameScreen>();
        }

        public static GameEngine Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new GameEngine();
                }
                return instance;
            }
        }

        private string _gameName;
        public string GameName
        {
            get { return _gameName; }
        }
        private HashSet<GameScreen> _gameScreens;
        private HashSet<GameScreen> _screensToRemove;

        public void pushGameScreen(GameScreen gameScreenIn)
        {
            gameScreenIn.Engine = this;
            gameScreenIn.loadGameScreen();
            _gameScreens.Add(gameScreenIn);
        }

        public void removeGameScreen(GameScreen screenToRemove)
        {

        }

        public void removeGameScreen(string screenToRemove)
        {

        }

        public void unloadScreens()
        {

        }

        public void runGame()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw()
        {

        }
    }
}
