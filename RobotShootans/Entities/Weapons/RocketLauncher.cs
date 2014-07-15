using Microsoft.Xna.Framework;

namespace RobotShootans.Entities.Weapons
{
    /// <summary>Rocket Launcher weapon class. Add rocket entities</summary>
    public class RocketLauncher : Weapon
    {
        /// <summary>Constructor for the rocket launcher weapon</summary>
        public RocketLauncher()
            : base()
        {
            _weaponType = "ROCKET LAUNCHER";
            _fireRate = 1000;

            _ammo = 10;

            _shootSoundToLoad = "RL_shoot";
        }

        /// <summary>If the fire rate has reset, shoots the weapon</summary>
        public override void shoot(Vector2 positionIn, float bearingIn)
        {
            if (_fireRateCounter >= _fireRate)
            {
                // Make a rocket
                _fireRateCounter = 0;

                _ammo--;

                _shootSound.Play();
            }
        }
    }
}
