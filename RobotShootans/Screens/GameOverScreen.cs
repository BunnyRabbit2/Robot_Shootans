using Microsoft.Xna.Framework;
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
        GUI_TextItem[] _nameText;

        Score _endScore;

        string _alphabet;

        int _flashTimer = 0;
        int _currentCharacter = 0;
        int _currentLetter = 1;

        /// <summary>constructor for the game over screen</summary>
        /// <param name="blockUpdatingIn"></param>
        /// <param name="scoreIn"></param>
        public GameOverScreen(bool blockUpdatingIn = true, Score scoreIn = new Score())
            : base(blockUpdatingIn)
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

            _gameOverText = new GUI_TextItem[6];

            float diff = Engine.RenderHeight / (float)(_gameOverText.Length + 1);

            for (int i = 0; i < _gameOverText.Length; i++)
            {
                _gameOverText[i] = new GUI_TextItem();
                _gameOverText[i].setFont(Engine.loadFont("FiraSans"));
                _gameOverText[i].setColor(Color.Red);
                _gameOverText[i].setOutlineColor(Color.Black);
                _gameOverText[i].DrawOutline = true;
                _gameOverText[i].Scale = new Vector2(1.5f);
                _gameOverText[i].Position = new Vector2(Engine.RenderWidth / 2f, (i + 1f) * diff);
                _gameOverText[i].setOrigin(OriginPosition.CENTER);
                _gameOverText[i].DrawOrder = 2;
                addEntity(_gameOverText[i]);
            }

            _gameOverText[0].setText("GAME OVER");
            _gameOverText[1].setText("YOU SCORED " + _endScore.TheScore + " POINTS");
            _gameOverText[2].setText("NAME:     ");
            _gameOverText[3].setText("CARE TO TRY AGAIN?");
            _gameOverText[4].setText("PRESS SPACE TO RESTART");
            _gameOverText[5].setText("PRESS ENTER TO INPUT HIGH SCORE");

            _nameText = new GUI_TextItem[3];

            for (int i = 0; i < _nameText.Length; i++)
            {
                _nameText[i] = new GUI_TextItem();
                _nameText[i].setFont(Engine.loadFont("FiraSans"));
                _nameText[i].setColor(Color.Red);
                _nameText[i].setOutlineColor(Color.Black);
                _nameText[i].DrawOutline = true;
                _nameText[i].Scale = new Vector2(1.5f);
                _nameText[i].Position = new Vector2(Engine.RenderWidth / 2f + 110f + (i * 65f), 3f * diff);
                _nameText[i].setOrigin(OriginPosition.CENTER);
                _nameText[i].DrawOrder = 2;
                addEntity(_nameText[i]);
            }

            _nameText[0].setText("A");
            _nameText[1].setText("A");
            _nameText[2].setText("A");

            _alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";

            for (int i = 0; i < _alphabet.Length; i++)
            {
                if (_alphabet[i] == _nameText[_currentCharacter].DisplayText[0])
                {
                    _currentLetter = i;
                    break;
                }
            }

            Engine.StartSong("ObservingTheStar");

            _loaded = true;
        }

        /// <summary>
        /// Updates the Game Over screen. Will restart the game
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _flashTimer += gameTime.ElapsedGameTime.Milliseconds;

            if(_flashTimer > 750 && _flashTimer < 1500)
            {
                makeLetterUnderScore();
            }
            else if(_flashTimer > 1500)
            {
                makeUnderScoreLetter();
            }

            if (InputHelper.isKeyPressNew(Keys.Up))
            {
                makeUnderScoreLetter();

                if (_currentLetter == _alphabet.Length)
                    _currentLetter = 0;
                else
                    _currentLetter++;

                makeUnderScoreLetter();
            }

            if (InputHelper.isKeyPressNew(Keys.Down))
            {
                makeUnderScoreLetter();

                if (_currentLetter == 0)
                    _currentLetter = _alphabet.Length - 1;
                else
                    _currentLetter--;

                makeUnderScoreLetter();
            }

            if(InputHelper.isKeyPressNew(Keys.Right) && _currentCharacter < 2)
            {
                makeUnderScoreLetter();
                _currentCharacter++;
                for (int i = 0; i < _alphabet.Length; i++)
                {
                    if (_alphabet[i] == _nameText[_currentCharacter].DisplayText[0])
                    {
                        _currentLetter = i;
                        break;
                    }
                }
                makeUnderScoreLetter();
            }

            if (InputHelper.isKeyPressNew(Keys.Left) && _currentCharacter > 0)
            {
                makeUnderScoreLetter();
                _currentCharacter--;
                for (int i = 0; i < _alphabet.Length; i++)
                {
                    if (_alphabet[i] == _nameText[_currentCharacter].DisplayText[0])
                    {
                        _currentLetter = i;
                        break;
                    }
                }
                makeUnderScoreLetter();
            }

            if (InputHelper.isKeyPressNew(Keys.Space))
            {
                Engine.removeGameScreen(this);
                Engine.pushGameScreen(new GameplayScreen());
            }

            if (InputHelper.isKeyPressNew(Keys.Enter))
            {
                _endScore.TheName = _nameText[0].DisplayText + _nameText[1].DisplayText + _nameText[2].DisplayText;
                HighScores.addScore(_endScore);
                Engine.removeGameScreen(this);
                Engine.pushGameScreen(new HighScoreScreen());
            }

            base.Update(gameTime);
        }

        private void makeLetterUnderScore()
        {
            _nameText[_currentCharacter].setText("_");
        }

        private void makeUnderScoreLetter()
        {
            _nameText[_currentCharacter].setText(_alphabet[_currentLetter].ToString());

            _flashTimer = 0;
        }
    }
}