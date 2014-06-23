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

        /// <summary>The position of the box in the physics simulation</summary>
        public Vector2 SimPos { get { return _physicsBody.Position; } }
        /// <summary>The position of the box on the display</summary>
        public Vector2 Position { get { return new Vector2(_displayRect.X, _displayRect.Y); } }

#if DEBUG
        // Surrounding this until I figure out a better way of doing this
        // Hopefully this will force me to check it before compiling as Release
        public Body Body { get { return _physicsBody; } }
#endif

        /// <summary>
        /// Creates the physics box
        /// </summary>
        public PhysicsBox()
            : base ("PHYSICS_BOX")
        {
            
        }

        /// <summary>
        /// Loads the physics box. Does not actually load anything
        /// </summary>
        public override void Load()
        {
            
        }

        /// <summary>
        /// Sets up the box to display
        /// </summary>
        /// <param name="worldIn">The physics world to associate the box with</param>
        /// <param name="width">The width of the box</param>
        /// <param name="height">The height of the box</param>
        /// <param name="isStaticIn">Whether the box is static or not</param>
        /// <param name="position">The starting position of the box</param>
        /// <param name="rotationIn">The starting rotation of the box</param>
        /// <param name="restitution">The restitution of the box</param>
        /// <param name="friction">The friction of the box</param>
        /// <param name="colorIn">The colour of the box</param>
        /// <param name="originIn">The origin of the box</param>
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

        /// <summary>
        /// Updates the position of the display box to be the same ad the sim box
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _displayRect.Position = ConvertUnits.ToDisplayUnits(_physicsBody.Position);
            _displayRect.setRotation(_physicsBody.Rotation);
        }

        public override void Unload()
        {
            _physicsBody.Dispose();
        }
    }
}
