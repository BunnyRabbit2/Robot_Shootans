﻿using Microsoft.Xna.Framework;
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
    /// Menu screen
    /// </summary>
    public class MenuScreen : GameScreen
    {
        GUI_TextItem[] _menuText;
        int _currentSelection;
        int _numberOfOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blockUpdatingIn"></param>
        public MenuScreen(bool blockUpdatingIn = true)
            : base(blockUpdatingIn)
        {
            _screenName = "MENU SCREEN";
        }

        /// <summary>Sets up the screen text</summary>
        public override void loadGameScreen()
        {
            _physicsEnabled = false;

            ColouredRectangle bg = new ColouredRectangle(new Rectangle(0, 0, Engine.RenderWidth, Engine.RenderHeight), Color.DimGray);
            bg.DrawOrder = 0;
            addEntity(bg);

            _numberOfOptions = 3;
            _menuText = new GUI_TextItem[_numberOfOptions];

            for (int i = 0; i < _numberOfOptions; i++)
            {
                _menuText[i] = new GUI_TextItem();
                _menuText[i].setFont(Engine.loadFont("FiraSans"));
                _menuText[i].setColor(Color.Red);
                _menuText[i].setOutlineColor(Color.Black);
                //_menuText[i].DrawOutline = true;
                _menuText[i].Scale = new Vector2(1.5f);
                _menuText[i].Position = new Vector2(Engine.RenderWidth / 2f, Engine.RenderHeight * ((i + 1f) * 0.2f));
                _menuText[i].setOrigin(OriginPosition.CENTER);
                _menuText[i].DrawOrder = 2;
                addEntity(_menuText[i]);
            }

            _menuText[0].setText("START GAME");
            _menuText[0].DrawOutline = true;
            _menuText[0].setOutlineColor(Color.Black);
            _menuText[1].setText("OPTIONS");
            _menuText[2].setText("QUIT GAME");

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
                _currentSelection--;

            if (InputHelper.isKeyPressNew(Keys.Down) && _currentSelection < _numberOfOptions - 1)
                _currentSelection++;

            if(oldSel != _currentSelection)
            {
                _menuText[oldSel].DrawOutline = false;
                _menuText[_currentSelection].DrawOutline = true;
            }

            if (InputHelper.isKeyPressNew(Keys.Enter))
            {
                if (_currentSelection == 0)
                {
                    Engine.removeGameScreen(this);
                    Engine.pushGameScreen(new PreGameScreen());
                }
                else if (_currentSelection == 1)
                {
                    Engine.removeGameScreen(this);
                    Engine.pushGameScreen(new OptionsScreen());
                }
                else if (_currentSelection == 2)
                {
                    Engine.Exit();
                }
            }

            base.Update(gameTime);
        }
    }
}
