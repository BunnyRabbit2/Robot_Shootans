using Microsoft.Xna.Framework;
using RobotShootans.Engine;

namespace RobotShootans.Entities.Weapons
{
    /// <summary>Pistol weapon class. Start weapon</summary>
    public class Pistol : Weapon
    {
        /// <summary>Constructor for the pistol class</summary>
        public Pistol() : base ()
        {
            _weaponType = "PISTOL";
            _fireRate = 500;

            _speed = 40f;

            _ammo = -1;

            _bulletSize = 10;

            _shootSoundToLoad = "P_shoot";
        }

        /// <summary>If the fire rate has reset, shoots the weapon</summary>
        public override void shoot(Vector2 positionIn, float bearingIn)
        {
            if (_fireRateCounter >= _fireRate)
            {
                Screen.addEntity(new Bullet(positionIn, _bulletSize, _speed, bearingIn, Color.Yellow));
                _fireRateCounter = 0;

                Screen.Engine.registerEvent(new GameEvent(EventType.SET_AMMO, -1));

                _shootSound.Play();
            }
        }
    }
}
