using Microsoft.Xna.Framework;
using RobotShootans.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Entities
{
    /// <summary>
    /// An explosion. It explodes. Fire and stuff.
    /// </summary>
    public class Explosion : GameEntity
    {
        Vector2 _position;
        Sprite _displaySprite;
        int _explosionType; // 1 = large, 2 = medium, 3 = small

        int _explodeTimer = 0;
        int _frames = 0;
        int _frameTimer = 0;

        float _scale;

        int _expSize;
        public int ExpSize { get { return _expSize * (int)_scale; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionIn"></param>
        /// <param name="typeIn">The type of explosion to make. 1 = large, 2 = medium, 3 = small</param>
        /// <param name="scaleIn"></param>
        public Explosion(Vector2 positionIn, int typeIn, float scaleIn = 1.0f)
            : base("EXPLOSION")
        {
            if (typeIn < 1 || typeIn > 3)
                typeIn = 1;

            _explosionType = typeIn;
            _scale = scaleIn;

            _position = positionIn;

            _displaySprite = new Sprite();
        }

        /// <summary>
        /// Loads the explosion with the correct explosion texture and sets a physics body up
        /// </summary>
        public override void Load()
        {
            Screen.addEntity(_displaySprite);
            _displaySprite.setImage("game/explosion3");
            _displaySprite.Position = _position;
            _displaySprite.DrawOrder = 5;
            setAnimation();
            _displaySprite.setScale(_scale);
            _displaySprite.Animation = "EXPLODE";
            
            _loaded = true;
        }

        /// <summary>
        /// Disposes of all the stuff it needs to
        /// </summary>
        public override void Unload()
        {
            Screen.removeEntity(_displaySprite);
        }

        /// <summary>
        /// Removes the explosion once it's done exploding
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            _explodeTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (_explodeTimer > _frameTimer * _frames)
                Screen.removeEntity(this);
        }

        private void setAnimation()
        {
            if(_explosionType == 1)
            {
                _expSize = 92;
                _frames = 10;
                _frameTimer = 100;

                _displaySprite.addAnimation("EXPLODE", _frameTimer,
                    new Rectangle[]
                    {
                        new Rectangle(0,0,_expSize,_expSize),
                        new Rectangle(_expSize,0,_expSize,_expSize),
                        new Rectangle(_expSize*2,0,_expSize,_expSize),
                        new Rectangle(_expSize*3,0,_expSize,_expSize),
                        new Rectangle(_expSize*4,0,_expSize,_expSize),
                        new Rectangle(0,_expSize,_expSize,_expSize),
                        new Rectangle(_expSize,_expSize,_expSize,_expSize),
                        new Rectangle(_expSize*2,_expSize,_expSize,_expSize),
                        new Rectangle(_expSize*3,_expSize,_expSize,_expSize),
                        new Rectangle(_expSize*4,_expSize,_expSize,_expSize),
                    });

                _displaySprite.setOrigin(new Vector2(_expSize/2));
            }
            else if(_explosionType == 2)
            {
                _expSize = 60;
                int vOffset = 92*2;

                _frames = 10;
                _frameTimer = 100;

                _displaySprite.addAnimation("EXPLODE", _frameTimer,
                    new Rectangle[]
                    {
                        new Rectangle(0,vOffset,_expSize,_expSize),
                        new Rectangle(_expSize,vOffset,_expSize,_expSize),
                        new Rectangle(_expSize*2,vOffset,_expSize,_expSize),
                        new Rectangle(_expSize*3,vOffset,_expSize,_expSize),
                        new Rectangle(_expSize*4,vOffset,_expSize,_expSize),
                        new Rectangle(0,vOffset+_expSize,_expSize,_expSize),
                        new Rectangle(_expSize,vOffset+_expSize,_expSize,_expSize),
                        new Rectangle(_expSize*2,vOffset+_expSize,_expSize,_expSize),
                        new Rectangle(_expSize*3,vOffset+_expSize,_expSize,_expSize),
                        new Rectangle(_expSize*4,vOffset+_expSize,_expSize,_expSize),
                    });

                _displaySprite.setOrigin(new Vector2(_expSize / 2));
            }
            else if(_explosionType == 3)
            {
                _expSize = 50;
                int vOffset = 92 * 2 + 60 * 2;

                _frames = 5;
                _frameTimer = 100;
                
                _displaySprite.addAnimation("EXPLODE", _frameTimer,
                    new Rectangle[]
                    {
                        new Rectangle(0,vOffset,_expSize,_expSize),
                        new Rectangle(_expSize,vOffset,_expSize,_expSize),
                        new Rectangle(_expSize*2,vOffset,_expSize,_expSize),
                        new Rectangle(_expSize*3,vOffset,_expSize,_expSize),
                        new Rectangle(_expSize*4,vOffset,_expSize,_expSize)
                    });

                _displaySprite.setOrigin(new Vector2(_expSize / 2));
            }
        }
    }
}
