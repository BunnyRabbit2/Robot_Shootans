using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RobotShootans.Engine;
using RobotShootans.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace RobotShootans.Screens
{
    /// <summary>
    /// Screen for displaying the high scores
    /// </summary>
    public class HighScoreScreen : GameScreen
    {
        GUI_TextItem[] _highScoreText;

        /// <summary>
        /// Creates the HighScoreScreen
        /// </summary>
        /// <param name="blockUpdatingIn"></param>
        public HighScoreScreen(bool blockUpdatingIn = true)
            : base (blockUpdatingIn)
        {
            _screenName = "HIGH SCORE SCREEN";
        }

        /// <summary>Sets up the screen text</summary>
        public override void loadGameScreen()
        {
            _physicsEnabled = false;

            ColouredRectangle bg = new ColouredRectangle(new Rectangle(0, 0, Engine.RenderWidth, Engine.RenderHeight), Color.DarkGray);
            bg.DrawOrder = 0;
            addEntity(bg);

            _highScoreText = new GUI_TextItem[33];

            float screenDiff = 1f / 15f;

            Score[] _scores = HighScores.Scores;

            for (int i = 0; i < 11; i++)
            {
                for (int i2 = 0; i2 < 3; i2++)
                {
                    _highScoreText[(i * 3) + i2] = new GUI_TextItem();
                    _highScoreText[(i * 3) + i2].setFont(Engine.loadFont("FiraSans"));
                    _highScoreText[(i * 3) + i2].setColor(Color.Red);
                    _highScoreText[(i * 3) + i2].setOutlineColor(Color.Black);
                    _highScoreText[(i * 3) + i2].Position = new Vector2(Engine.RenderWidth * (i2 + 1) * 0.25f, Engine.RenderHeight * ((i + 1f) * screenDiff));
                    _highScoreText[(i * 3) + i2].setOrigin(OriginPosition.CENTER);
                    _highScoreText[(i * 3) + i2].Scale = new Vector2(0.8f);
                    _highScoreText[(i * 3) + i2].DrawOrder = 2;

                    if (i > 0)
                    {
                        if (i2 == 0)
                            _highScoreText[(i * 3) + i2].setText(_scores[i - 1].TheName);
                        else if (i2 == 1)
                            _highScoreText[(i * 3) + i2].setText(_scores[i - 1].TheScore.ToString());
                        else if (i2 == 2)
                            _highScoreText[(i * 3) + i2].setText(_scores[i - 1].TheRobotsKilled.ToString());
                    }

                    addEntity(_highScoreText[(i * 3) + i2]);
                }
            }

            _highScoreText[0].setText("NAME");
            _highScoreText[0].Scale = new Vector2(1.2f);
            _highScoreText[1].setText("SCORE");
            _highScoreText[1].Scale = new Vector2(1.2f);
            _highScoreText[2].setText("ROBOTS KILLED");
            _highScoreText[2].Scale = new Vector2(1.2f);

            GUI_TextItem text = new GUI_TextItem();
            text.setFont(Engine.loadFont("FiraSans"));
            text.setColor(Color.Red);
            text.setOutlineColor(Color.Black);
            text.Position = new Vector2(Engine.RenderWidth / 2, Engine.RenderHeight * 0.9f);
            text.setOrigin(OriginPosition.CENTER);
            text.setText("PRESS ESCAPE TO GO BACK");
            text.DrawOrder = 2;
            addEntity(text);

            Engine.StartSong("ObservingTheStar");

            _loaded = true;
        }

        /// <summary>
        /// Updates the Game Over screen. Will restart the game
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (InputHelper.isKeyPressNew(Keys.Escape))
            {
                if (!Engine.containsScreen("MENU SCREEN"))
                    Engine.pushGameScreen(new MenuScreen());
                Engine.removeGameScreen(this);
            }

            base.Update(gameTime);
        }
    }
}
