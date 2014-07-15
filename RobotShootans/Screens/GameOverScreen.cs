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
    /// <summary>The screen that gets shown when the game is lost</summary>
    public class GameOverScreen : GameScreen
    {
        GUI_TextItem[] _gameOverText;

        int _endScore;

        /// <summary>constructor for the game over screen</summary>
        /// <param name="blockUpdatingIn"></param>
        /// <param name="scoreIn"></param>
        public GameOverScreen(bool blockUpdatingIn = true, int scoreIn = 0)
            : base (blockUpdatingIn)
        {
            // You done fucked up son
            // Game. Over.
            _endScore = scoreIn;

            _screenName = "GAME OVER SCREEN";
        }

        /// <summary>Sets up the screen text</summary>
        public override void loadGameScreen()
        {
            _physicsEnabled = false;

            ColouredRectangle bg = new ColouredRectangle(new Rectangle(0, 0, Engine.RenderWidth, Engine.RenderHeight), Color.LightBlue);
            bg.DrawOrder = 0;
            addEntity(bg);

            _gameOverText = new GUI_TextItem[4];
            
            for (int i = 0; i < 4; i++)
            {
                _gameOverText[i] = new GUI_TextItem();
                _gameOverText[i].setFont(Engine.loadFont("FiraSans"));
                _gameOverText[i].setColor(Color.Red);
                _gameOverText[i].setOutlineColor(Color.Black);
                _gameOverText[i].DrawOutline = true;
                _gameOverText[i].Scale = new Vector2(1.5f);
                _gameOverText[i].Position = new Vector2(Engine.RenderWidth / 2f, Engine.RenderHeight * ( (i+1f) * 0.2f ) );
                _gameOverText[i].setOrigin(OriginPosition.CENTER);
                _gameOverText[i].DrawOrder = 2;
                addEntity(_gameOverText[i]);
            }

            _gameOverText[0].setText("GAME OVER");
            _gameOverText[1].setText("YOU SCORED " + _endScore + " POINTS");
            _gameOverText[2].setText("CARE TO TRY AGAIN?");
            _gameOverText[3].setText("PRESS SPACE TO RESTART");

            Engine.StopSong();
            Engine.StartSong("ObservingTheStar");

            _loaded = true;
        }

        /// <summary>
        /// Updates the Game Over screen. Will restart the game
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if(InputHelper.isKeyPressNew(Keys.Space))
            {
                Engine.removeGameScreen(this);
                Engine.pushGameScreen(new GameplayScreen());
            }

            base.Update(gameTime);
        }
    }
}
