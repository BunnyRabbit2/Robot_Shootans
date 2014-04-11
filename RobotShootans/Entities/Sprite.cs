using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobotShootans.Engine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;

namespace RobotShootans.Entities
{
    /// <summary>
    /// An displayable image with some retrievable information
    /// </summary>
    public class Sprite : GameEntity
    {
        Texture2D _image;
        Color _color;

        /// <summary>
        /// The position of the image
        /// </summary>
        public Vector2 Position;

        private Vector2 _origin;

        /// <summary>
        /// Creates the Sprite object
        /// </summary>
        public Sprite()
            : base("SPRITE")
        {
            _color = Color.White;
        }

        /// <summary>
        /// Sets the image of the Sprite
        /// </summary>
        /// <param name="imageLocation">The location of the image on the drive</param>
        public void setImage(string imageLocation)
        {
            if (File.Exists(imageLocation + ".xnb"))
                _image = Screen.Engine.Content.Load<Texture2D>(imageLocation);
            else
                LogFile.LogStringLine("Failed to load image: " + imageLocation + " into sprite. Nothing loaded", LogType.ERROR);
        }

        /// <summary>
        /// Sets the color of the sprite
        /// </summary>
        /// <param name="colorIn"></param>
        public void setColor(Color colorIn)
        {
            _color = colorIn;
        }
    }
}
