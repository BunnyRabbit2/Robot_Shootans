using Microsoft.Xna.Framework;
using RobotShootans.Engine;
using System;
using System.Collections.Generic;

namespace RobotShootans.Entities
{
    /// <summary>
    /// Spawns power ups at given intervals
    /// </summary>
    public class PowerUpSpawner : GameEntity
    {
        /// <summary>
        /// The position of the player
        /// </summary>
        public Vector2 PlayerPosition;

        int _spawnRate;
        int _spawnTimer;
        float _minSpawnDistance;

        Random rand = new Random();

        List<PowerUp> _powerUps;

        /// <summary>
        /// Constructor for the Power Up Spawner
        /// </summary>
        public PowerUpSpawner() : base ("POWER_UP_SPAWNER")
        {
            _spawnRate = 10000; // Ten seconds
            _spawnTimer = 0;

            _powerUps = new List<PowerUp>();
        }

        /// <summary></summary>
        public override void Load()
        {
            _minSpawnDistance = (float)(Screen.Engine.RenderHeight * 0.1);

            _loaded = true;
        }

        /// <summary></summary>
        public override void Unload()
        {
            List<GameEntity> powerUps = Screen.getEntityByName("POWER_UP");
            foreach (GameEntity p in powerUps)
            {
                Screen.removeEntity(p);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventIn"></param>
        /// <returns></returns>
        public override bool HandleEvent(GameEvent eventIn)
        {
            if(eventIn.EventType == EventType.CREATE_LIFE_PU)
            {
                createPowerUp(PowerUpType.LIFE);
            }

            return false;
        }

        /// <summary>Updates the spawner and places robots</summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _spawnTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (_spawnTimer > _spawnRate)
            {
                int pu = rand.Next(1, 4);
                
                if (pu == 1)
                    createPowerUp(PowerUpType.SHIELD);
                else if (pu == 2)
                    createPowerUp(PowerUpType.MACHINEGUN);
                else if (pu == 3)
                    createPowerUp(PowerUpType.SHOTGUN);
                else if (pu == 4)
                    createPowerUp(PowerUpType.ROCKET_LAUNCHER);

                _spawnTimer = 0;
            }

            // Checks if any robots are destroyed and cleans the list of nulls
            for (int i = 0; i < _powerUps.Count; i++)
            {
                if (_powerUps[i] == null)
                {
                    _powerUps.RemoveAt(i);
                    i--;
                }
            }
        }

        private void createPowerUp(PowerUpType typeIn)
        {
            // bit of a funky way to do it but should work
            Vector2 position = new Vector2(rand.Next(0, Screen.Engine.RenderWidth), rand.Next(0, Screen.Engine.RenderHeight));

            while (HelperFunctions.GetDistanceBetweenTwoPoints(position, PlayerPosition) < _minSpawnDistance)
            {
                position = new Vector2(rand.Next(0, Screen.Engine.RenderWidth), rand.Next(0, Screen.Engine.RenderHeight));
            }

            PowerUp p = new PowerUp(position, typeIn);
            Screen.addEntity(p);
            _powerUps.Add(p);
        }
    }
}
