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
    /// Struct used for XML high score sheet
    /// </summary>
    public struct Score
    {
        /// <summary>Name of the person who scored the score</summary>
        public string TheName;
        /// <summary>The score of the person who scored the score</summary>
        public int TheScore;
        /// <summary>How many robots were killed by the person who scored the score</summary>
        public int TheRobotsKilled;
    }

    public class HighScoreScreen : GameScreen
    {
        Score _scoreIn;

        Score[] _scores;
        GUI_TextItem[] _highScoreText;

        public HighScoreScreen(Score scoreIn, bool blockUpdatingIn = true)
            : base (blockUpdatingIn)
        {
            _scoreIn = scoreIn;

            _screenName = "HIGH SCORE SCREEN";
        }

        /// <summary>Sets up the screen text</summary>
        public override void loadGameScreen()
        {
            _physicsEnabled = false;

            ColouredRectangle bg = new ColouredRectangle(new Rectangle(0, 0, Engine.RenderWidth, Engine.RenderHeight), Color.DarkGray);
            bg.DrawOrder = 0;
            addEntity(bg);

            _scores = new Score[10];
            loadHighScores();

            if (_scoreIn.TheScore != 0)
            {
                addScore(_scoreIn);
            }

            _highScoreText = new GUI_TextItem[33];

            float screenDiff = 1f / 15f;

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
        /// Unloads the screen and writes the current top ten score out
        /// </summary>
        public override void unloadGameScreen()
        {
            writeHighScores();
        }

        /// <summary>
        /// Updates the Game Over screen. Will restart the game
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (InputHelper.isKeyPressNew(Keys.Escape))
            {
                Engine.removeGameScreen(this);
            }

            base.Update(gameTime);
        }

        private void loadHighScores()
        {
            List<Score> loadedScores = new List<Score>();

#if DEBUG
            if (File.Exists("../../../Docs/high-scores.xml"))
            {
                XDocument doc = XDocument.Load("../../../Docs/high-scores.xml");
#else
            if ( File.Exists("options.xml") )
            {
                XDocument doc = XDocument.Load("high-scores.xml");
#endif
                var highScores = doc.Descendants("high-scores");

                foreach (var hScore in highScores)
                {
                    var allScores = hScore.Descendants("high-score");

                    foreach (var h in allScores)
                    {
                        Score s = new Score();

                        s.TheName = h.Element("name").Value;
                        s.TheScore = int.Parse(h.Element("score").Value);
                        s.TheRobotsKilled = int.Parse(h.Element("robots-killed").Value);
                        loadedScores.Add(s);
                    }
                }
            }
            else
            {
                LogFile.LogStringLine("Failed to find high-scores.xml. No scores loaded");
            }

            List<Score> sortedScores = loadedScores.OrderByDescending(s => s.TheScore).ToList();

            Score def = new Score();
            def.TheScore = 0;
            def.TheRobotsKilled = 0;
            def.TheName = "AAA";

            for(int i = 0; i < 10; i++)
            {
                if(i > sortedScores.Count - 1)
                    _scores[i] = def;
                else
                    _scores[i] = sortedScores[i];
            }

            writeHighScores();
        }

        /// <summary>
        /// Adds a score to the screen
        /// </summary>
        /// <param name="scoreIn"></param>
        public void addScore(Score scoreIn)
        {
            List<Score> sortedScores = _scores.OrderByDescending(s => s.TheScore).ToList();

            Score def = new Score();
            def.TheScore = 0;
            def.TheRobotsKilled = 0;
            def.TheName = "AAA";

            for (int i = 0; i < 10; i++)
            {
                if (i > sortedScores.Count - 1)
                    _scores[i] = def;
                else
                    _scores[i] = sortedScores[i];
            }
        }

        private void writeHighScores()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
#if DEBUG
            XmlWriter highScoresOut = XmlWriter.Create("../../../Docs/high-scores.xml", settings);
#else
            XmlWriter highScoresOut = XmlWriter.Create("high-scores.xml", settings);
#endif
            highScoresOut.WriteStartDocument();
            highScoresOut.WriteStartElement("high-scores");

            for(int i = 0; i < 10; i++)
            {
                highScoresOut.WriteStartElement("high-score");

                highScoresOut.WriteElementString("score", _scores[i].TheScore.ToString());
                highScoresOut.WriteElementString("name", _scores[i].TheName.ToString());
                highScoresOut.WriteElementString("robots-killed", _scores[i].TheRobotsKilled.ToString());

                highScoresOut.WriteEndElement();
            }

            highScoresOut.WriteEndElement();
            highScoresOut.WriteEndDocument();

            highScoresOut.Close();
        }
    }
}
