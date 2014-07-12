using Microsoft.Xna.Framework;
using RobotShootans.Engine;

namespace RobotShootans.Entities.Weapons
{
    /// <summary>Machine Gun weapon class. Rapid firing</summary>
    public class MachineGun : Weapon
    {
        /// <summary>Constructor for the machine gun weapon</summary>
        public MachineGun()
            : base()
        {
            _weaponType = "MACHINE GUN";
            _fireRate = 100;

            _speed = 40f;

            _ammo = 100;

            _bulletSize = 10;
        }

        /// <summary>If the fire rate has reset, shoots the weapon</summary>
        public override void shoot(Vector2 positionIn, float bearingIn)
        {
            if (_fireRateCounter >= _fireRate)
            {
                Screen.addEntity(new Bullet(positionIn, _bulletSize, _speed, bearingIn, Color.Red));
                _fireRateCounter = 0;

                _ammo--;

                Screen.Engine.registerEvent(new GameEvent(EventType.AMMO_CHANGED, -1));
            }
        }
    }
}
