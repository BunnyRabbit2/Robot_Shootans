using Microsoft.Xna.Framework;
using RobotShootans.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Entities
{
    public class GUI_HUD : GameEntity
    {
        GUI_TextItem _ammoCounter, _scoreCounter;
        int _currentAmmo, _currentScore;

        public GUI_HUD(string entityName = "HUD")
            : base (entityName)
        {
            _ammoCounter = new GUI_TextItem();
            _scoreCounter = new GUI_TextItem();
        }

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
            Screen.addEntity(_scoreCounter);

            _loaded = true;
        }

        public override void Unload()
        {
            Screen.removeEntity(_ammoCounter);
        }

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

