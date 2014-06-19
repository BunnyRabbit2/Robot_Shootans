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
    /// A screen used to test implementation of Farseer physics system with falling boxes
    /// </summary>
    public class PhysicsTestScreen : GameScreen
    {
        List<PhysicsBox> _boxes;

        PhysicsBox _groundBox, _platform1, _platform2;

        private World _physicsWorld;
        /// <summary>
        /// The physics world for the screen
        /// </summary>
        public World PhysicsWorld { get { return _physicsWorld; } }

        /// <summary>
        /// Constructor for the Physics Test Screen
        /// </summary>
        /// <param name="blockUpdating">Sets whether the screen block screens under it from updating</param>
        public PhysicsTestScreen(bool blockUpdating = false)
            : base(blockUpdating)
        {
            _screenName = "PHYSICS TEST SCREEN";
        }
        
        /// <summary>
        /// Loads the screen and sets up all the required platforms for the screen
        /// </summary>
        public override void loadGameScreen()
        {
            _physicsWorld = new World(new Vector2(0f, 10f));

            _boxes = new List<PhysicsBox>();

            addEntity(new ColouredRectangle(new Rectangle(0, 0, 1920, 1080), Color.DarkGray));

            _groundBox = new PhysicsBox();
            addEntity(_groundBox);
            _groundBox.SetupBox(_physicsWorld, Engine.RenderWidth * 2, 50, true, new Vector2(Engine.RenderOrigin.X, Engine.RenderHeight - 50), 0.0f, 0.2f, 0.2f, Color.Yellow, OriginPosition.CENTER);

            _platform1 = new PhysicsBox();
            addEntity(_platform1);
            _platform1.SetupBox(_physicsWorld, 500, 50, true, new Vector2(Engine.RenderWidth * 0.33f, Engine.RenderHeight * 0.5f), 45f, 0.2f, 0.2f, Color.Yellow);

            _platform2 = new PhysicsBox();
            addEntity(_platform2);
            _platform2.SetupBox(_physicsWorld, 500, 50, true, new Vector2(Engine.RenderWidth * 0.66f, Engine.RenderHeight * 0.5f), -45f, 0.2f, 0.2f, Color.Yellow);

            _loaded = true;
        }

        /// <summary>
        /// Unloads all content for the game screen
        /// </summary>
        public override void unloadGameScreen()
        {
            
        }

        /// <summary>
        /// Updates the physics test screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if(InputHelper.isKeyPressNew(Keys.Space))
            {
                Random random = new Random();
                int width = random.Next(20, 100);
                int height = random.Next(20, 100);

                width = 50;
                height = 50;

                PhysicsBox pBox = new PhysicsBox();
                addEntity(pBox);

                pBox.SetupBox(_physicsWorld, width, height, false, new Vector2(random.Next((int)(Engine.RenderWidth * 0.25), (int)(Engine.RenderWidth * 0.75)), 0), 0f, 0.2f, 0.2f, Color.Red);

                _boxes.Add(pBox);
            }

            for (int i = 0; i < _boxes.Count; i++)
            {
                Vector2 boxP = _boxes[i].Position;

                if (boxP.X < 0 || boxP.X > Engine.RenderWidth
                    || boxP.Y > Engine.RenderHeight)
                {
                    PhysicsBox b = _boxes[i];
                    _boxes.Remove(_boxes[i]);
                    b.Dispose();
                    removeEntity(_boxes[i]);
                }
            }

            _physicsWorld.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the physics test screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="sBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            

            base.Draw(gameTime, sBatch);
        }
    }
}
