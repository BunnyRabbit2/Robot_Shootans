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
    public class PhysicsTestScreen : GameScreen
    {
        Body _ground;
        Body _platform1, _platform2;
        List<ColouredRectangle> _displayBoxes;
        List<Body> _boxes;

        ColouredRectangle _groundDisp, _plat1Disp, _plat2Disp;

        public World _physicsWorld;
        public World PhysicsWorld { get { return _physicsWorld; } }

        public PhysicsTestScreen(bool blockUpdating = false)
            : base(blockUpdating)
        {
            _screenName = "PHYSICS TEST SCREEN";
        }
        
        public override void loadGameScreen()
        {
            _physicsWorld = new World(new Vector2(0f, 10f));

            _boxes = new List<Body>();
            _displayBoxes = new List<ColouredRectangle>();

            addEntity(new ColouredRectangle(new Rectangle(0, 0, 1920, 1080), Color.DarkGray));

            _ground = BodyFactory.CreateRectangle(_physicsWorld, ConvertUnits.ToSimUnits(Engine.RenderWidth * 2), ConvertUnits.ToSimUnits(50), 10f);
            _ground.Position = ConvertUnits.ToSimUnits(Engine.RenderWidth / 2, Engine.RenderHeight-50);
            _ground.IsStatic = true;
            _ground.Restitution = 0.2f;
            _ground.Friction = 0.2f;
            _groundDisp = new ColouredRectangle(new Rectangle(0, 0, Engine.RenderWidth, 50), Color.Yellow);
            _groundDisp.setOrigin(OriginPosition.CENTER);

            addEntity(_groundDisp);

            _platform1 = BodyFactory.CreateRectangle(_physicsWorld, ConvertUnits.ToSimUnits(500), ConvertUnits.ToSimUnits(50), 10f);
            _platform1.Position = ConvertUnits.ToSimUnits(Engine.RenderWidth * 0.33, Engine.RenderHeight *0.5);
            _platform1.IsStatic = true;
            _platform1.Restitution = 0.2f;
            _platform1.Friction = 0.2f;
            _platform1.Rotation = MathHelper.ToRadians(45f);
            _plat1Disp = new ColouredRectangle(new Rectangle(0, 0, 500, 50), Color.Yellow);
            _plat1Disp.setOrigin(OriginPosition.CENTER);

            addEntity(_plat1Disp);

            _platform2 = BodyFactory.CreateRectangle(_physicsWorld, ConvertUnits.ToSimUnits(500), ConvertUnits.ToSimUnits(50), 10f);
            _platform2.Position = ConvertUnits.ToSimUnits(Engine.RenderWidth * 0.66, Engine.RenderHeight * 0.5);
            _platform2.IsStatic = true;
            _platform2.Restitution = 0.2f;
            _platform2.Friction = 0.2f;
            _platform2.Rotation = MathHelper.ToRadians(-45f);
            _plat2Disp = new ColouredRectangle(new Rectangle(0, 0, 500, 50), Color.Yellow);
            _plat2Disp.setOrigin(OriginPosition.CENTER);

            addEntity(_plat2Disp);

            _groundDisp.Position = ConvertUnits.ToDisplayUnits(_ground.Position);
            _groundDisp.setRotation(_ground.Rotation);
            _plat1Disp.Position = ConvertUnits.ToDisplayUnits(_platform1.Position);
            _plat1Disp.setRotation(_platform1.Rotation);
            _plat2Disp.Position = ConvertUnits.ToDisplayUnits(_platform2.Position);
            _plat2Disp.setRotation(_platform2.Rotation);

            _loaded = true;
        }

        public override void unloadGameScreen()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if(InputHelper.isKeyPressNew(Keys.Space))
            {
                Random random = new Random();
                int width = random.Next(20, 100);
                int height = random.Next(20, 100);

                width = 50;
                height = 50;

                Body body = BodyFactory.CreateRectangle(_physicsWorld,
                    ConvertUnits.ToSimUnits(width),
                    ConvertUnits.ToSimUnits(height),
                    10f,
                    new Point(width, height));

                body.BodyType = BodyType.Dynamic;
                body.Restitution = 0.2f;
                body.Friction = 0.2f;

                body.Position = ConvertUnits.ToSimUnits(random.Next((int)(Engine.RenderWidth * 0.25), (int)(Engine.RenderWidth * 0.75)), 0);

                _boxes.Add(body);
                ColouredRectangle dispRect = 
                    new ColouredRectangle(
                        new Rectangle(
                                (int)ConvertUnits.ToDisplayUnits(body.Position.X),
                                (int)ConvertUnits.ToDisplayUnits(body.Position.Y),
                                width,
                                height),
                            Color.Red);
                dispRect.setOrigin(OriginPosition.CENTER);
                addEntity(dispRect);

                _displayBoxes.Add(dispRect);
            }

            for (int i = 0; i < _displayBoxes.Count; i++)
            {
                _displayBoxes[i].Position = ConvertUnits.ToDisplayUnits(_boxes[i].Position);
                //_displayBoxes[i].Position = ConvertUnits.ToDisplayUnits(_boxes[i].Position);
                _displayBoxes[i].setRotation(_boxes[i].Rotation);
            }

            _physicsWorld.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            

            base.Draw(gameTime, sBatch);
        }
    }
}
