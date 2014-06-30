using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RobotShootans.Engine;
using RobotShootans.Entities;
using System;

// I used the following post as inspiration and a guide for getting the movement working how I wanted it
// http://www.badlogicgames.com/forum/viewtopic.php?f=11&t=14129

namespace RobotShootans.Screens
{
    /// <summary>
    /// A physics test screen for testing player movement using Farseer physics
    /// </summary>
    public class PhysicsTestScreenTwo : GameScreen
    {
        PhysicsBox _player;

        float _maxVelocity, _maxAngleVelocity, _moveImpulse, _angleMoveImpulse;

        GUI_TextItem _dirDisplay, _impulseDisplay, _maxVelDisplay;

        /// <summary>Creates the physics screen</summary>
        /// <param name="blockUpdating"></param>
        public PhysicsTestScreenTwo(bool blockUpdating = false)
            : base (blockUpdating)
        {
            _screenName = "PHYSICS TEST SCREEN TWO";
        }

        /// <summary>Changes the maximum velocity value</summary>
        /// <param name="upOrDown"></param>
        public void changeMaxVel(int upOrDown)
        {
            if (upOrDown == 1)
                _maxVelocity++;
            else if (upOrDown == -1)
                _maxVelocity--;

            _maxAngleVelocity = (float)Math.Sqrt(((double)_maxVelocity * (double)_maxVelocity) / 2.0);
        }

        /// <summary>
        /// Loads the screen
        /// </summary>
        public override void loadGameScreen()
        {
            _physicsWorld = new World(new Vector2(0f, 0f));

            addEntity(new ColouredRectangle(new Rectangle(0, 0, 1920, 1080), Color.DarkBlue));

            _maxVelocity = 4f;
            _maxAngleVelocity = (float)Math.Sqrt(((double)_maxVelocity * (double)_maxVelocity) / 2.0);

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

            _maxVelDisplay = new GUI_TextItem();
            _maxVelDisplay.setFont(Engine.loadFont("SourceSansPro-Regular"));
            _maxVelDisplay.Position = new Vector2(100, 300);
            _maxVelDisplay.setColor(Color.Red);
            addEntity(_maxVelDisplay);
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
                vertDir--;
            if (InputHelper.isKeyDown(Keys.S))
                vertDir++;
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

            if (InputHelper.isKeyPressNew(Keys.P))
                changeMaxVel(1);
            if (InputHelper.isKeyPressNew(Keys.O))
                changeMaxVel(-1);
            _maxVelDisplay.setText(_maxVelocity.ToString());

            if(linearImpulse != Vector2.Zero)
            {
                _player.Body.ApplyLinearImpulse(linearImpulse, _player.SimPos);

                if(horiDir == 0)
                    _player.Body.LinearVelocity = new Vector2(0f, MathHelper.Clamp(_player.Body.LinearVelocity.Y, -_maxVelocity, _maxVelocity));
                else if(vertDir == 0)
                    _player.Body.LinearVelocity = new Vector2(MathHelper.Clamp(_player.Body.LinearVelocity.X, -_maxVelocity, _maxVelocity), 0f);
                else
                    _player.Body.LinearVelocity = new Vector2(
                        MathHelper.Clamp(_player.Body.LinearVelocity.X, -_maxAngleVelocity, _maxAngleVelocity),
                        MathHelper.Clamp(_player.Body.LinearVelocity.Y, -_maxAngleVelocity, _maxAngleVelocity));
            }
            else
            {
                _player.Body.LinearVelocity = Vector2.Zero;
            }

            _player.Body.Position = ConvertUnits.ToSimUnits(
                HelperFunctions.KeepVectorInBounds(_player.Position,
                (int)(Engine.RenderWidth * 0.05), (int)(Engine.RenderWidth * 0.95),
                (int)(Engine.RenderHeight * 0.05), (int)(Engine.RenderHeight * 0.95)));

            _physicsWorld.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

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
