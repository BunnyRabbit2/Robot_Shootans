using Microsoft.Xna.Framework;

namespace RobotShootans.Entities.Weapons
{
    /// <summary>Pistol weapon class. Start weapon</summary>
    public class Pistol : Weapon
    {
        /// <summary>Constructor for the pistol class</summary>
        public Pistol() : base ()
        {
            _weaponType = "PISTOL";
            _fireRate = 1000;

            _ammo = -1;
        }

        /// <summary>If the fire rate has reset, shoots the weapon</summary>
        public override void shoot(Vector2 positionIn, float bearingIn)
        {
            if (_fireRateCounter >= _fireRate)
            {
                Screen.addEntity(new Bullet(positionIn, 5, Screen.Engine.RenderHeight / 2.0f, bearingIn, Color.Yellow));
                _fireRateCounter = 0;

                _ammo--;
            }
        }
    }
}
