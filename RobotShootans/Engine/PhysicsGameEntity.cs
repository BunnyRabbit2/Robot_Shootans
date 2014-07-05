using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Engine
{
    public class PhysicsGameEntity : GameEntity
    {
        protected Body _physicsBody;

        public Body PhysicsBody { get { return _physicsBody; } }

        public Vector2 SimPos { get { return _physicsBody.Position; } }

        public PhysicsGameEntity(string entityName)
            : base (entityName)
        {

        }
    }
}
