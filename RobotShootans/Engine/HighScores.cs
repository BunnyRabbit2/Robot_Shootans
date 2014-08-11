using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace RobotShootans.Engine
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

    /// <summary>
    /// Static class for managing high scores
    /// </summary>
    public static class HighScores
    {
        static Score[] _scores;

        /// <summary>An array containing the top ten scores</summary>
        public static Score[] Scores { get { return _scores; } }

        /// <summary>Loads the high scores</summary>
        public static void Initialise()
        {
            _scores = new Score[10];
            loadHighScores();
        }

        private static void loadHighScores()
        {
            List<Score> loadedScores = new List<Score>();

#if DEBUG
            if (File.Exists("../../../Docs/high-scores.xml"))
            {
                XDocument doc = XDocument.Load("../../../Docs/high-scores.xml");
#else
            if (File.Exists("options.xml"))
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

                        // Truncate whatever the name is to three characters
                        s.TheName = h.Element("name").Value.Substring(0,3).ToUpper();
                        // Validate score in so people can't cause exceptions
                        if (!int.TryParse(h.Element("score").Value, out s.TheScore))
                        {
                            LogFile.LogStringLine("Failed to parse score from high score file. Reset score to 0");
                            s.TheScore = 0;
                        }
                        // Validate robots killed in to people can't cause exceptions
                        if (!int.TryParse(h.Element("robots-killed").Value, out s.TheRobotsKilled))
                        {
                            LogFile.LogStringLine("Failed to parse robots killed from high score file. Reset robots killed to 0");
                            s.TheRobotsKilled = 0;
                        }
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

            for (int i = 0; i < 10; i++)
            {
                if (i > sortedScores.Count - 1)
                    _scores[i] = def;
                else
                    _scores[i] = sortedScores[i];
            }

            writeHighScores();
        }

        /// <summary>
        /// Writes out the top ten high scores
        /// </summary>
        public static void writeHighScores()
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

            for (int i = 0; i < 10; i++)
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

        /// <summary>
        /// Adds a score to the list and then cuts to top ten
        /// </summary>
        /// <param name="scoreIn"></param>
        public static void addScore(Score scoreIn)
        {
            List<Score> sortedScores = _scores.ToList();
            sortedScores.Add(scoreIn);
            sortedScores = sortedScores.OrderByDescending(s => s.TheScore).ToList();

            Score def = new Score();
            def.TheScore = 0;
            def.TheRobotsKilled = 0;
            def.TheName = "AAA";

            // If there's not enough scores in the sorted list use the default instead
            for (int i = 0; i < 10; i++)
            {
                if (i > sortedScores.Count - 1)
                    _scores[i] = def;
                else
                    _scores[i] = sortedScores[i];
            }
        }

        private static void resetScores()
        {
            Score def = new Score();
            def.TheScore = 0;
            def.TheRobotsKilled = 0;
            def.TheName = "AAA";

            for (int i = 0; i < 10; i++)
            {
                _scores[i] = def;
            }
        }
    }
}
