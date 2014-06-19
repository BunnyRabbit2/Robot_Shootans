// http://www.badlogicgames.com/forum/viewtopic.php?f=11&t=14129

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

        float _maxVelocity, _moveImpulse;

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

            _player = new PhysicsBox();
            addEntity(_player);
            _player.SetupBox(_physicsWorld, 64, 64, false, Engine.RenderOrigin, 0f, 0.2f, 0.2f, Color.Red, OriginPosition.CENTER);

            _loaded = true;
        }

        /// <summary>
        /// Updates the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
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
