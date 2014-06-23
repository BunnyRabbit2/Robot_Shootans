﻿// http://www.badlogicgames.com/forum/viewtopic.php?f=11&t=14129

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RobotShootans.Engine;
using RobotShootans.Entities;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace RobotShootans.Screens
{
    /// <summary>
    /// A physics test screen for testing player movement using Farseer physics
    /// </summary>
    public class PhysicsTestScreenTwo : GameScreen
    {
        private World _physicsWorld;
        /// <summary>The Screen's physics world</summary>
        public World PhysicsWorld { get { return _physicsWorld; } }

        PhysicsBox _player;

        float _maxVelocity, _moveImpulse, _angleMoveImpulse;

        GUI_TextItem _dirDisplay, _impulseDisplay;

        /// <summary>Creates the physics screen</summary>
        /// <param name="blockUpdating"></param>
        public PhysicsTestScreenTwo(bool blockUpdating = false)
            : base (blockUpdating)
        {
            _screenName = "PHYSICS TEST SCREEN TWO";
        }

        /// <summary>
        /// Loads the screen
        /// </summary>
        public override void loadGameScreen()
        {
            _physicsWorld = new World(new Vector2(0f, 0f));

            addEntity(new ColouredRectangle(new Rectangle(0, 0, 1920, 1080), Color.DarkBlue));

            _maxVelocity = 10f;
            _moveImpulse = 10f;
            _angleMoveImpulse = (float)Math.Sqrt(((double)_moveImpulse * (double)_moveImpulse) / 2.0);

            _player = new PhysicsBox();
            addEntity(_player);
            _player.SetupBox(_physicsWorld, 64, 64, false, Engine.RenderOrigin, 0f, 0.2f, 0f, Color.Red, OriginPosition.CENTER);

            _loaded = true;

            _dirDisplay = new GUI_TextItem();
            _dirDisplay.setFont(Engine.loadFont("SourceSansPro-Regular"));
            _dirDisplay.Position = new Vector2(100, 100);
            _dirDisplay.setColor(Color.Red);
            addEntity(_dirDisplay);

            _impulseDisplay = new GUI_TextItem();
            _impulseDisplay.setFont(Engine.loadFont("SourceSansPro-Regular"));
            _impulseDisplay.Position = new Vector2(100, 200);
            _impulseDisplay.setColor(Color.Red);
            addEntity(_impulseDisplay);
        }

        /// <summary>
        /// Updates the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            int vertDir = 0;
            int horiDir = 0;
            Vector2 linearImpulse = Vector2.Zero;

            if (InputHelper.isKeyDown(Keys.W))
                vertDir++;
            if (InputHelper.isKeyDown(Keys.S))
                vertDir--;
            if (InputHelper.isKeyDown(Keys.D))
                horiDir++;
            if (InputHelper.isKeyDown(Keys.A))
                horiDir--;

            if (vertDir == 0)
            {
                if (horiDir == 1)
                    linearImpulse.X = _moveImpulse;
                else if (horiDir == -1)
                    linearImpulse.X = -_moveImpulse;
            }
            else
            {
                if (horiDir != 0)
                {
                    if(vertDir == 1)
                    {
                        if (horiDir == 1)
                            linearImpulse = new Vector2(_angleMoveImpulse, _angleMoveImpulse);
                        else if (horiDir == -1)
                            linearImpulse = new Vector2(-_angleMoveImpulse, _angleMoveImpulse);
                    }
                    else if(vertDir == -1)
                    {
                        if (horiDir == 1)
                            linearImpulse = new Vector2(_angleMoveImpulse, -_angleMoveImpulse);
                        else if (horiDir == -1)
                            linearImpulse = new Vector2(-_angleMoveImpulse, -_angleMoveImpulse);
                    }
                }
                else
                {
                    if (vertDir == 1)
                        linearImpulse.Y = _moveImpulse;
                    else if (vertDir == -1)
                        linearImpulse.Y = -_moveImpulse;
                }
            }

            _dirDisplay.setText(horiDir + ", " + vertDir);
            _impulseDisplay.setText(linearImpulse.ToString());

            if(linearImpulse != Vector2.Zero)
            {
                _player.Body.ApplyLinearImpulse(linearImpulse, _player.SimPos);
            }
            else
            {
                _player.Body.LinearVelocity = Vector2.Zero;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="sBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            base.Draw(gameTime, sBatch);
        }
    }
}
