using Microsoft.Xna.Framework;
using RobotShootans.Engine;

namespace RobotShootans.Entities
{
    /// <summary>
    /// A weapon class used to create bullets
    /// </summary>
    public class Weapon : GameEntity
    {
        /// <summary>The type of weapon</summary>
        protected string _weaponType;
        /// <summary>The type of weapon</summary>
        public string Type { get { return _weaponType; } }

        /// <summary>The counter for the fire rate</summary>
        protected int _fireRateCounter;
        /// <summary>The time between shots</summary>
        protected int _fireRate;

        /// <summary>Ammo remaining in the weapon</summary>
        protected int _ammo;
        /// <summary>Ammo remaining in the weapon</summary>
        public int Ammo { get { return _ammo; } }

        /// <summary>Speed of the bullet</summary>
        protected float _speed;

        protected int _bulletSize;

        /// <summary>The constructor for the weapon</summary>
        public Weapon()
            : base("WEAPON")
        {
            _fireRateCounter = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Load()
        {
            _fireRateCounter = _fireRate;
            _loaded = true;
        }

        /// <summary>If the fire rate has reset, shoots the weapon</summary>
        public virtual void shoot(Vector2 positionIn, float bearingIn)
        {
            // BANG!
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
