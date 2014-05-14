using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotShootans.Engine;
using Microsoft.Xna.Framework;

namespace RobotShootans.Entities
{
    /// <summary>The type of weapon</summary>
    public enum WeaponType
    {
        /// <summary>Pistol</summary>
        PISTOL,
        /// <summary>Machine Gune</summary>
        MACHINE_GUN,
        /// <summary>Shotung</summary>
        SHOTGUN,
        /// <summary>Rocket Launcher</summary>
        ROCKET_LAUNCHER
    }

    /// <summary>
    /// A weapon class used to create bullets
    /// </summary>
    public class Weapon : GameEntity
    {
        WeaponType _weaponType;
        /// <summary>Publicly readable weapon type</summary>
        public string Type { get { return _weaponType.ToString(); } }

        int _fireRateCounter;

        /// <summary>The constructor for the weapon</summary>
        /// <param name="weaponTypeIn"></param>
        public Weapon(WeaponType weaponTypeIn = WeaponType.PISTOL)
            : base("WEAPON")
        {
            _weaponType = weaponTypeIn;
            _fireRateCounter = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Load()
        {
            _loaded = true;
        }

        /// <summary>If the fire rate has reset, shoots the weapon</summary>
        public void shoot(Vector2 positionIn, float bearingIn)
        {
            switch(_weaponType)
            {
                case WeaponType.PISTOL:
                    if(_fireRateCounter >= 1000)
                    {
                        Screen.addEntity(new Bullet(positionIn, 5, Screen.Engine.RenderHeight / 2.0f, bearingIn, Color.Yellow));
                        _fireRateCounter = 0;
                    }
                    break;
                case WeaponType.MACHINE_GUN:
                    if (_fireRateCounter >= 100)
                    {
                        Screen.addEntity(new Bullet(positionIn, 5, Screen.Engine.RenderHeight / 2.0f, bearingIn, Color.Red));
                        _fireRateCounter = 0;
                    }
                    break;
                case WeaponType.SHOTGUN:
                    if (_fireRateCounter >= 2000)
                    {
                        float spread = 0.130f;
                        Screen.addEntity(new Bullet(positionIn, 6, Screen.Engine.RenderHeight / 2.0f, bearingIn - spread * 1.5f, Color.Black));
                        Screen.addEntity(new Bullet(positionIn, 6, Screen.Engine.RenderHeight / 2.0f, bearingIn - spread, Color.Black));
                        Screen.addEntity(new Bullet(positionIn, 6, Screen.Engine.RenderHeight / 2.0f, bearingIn - spread * 0.5f, Color.Black));
                        Screen.addEntity(new Bullet(positionIn, 6, Screen.Engine.RenderHeight / 2.0f, bearingIn, Color.Black));
                        Screen.addEntity(new Bullet(positionIn, 6, Screen.Engine.RenderHeight / 2.0f, bearingIn + spread * 0.5f, Color.Black));
                        Screen.addEntity(new Bullet(positionIn, 6, Screen.Engine.RenderHeight / 2.0f, bearingIn + spread, Color.Black));
                        Screen.addEntity(new Bullet(positionIn, 6, Screen.Engine.RenderHeight / 2.0f, bearingIn + spread * 1.5f, Color.Black));
                        _fireRateCounter = 0;
                    }
                    break;
                case WeaponType.ROCKET_LAUNCHER:
                    if (_fireRateCounter >= 1000)
                    {
                        // Make a rocket (seperate entity)
                        _fireRateCounter = 0;
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _fireRateCounter += gameTime.ElapsedGameTime.Milliseconds;
        }
    }
}
