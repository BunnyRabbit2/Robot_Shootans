using Microsoft.Xna.Framework;

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

            _ammo = 25;
        }

        /// <summary>If the fire rate has reset, shoots the weapon</summary>
        public override void shoot(Vector2 positionIn, float bearingIn)
        {
            if (_fireRateCounter >= _fireRate)
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

                _ammo--;
            }
        }
    }
}
