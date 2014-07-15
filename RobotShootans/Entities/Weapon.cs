using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        /// <summary>the size of the bullet</summary>
        protected int _bulletSize;

        /// <summary>Set so that the correct sound is loaded by the default load function</summary>
        protected string _shootSoundToLoad;
        /// <summary>The sound to play when the weapon shoots</summary>
        protected SoundEffect _shootSound;

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
            if (!string.IsNullOrEmpty(_shootSoundToLoad))
                _shootSound = Screen.Engine.loadSound(_shootSoundToLoad);

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
