using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using RobotShootans.Engine;
using RobotShootans.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Screens
{
    /// <summary>
    /// Pause Screen
    /// </summary>
    public class PauseScreen : GameScreen
    {
        // ColouredRectangle[] _pauseBoxes;
        GUI_TextItem[] _pauseText;
        int _currentSelection;
        int _numberOfOptions;
        SoundEffect _selectNoise;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blockUpdatingIn"></param>
        public PauseScreen(bool blockUpdatingIn = true)
            : base(blockUpdatingIn)
        {
            _screenName = "PAUSE SCREEN";
        }

        /// <summary>Sets up the screen text</summary>
        public override void loadGameScreen()
        {
            _physicsEnabled = false;

            ColouredRectangle bg = new ColouredRectangle(new Rectangle(0, 0, Engine.RenderWidth, Engine.RenderHeight), new Color(0,0,0,128));
            bg.DrawOrder = 0;
            addEntity(bg);

            _numberOfOptions = 3;
            _pauseText = new GUI_TextItem[_numberOfOptions];

            float screenDiff = 1f / (_numberOfOptions + 1);

            for (int i = 0; i < _numberOfOptions; i++)
            {
                _pauseText[i] = new GUI_TextItem();
                _pauseText[i].setFont(Engine.loadFont("FiraSans"));
                _pauseText[i].setColor(Color.Red);
                _pauseText[i].setOutlineColor(Color.Black);
                _pauseText[i].Scale = new Vector2(1.5f);
                _pauseText[i].Position = new Vector2(Engine.RenderWidth / 2f, Engine.RenderHeight * ((i + 1f) * screenDiff));
                _pauseText[i].setOrigin(OriginPosition.CENTER);
                _pauseText[i].DrawOrder = 5;
                addEntity(_pauseText[i]);
            }

            _pauseText[0].setText("RESUME GAME");
            _pauseText[0].DrawOutline = true;
            _pauseText[0].setOutlineColor(Color.Black);
            _pauseText[1].setText("OPTIONS");
            _pauseText[2].setText("QUIT");

            //_pauseBoxes = new ColouredRectangle[_numberOfOptions];
            //for (int i = 0; i < _numberOfOptions; i++ )
            //{
            //    _pauseBoxes[i] = new ColouredRectangle(
            //        new Rectangle((int)_pauseText[i].X, (int)_pauseText[i].Y, (int)(_pauseText[i].TextSize.X * 1.1), (int)(_pauseText[i].TextSize.Y * 1.1)),
            //        new Color(0,0,0,128),
            //        OriginPosition.CENTER
            //        );
            //    _pauseBoxes[i].DrawOrder = 3;
            //    addEntity(_pauseBoxes[i]);
            //}

            _selectNoise = Engine.loadSound("menu_select");

            Engine.StartSong("ObservingTheStar");

            _loaded = true;
        }

        /// <summary>
        /// Updates the Menu screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            int oldSel = _currentSelection;

            if (InputHelper.isKeyPressNew(Keys.Up) && _currentSelection > 0)
            {
                _currentSelection--;
                _selectNoise.Play();
            }

            if (InputHelper.isKeyPressNew(Keys.Down) && _currentSelection < _numberOfOptions - 1)
            {
                _currentSelection++;
                _selectNoise.Play();
            }

            if (oldSel != _currentSelection)
            {
                _pauseText[oldSel].DrawOutline = false;
                _pauseText[_currentSelection].DrawOutline = true;
            }

            if (InputHelper.isKeyPressNew(Keys.Enter))
            {
                if (_currentSelection == 0)
                {
                    Engine.removeGameScreen(this);
                }
                else if (_currentSelection == 1)
                {
                    Engine.pushGameScreen(new OptionsScreen());
                }
                else if (_currentSelection == 2)
                {
                    Engine.removeGameScreen(this);
                    Engine.removeGameScreen("GAMEPLAY SCREEN");
                    Engine.pushGameScreen(new MenuScreen());
                }
            }

            base.Update(gameTime);
        }
    }
}
