using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotShootans.Engine;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Factories;
using FarseerPhysics;

namespace RobotShootans.Entities
{
    /// <summary>
    /// A quick little entity to make creating visible physics boxes easier
    /// </summary>
    public class PhysicsBox : GameEntity
    {
        Body _physicsBody;
        ColouredRectangle _displayRect;

        public Vector2 SimPos { get { return _physicsBody.Position; } }
        public Vector2 Position { get { return new Vector2(_displayRect.X, _displayRect.Y); } }

        /// <summary>
        /// Creates the physics box
        /// </summary>
        public PhysicsBox()
            : base ("PHYSICS_BOX")
        {
            
        }

        public override void Load()
        {
            
        }

        public void SetupBox(World worldIn, int width, int height, bool isStaticIn, Vector2 position, float rotationIn, float restitution, float friction, Color colorIn, OriginPosition originIn = OriginPosition.CENTER)
        {
            if (Screen != null)
            {
                _physicsBody = BodyFactory.CreateRectangle(worldIn, ConvertUnits.ToSimUnits(width), ConvertUnits.ToSimUnits(height), 10f);
                _physicsBody.Restitution = restitution;
                _physicsBody.Friction = friction;
                _physicsBody.Position = ConvertUnits.ToSimUnits(position);
                _physicsBody.Rotation = MathHelper.ToRadians(rotationIn);
                _physicsBody.IsStatic = isStaticIn;
                _displayRect = new ColouredRectangle(new Rectangle((int)position.X, (int)position.Y, width, height), colorIn, originIn);

                Screen.addEntity(_displayRect);

                _loaded = true;
            }
            else
            {
                LogFile.LogStringLine("Failed to set up physics box due to no having screen set", LogType.ERROR);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _displayRect.Position = ConvertUnits.ToDisplayUnits(_physicsBody.Position);
            _displayRect.setRotation(_physicsBody.Rotation);
        }

        public void Dispose()
        {
            if (_physicsBody != null)
                _physicsBody.Dispose();
        }
    }
}
