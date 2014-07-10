using Microsoft.Xna.Framework;
using RobotShootans.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Entities
{
    /// <summary>
    /// An entity used for displaying a HUD. Uses events recieved to change stuff
    /// </summary>
    public class GUI_HUD : GameEntity
    {
        GUI_TextItem _ammoCounter, _scoreCounter, _lives;
        int _currentAmmo, _currentScore;

        /// <summary>Gets the current score</summary>
        public int Score { get { return _currentScore; } }

        /// <summary>
        /// Constructor for the HUD
        /// </summary>
        /// <param name="entityName"></param>
        public GUI_HUD(string entityName = "HUD")
            : base (entityName)
        {
            _ammoCounter = new GUI_TextItem();
            _scoreCounter = new GUI_TextItem();
        }

        /// <summary>
        /// Sets up all the GUI_TextItems
        /// </summary>
        public override void Load()
        {
            _ammoCounter.setFont(Screen.Engine.loadFont("SourceSansPro-Regular"));
            _currentAmmo = 0;
            _ammoCounter.setText(_currentAmmo.ToString());
            _ammoCounter.Position = new Vector2(Screen.Engine.RenderWidth * 0.1f, Screen.Engine.RenderHeight * 0.1f);
            _ammoCounter.setColor(Color.Red);
            _ammoCounter.DrawOrder = 10;
            Screen.addEntity(_ammoCounter);

            _scoreCounter.setFont(Screen.Engine.loadFont("SourceSansPro-Regular"));
            _currentScore = 0;
            _scoreCounter.setText(_currentScore.ToString());
            _scoreCounter.Position = new Vector2(Screen.Engine.RenderWidth * 0.9f, Screen.Engine.RenderHeight * 0.1f);
            _scoreCounter.setColor(Color.Red);
            _scoreCounter.DrawOrder = 10;
            _scoreCounter.setOrigin(OriginPosition.TOPRIGHT);
            Screen.addEntity(_scoreCounter);

            _loaded = true;
        }

        /// <summary>
        /// Unloads all entities used by this from the screen owner
        /// </summary>
        public override void Unload()
        {
            Screen.removeEntity(_ammoCounter);
            Screen.removeEntity(_scoreCounter);
        }

        /// <summary>
        /// Handles an event handed to it
        /// </summary>
        /// <param name="eventIn"></param>
        /// <returns>Whether the event was used</returns>
        public override bool HandleEvent(GameEvent eventIn)
        {
            if(eventIn.EventType == EventType.AMMO_CHANGED)
            {
                _currentAmmo += eventIn.ChangeInt;
                return true;
            }
            else if(eventIn.EventType == EventType.SET_AMMO)
            {
                _currentAmmo = eventIn.ChangeInt;
                return true;
            }
            else if(eventIn.EventType == EventType.SCORE_CHANGED)
            {
                _currentScore += eventIn.ChangeInt;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Updates all textitems with their current values
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (_currentAmmo == -1)
                _ammoCounter.setText("\u221E"); // Infinity symbol
            else
                _ammoCounter.setText(_currentAmmo.ToString());

            _scoreCounter.setText(_currentScore.ToString());
        }
    }
}

