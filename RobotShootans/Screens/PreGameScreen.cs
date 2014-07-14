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
    /// <summary>
    /// Pre Game Screen. So it doesn't go from the Splash Screen to just playing
    /// </summary>
    public class PreGameScreen : GameScreen
    {
        GUI_TextItem[] _preGameText;

        /// <summary>Constructor for the game over screen</summary>
        /// <param name="blockUpdatingIn"></param>
        public PreGameScreen(bool blockUpdatingIn = true)
            : base (blockUpdatingIn)
        {
            _screenName = "PRE GAME SCREEN";
        }

        /// <summary>Sets up the screen text</summary>
        public override void loadGameScreen()
        {
            _physicsEnabled = false;

            //ColouredRectangle bg = new ColouredRectangle(new Rectangle(0, 0, Engine.RenderWidth, Engine.RenderHeight), Color.LightBlue);
            //bg.DrawOrder = 0;
            //addEntity(bg);

            _preGameText = new GUI_TextItem[4];
            
            for (int i = 0; i < 4; i++)
            {
                _preGameText[i] = new GUI_TextItem();
                _preGameText[i].setFont(Engine.loadFont("SourceSansPro-Regular"));
                _preGameText[i].setColor(Color.Blue);
                _preGameText[i].setOutlineColor(Color.White);
                _preGameText[i].DrawOutline = true;
                _preGameText[i].Scale = new Vector2(1.5f);
                _preGameText[i].Position = new Vector2(Engine.RenderWidth / 2f, Engine.RenderHeight * ( (i+1f) * 0.2f ) );
                _preGameText[i].setOrigin(OriginPosition.CENTER);
                _preGameText[i].DrawOrder = 2;
                addEntity(_preGameText[i]);
            }

            _preGameText[0].setText("ROBOT SHOOTANS");
            _preGameText[0].Scale = new Vector2(2.5f);
            _preGameText[0].setColor(Color.Red);
            _preGameText[0].setOutlineColor(Color.Black);
            _preGameText[1].setText("W,A,S,D TO MOVE");
            _preGameText[2].setText("SPACE TO SHOOT");
            _preGameText[3].setText("PRESS SPACE TO START");

            _loaded = true;
        }

        /// <summary>
        /// Updates the Pre Game screen. Will start the game
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if(InputHelper.isKeyPressNew(Keys.Space))
            {
                Engine.removeGameScreen(this);
            }

            base.Update(gameTime);
        }
    }
}
