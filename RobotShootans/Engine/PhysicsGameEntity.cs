using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Engine
{
    /// <summary>
    /// A GameEntity with a physics body
    /// </summary>
    public class PhysicsGameEntity : GameEntity
    {
        /// <summary>
        /// The physics body of the entity
        /// </summary>
        protected Body _physicsBody;

        /// <summary>
        /// The physics body of the entity
        /// </summary>
        public Body PhysicsBody { get { return _physicsBody; } }

        /// <summary>
        /// The position of the body in the simulation
        /// </summary>
        public Vector2 SimPos { get { return _physicsBody.Position; } }

        /// <summary>
        /// Constructor for the entity
        /// </summary>
        /// <param name="entityName"></param>
        public PhysicsGameEntity(string entityName)
            : base (entityName)
        {

        }
    }
}
