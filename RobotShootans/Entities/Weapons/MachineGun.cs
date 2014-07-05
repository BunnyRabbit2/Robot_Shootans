using Microsoft.Xna.Framework;

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

            _speed = 5f;

            _ammo = 100;
        }

        /// <summary>If the fire rate has reset, shoots the weapon</summary>
        public override void shoot(Vector2 positionIn, float bearingIn)
        {
            if (_fireRateCounter >= _fireRate)
            {
                Screen.addEntity(new Bullet(positionIn, 5, _speed, bearingIn, Color.Red));
                _fireRateCounter = 0;

                _ammo--;
            }
        }
    }
}
