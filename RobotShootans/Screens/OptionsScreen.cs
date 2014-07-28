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
    /// A screen for changing the game options
    /// </summary>
    public class OptionsScreen : GameScreen
    {
        GUI_TextItem[] _optionsText, _currentOptionsText;
        int _currentSelection;
        int _numberOfOptions;
        SoundEffect _selectNoise;

        int _selectedResolution = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blockUpdatingIn"></param>
        public OptionsScreen(bool blockUpdatingIn = true)
            : base(blockUpdatingIn)
        {
            _screenName = "OPTIONS SCREEN";
        }

        /// <summary>Sets up the screen text</summary>
        public override void loadGameScreen()
        {
            _physicsEnabled = false;

            ColouredRectangle bg = new ColouredRectangle(new Rectangle(0, 0, Engine.RenderWidth, Engine.RenderHeight), Color.DimGray);
            bg.DrawOrder = 0;
            addEntity(bg);

            _numberOfOptions = 7;
            _optionsText = new GUI_TextItem[_numberOfOptions];

            for (int i = 0; i < _numberOfOptions; i++)
            {
                _optionsText[i] = new GUI_TextItem();
                _optionsText[i].setFont(Engine.loadFont("FiraSans"));
                _optionsText[i].setColor(Color.Red);
                _optionsText[i].setOutlineColor(Color.Black);
                _optionsText[i].Position = new Vector2(Engine.RenderWidth * 0.2f, Engine.RenderHeight * ((i + 1f) * 0.1f));
                _optionsText[i].setOrigin(OriginPosition.TOPLEFT);
                _optionsText[i].DrawOrder = 2;
                addEntity(_optionsText[i]);
            }

            _optionsText[0].setText("MUSIC");
            _optionsText[0].DrawOutline = true;
            _optionsText[0].setOutlineColor(Color.Black);
            _optionsText[1].setText("MUSIC VOLUME");
            _optionsText[2].setText("SOUND EFFECTS");
            _optionsText[3].setText("SFX VOLUME");
            _optionsText[4].setText("FULL SCREEN");
            _optionsText[5].setText("RESOLUTION");
            _optionsText[6].setText("EXIT");

            _currentOptionsText = new GUI_TextItem[_numberOfOptions];

            for (int i = 0; i < _numberOfOptions; i++)
            {
                _currentOptionsText[i] = new GUI_TextItem();
                _currentOptionsText[i].setFont(Engine.loadFont("FiraSans"));
                _currentOptionsText[i].setColor(Color.Black);
                _currentOptionsText[i].Position = new Vector2(Engine.RenderWidth * 0.8f, Engine.RenderHeight * ((i + 1f) * 0.1f));
                _currentOptionsText[i].setOrigin(OriginPosition.TOPRIGHT);
                _currentOptionsText[i].DrawOrder = 2;
                addEntity(_currentOptionsText[i]);
            }

            GameOptions ops = Engine.Options;
            Vector2 currentRes = new Vector2(ops.WindowWidth, ops.WindowHeight);
            for (int i = 0; i < ops.SupportedResolutions.Count; i++)
            {
                if (ops.SupportedResolutions[i].X == currentRes.X && ops.SupportedResolutions[i].Y == currentRes.Y)
                {
                    _selectedResolution = i;
                }
            }

            setCurrentOptionsText();

            _selectNoise = Engine.loadSound("menu_select");

            Engine.StopSong();
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

            if(oldSel != _currentSelection)
            {
                _optionsText[oldSel].DrawOutline = false;
                _optionsText[_currentSelection].DrawOutline = true;
            }

            if (InputHelper.isKeyPressNew(Keys.Enter))
            {
                if (_currentSelection == 0)
                {
                    Engine.Options.SwitchMusic();
                }
                else if (_currentSelection == 2)
                {
                    Engine.Options.SwitchSFX();
                }
                else if (_currentSelection == 4)
                {
                    Engine.Options.switchFullScreen();
                }
                else if (_currentSelection == 6)
                {
                    Engine.removeGameScreen(this);
                    Engine.pushGameScreen(new MenuScreen());
                }
            }

            if (InputHelper.isKeyPressNew(Keys.Right))
            {
                _selectNoise.Play();
                if (_currentSelection == 0)
                {
                    Engine.Options.SwitchMusic();
                }
                else if (_currentSelection == 1)
                {
                    Engine.Options.increaseMusicVolume();
                }
                else if (_currentSelection == 2)
                {
                    Engine.Options.SwitchSFX();
                }
                else if (_currentSelection == 3)
                {
                    Engine.Options.increaseSfxVolume();
                }
                else if (_currentSelection == 4)
                {
                    Engine.Options.switchFullScreen();
                }
                else if (_currentSelection == 5)
                {
                    if (_selectedResolution < Engine.Options.SupportedResolutions.Count - 1)
                        _selectedResolution++;
                    Engine.Options.changeResolution((int)Engine.Options.SupportedResolutions[_selectedResolution].X, (int)Engine.Options.SupportedResolutions[_selectedResolution].Y);
                }
            }

            if (InputHelper.isKeyPressNew(Keys.Left))
            {
                _selectNoise.Play();
                if (_currentSelection == 0)
                {
                    Engine.Options.SwitchMusic();
                }
                else if (_currentSelection == 1)
                {
                    Engine.Options.decreaseMusicVolume();
                }
                else if (_currentSelection == 2)
                {
                    Engine.Options.SwitchSFX();
                }
                else if (_currentSelection == 3)
                {
                    Engine.Options.decreaseSfxVolume();
                }
                else if (_currentSelection == 4)
                {
                    Engine.Options.switchFullScreen();
                }
                else if (_currentSelection == 5)
                {
                    if(_selectedResolution > 0)
                        _selectedResolution--;
                    Engine.Options.changeResolution((int)Engine.Options.SupportedResolutions[_selectedResolution].X, (int)Engine.Options.SupportedResolutions[_selectedResolution].Y);
                }
            }

            setCurrentOptionsText();

            base.Update(gameTime);
        }

        private void setCurrentOptionsText()
        {
            GameOptions ops = Engine.Options;

            _currentOptionsText[0].setText(ops.MusicOn.ToString());
            _currentOptionsText[1].setText(ops.MusicVolume.ToString());
            _currentOptionsText[2].setText(ops.SfxOn.ToString());
            _currentOptionsText[3].setText(ops.SfxVolume.ToString());
            _currentOptionsText[4].setText(ops.FullScreen.ToString());
            _currentOptionsText[5].setText(ops.SupportedResolutions[_selectedResolution].X + " x " + ops.SupportedResolutions[_selectedResolution].Y);
        }
    }
}
