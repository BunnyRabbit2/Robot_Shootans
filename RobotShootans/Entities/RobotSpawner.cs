using Microsoft.Xna.Framework;
using RobotShootans.Engine;
using System;
using System.Collections.Generic;

namespace RobotShootans.Entities
{
    /// <summary>Spawner for the robots</summary>
    public class RobotSpawner : GameEntity
    {
        /// <summary>The position of the player</summary>
        public Vector2 playerPosition;

        int[] _spawnTimers;
        int[] _spawnRates;
        int[] _minimumSpawnRates;
        int[] _robotSizes;

        float _minSpawnDistance;

        Random rand = new Random();

        List<Robot> _robots;

        /// <summary>Creates the robot spawner and sets a few things</summary>
        public RobotSpawner() : base ("ROBOT_SPAWNER")
        {
            // 0 = normal, 1 = bit, 2 = fast, 3 = explodey
            _spawnRates = new int[] { 6000, 18000, 30000, 24000 };
            _minimumSpawnRates = new int[] { 2000, 6000, 10000, 8000 };

            _spawnTimers = new int[] { 0, 0, 0, 0 };

            _robotSizes = new int[] { 25, 50, 20, 25 };
            
            _robots = new List<Robot>();
        }

        /// <summary></summary>
        public override void Load()
        {
            _minSpawnDistance = (float)(Screen.Engine.RenderHeight * 0.25);

            _loaded = true;
        }

        /// <summary></summary>
        public override void Unload()
        {
            List<GameEntity> robots = Screen.getEntityByName("ROBOT");
            foreach(GameEntity r in robots)
            {
                Screen.removeEntity(r);
            }
        }

        /// <summary>Updates the spawner and places robots</summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < 4; i++)
            {
                _spawnTimers[i] += gameTime.ElapsedGameTime.Milliseconds;

                if(_spawnTimers[i] > _spawnRates[i])
                {
                    Vector2 position = new Vector2(rand.Next(0, Screen.Engine.RenderWidth), rand.Next(0, Screen.Engine.RenderHeight));

                    while (HelperFunctions.GetDistanceBetweenTwoPoints(position, playerPosition) < _minSpawnDistance)
                    {
                        position = new Vector2(rand.Next(0, Screen.Engine.RenderWidth), rand.Next(0, Screen.Engine.RenderHeight));
                    }

                    Robot newRobot = new Robot(position, this, _robotSizes[i], i);
                    Screen.addEntity(newRobot);
                    _robots.Add(newRobot);

                    _spawnRates[i] = (int)(_spawnRates[i] * 0.95);
                    _spawnTimers[i] = 0;
                }
            }

            // Checks if any robots are destroyed and cleans the list of nulls
            for (int i = 0; i < _robots.Count; i++)
            {
                if (_robots[i] == null)
                {
                    _robots.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
