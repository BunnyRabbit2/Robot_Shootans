﻿using Microsoft.Xna.Framework;
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

        int _spawnRate;
        int _spawnTimer;
        float _minSpawnDistance;

        Random rand = new Random();

        List<Robot> _robots;

        /// <summary>Creates the robot spawner and sets a few things</summary>
        public RobotSpawner() : base ("ROBOT_SPAWNER")
        {
            _spawnRate = 2000;
            _spawnTimer = 0;
            
            _robots = new List<Robot>();
        }

        /// <summary></summary>
        public override void Load()
        {
            _minSpawnDistance = (float)(Screen.Engine.RenderHeight * 0.25);

            _loaded = true;
        }

        /// <summary>Updates the spawner and places robots</summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _spawnTimer += gameTime.ElapsedGameTime.Milliseconds;

            if(_spawnTimer > _spawnRate)
            {
                // bit of a funky way to do it but should work
                Vector2 position = new Vector2(rand.Next(0, Screen.Engine.RenderWidth), rand.Next(0, Screen.Engine.RenderHeight));

                while(HelperFunctions.GetDistanceBetweenTwoPoints(position, playerPosition) < _minSpawnDistance)
                {
                    position = new Vector2(rand.Next(0, Screen.Engine.RenderWidth), rand.Next(0, Screen.Engine.RenderHeight));
                }

                Robot newRobot = new Robot(position, this);
                Screen.addEntity(newRobot);
                _robots.Add(newRobot);

                _spawnTimer = 0;
            }
        }
    }
}
