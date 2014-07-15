using Microsoft.Xna.Framework;
using RobotShootans.Engine;

namespace RobotShootans.Entities.Weapons
{
    /// <summary>Shotgun weapon class. Shoots a spread of bullets</summary>
    public class Shotgun : Weapon
    {
        /// <summary>Constructor for the shotgun weapon</summary>
        public Shotgun()
            : base()
        {
            _weaponType = "SHOTGUN";
            _fireRate = 2000;

            _speed = 40f;

            _ammo = 10;

            _bulletSize = 10;

            _shootSoundToLoad = "SG_shoot";
        }

        /// <summary>If the fire rate has reset, shoots the weapon</summary>
        public override void shoot(Vector2 positionIn, float bearingIn)
        {
            if (_fireRateCounter >= _fireRate)
            {
                float spread = 0.130f;
                Screen.addEntity(new Bullet(positionIn, _bulletSize, _speed, bearingIn - spread * 1.5f, Color.Green));
                Screen.addEntity(new Bullet(positionIn, _bulletSize, _speed, bearingIn - spread, Color.Green));
                Screen.addEntity(new Bullet(positionIn, _bulletSize, _speed, bearingIn - spread * 0.5f, Color.Green));
                Screen.addEntity(new Bullet(positionIn, _bulletSize, _speed, bearingIn, Color.Green));
                Screen.addEntity(new Bullet(positionIn, _bulletSize, _speed, bearingIn + spread * 0.5f, Color.Green));
                Screen.addEntity(new Bullet(positionIn, _bulletSize, _speed, bearingIn + spread, Color.Green));
                Screen.addEntity(new Bullet(positionIn, _bulletSize, _speed, bearingIn + spread * 1.5f, Color.Green));
                _fireRateCounter = 0;

                _ammo--;
                Screen.Engine.registerEvent(new GameEvent(EventType.AMMO_CHANGED, -1));

                _shootSound.Play();
            }
        }
    }
}
