using Microsoft.Xna.Framework;
using RobotShootans.Engine;

namespace RobotShootans.Entities
{
    /// <summary>Robot class. Might make abstract</summary>
    public class Robot : GameEntity
    {
        ColouredRectangle _displayRect;
        RobotSpawner _spawner;
        float _speed;


        /// <summary>Creates the robot and sets the start position</summary>
        /// <param name="positionIn"></param>
        /// /// <param name="spawnerIn"></param>
        public Robot(Vector2 positionIn, RobotSpawner spawnerIn) : base ("ROBOT")
        {
            _spawner = spawnerIn;
            _displayRect = new ColouredRectangle(new Rectangle((int)positionIn.X, (int)positionIn.Y, 20, 20), Color.Red);
            _displayRect.setOrigin(OriginPosition.CENTER);
        }

        /// <summary></summary>
        public override void Load()
        {
            Screen.addEntity(_displayRect);
            _speed = Screen.Engine.RenderHeight / 4.0f;

            _loaded = true;
        }

        /// <summary>Updates the robot and sends it towars the player</summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            float deltaSpeed = _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 pos1 = _displayRect.Position;
            Vector2 pos2 = _spawner.playerPosition;
            Vector2 velocity = Vector2.Zero;

            if (pos1 != pos2)
            {

                float dist = (float)HelperFunctions.GetDistanceBetweenTwoPoints(pos1, pos2);
                float distX = pos1.X - pos2.X;
                float distY = pos1.Y - pos2.Y;

                float multi = deltaSpeed / dist;

                velocity = new Vector2(multi * distX, multi * distY);
            }

            _displayRect.Position -= velocity;
        }
    }
}
