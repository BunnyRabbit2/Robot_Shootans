﻿using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using RobotShootans.Engine;
using RobotShootans.Entities.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Entities
{
    /// <summary>
    /// The type of power up
    /// </summary>
    public enum PowerUpType
    {
        /// <summary>A shield that will absorb the next robot strike</summary>
        SHIELD,
        /// <summary>A fast firing weapon</summary>
        MACHINEGUN,
        /// <summary>A slow firing weapon that shoots a spread of bullets</summary>
        SHOTGUN,
        /// <summary>Fires a large round that explodes on impact</summary>
        ROCKET_LAUNCHER,
        /// <summary>Gains the player a life</summary>
        LIFE,
        /// <summary>Used for the event when wanting a random power up</summary>
        RANDOM
    }

    /// <summary>
    /// The class all power ups inherit from
    /// </summary>
    public class PowerUp : PhysicsGameEntity
    {
        // Needs to be set to ONLY collide with the player

        Vector2 _position;
        Sprite _displayImage;
        int _size;

        PowerUpType _type;

        SoundEffect _collectSound;

        /// <summary>
        /// Constructor for the Power Up
        /// </summary>
        /// <param name="positionIn"></param>
        /// <param name="typeIn"></param>
        /// <param name="sizeIn"></param>
        public PowerUp(Vector2 positionIn, PowerUpType typeIn, int sizeIn = 20)
            : base ("POWER_UP")
        {
            _type = typeIn;
            _position = positionIn;
            _size = sizeIn;
        }

        /// <summary>
        /// Sets up the physics body and the sprite for displaying the power up
        /// </summary>
        public override void Load()
        {
            _physicsBody = BodyFactory.CreateCircle(Screen.PhysicsWorld, ConvertUnits.ToSimUnits(_size), 10f, "POWER_UP");
            _physicsBody.UserData = "POWER_UP";
            _physicsBody.Position = ConvertUnits.ToSimUnits(_position);
            _physicsBody.Restitution = 0.0f;
            _physicsBody.Friction = 0.2f;
            _physicsBody.IsStatic = false;
            _physicsBody.OnCollision += onCollision;

            _displayImage = new Sprite();
            Screen.addEntity(_displayImage);
            _displayImage.setImage(getPowerUpTexture(), OriginPosition.CENTER);
            _displayImage.Position = _position;

            _collectSound = Screen.Engine.loadSound("Powerup_GET");

            _loaded = true;
        }

        /// <summary>
        /// Registers the collision between the power up and the player
        /// </summary>
        /// <param name="fixtureA"></param>
        /// <param name="fixtureB"></param>
        /// <param name="contact"></param>
        /// <returns></returns>
        public bool onCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            if(fixtureB.Body.UserData.ToString() != "PLAYER")
            {
                return false;
            }
            else
            {
                registerPowerUp();
                Screen.removeEntity(this);
                return false;
            }
        }

        /// <summary>
        /// Removes any entities owned by this
        /// </summary>
        public override void Unload()
        {
            _physicsBody.Dispose();
            Screen.removeEntity(_displayImage);
        }

        private void registerPowerUp()
        {
            switch(_type)
            {
                case PowerUpType.LIFE:
                    Screen.Engine.registerEvent(new GameEvent(EventType.LIFE_GAINED));
                    break;
                case PowerUpType.MACHINEGUN:
                    Screen.Engine.registerEvent(new GameEvent(EventType.WEAPON_CHANGED, new MachineGun()));
                    break;
                case PowerUpType.ROCKET_LAUNCHER:
                    Screen.Engine.registerEvent(new GameEvent(EventType.WEAPON_CHANGED, new RocketLauncher()));
                    break;
                case PowerUpType.SHIELD:
                    Screen.Engine.registerEvent(new GameEvent(EventType.SHIELD_GAINED));
                    break;
                case PowerUpType.SHOTGUN:
                    Screen.Engine.registerEvent(new GameEvent(EventType.WEAPON_CHANGED, new Shotgun()));
                    break;
            }
        }

        private string getPowerUpTexture()
        {
            string _texReturn = "";

            switch (_type)
            {
                case PowerUpType.LIFE:
                    _texReturn = "game/PU_life";
                    break;
                case PowerUpType.MACHINEGUN:
                    _texReturn = "game/PU_MachineGun";
                    break;
                case PowerUpType.ROCKET_LAUNCHER:
                    _texReturn = "game/PU_RocketLauncher";
                    break;
                case PowerUpType.SHIELD:
                    _texReturn = "game/PU_Shield";
                    break;
                case PowerUpType.SHOTGUN:
                    _texReturn = "game/PU_Shotgun";
                    break;
            }

            return _texReturn;
        }
    }
}
