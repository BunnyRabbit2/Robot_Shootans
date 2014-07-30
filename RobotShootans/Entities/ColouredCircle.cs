﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RobotShootans.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Entities
{
    public class ColouredCircle : GameEntity
    {
        Texture2D _texture;
        Vector2 _position;

        /// <summary>
        /// The X position of the Rectangle
        /// </summary>
        public float X { get { return _position.X; } set { _position.X = value; } }
        /// <summary>
        /// The Y position of the Rectangle
        /// </summary>
        public float Y { get { return _position.Y; } set { _position.Y = value; } }
        /// <summary>
        /// The position of the Rectangle
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

        int _size;
        public int Size { get { return _size; } }

        /// <summary>The origin of the image</summary>
        protected Vector2 _origin;

        /// <summary>
        /// Sets the origin to draw the image from
        /// </summary>
        public void setOrigin(OriginPosition originIn)
        {
            switch (originIn)
            {
                case OriginPosition.TOPLEFT:
                    _origin = new Vector2(0f, 0f);
                    break;
                case OriginPosition.TOPRIGHT:
                    _origin = new Vector2(_size*2, 0f);
                    break;
                case OriginPosition.BOTTOMLEFT:
                    _origin = new Vector2(0f, _size*2);
                    break;
                case OriginPosition.BOTTOMRIGHT:
                    _origin = new Vector2(_size*2, _size*2);
                    break;
                case OriginPosition.CENTER:
                    _origin = new Vector2(_size, _size);
                    break;
            }
        }

        Color _color;
        /// <summary>
        /// The alpha of the rectangle
        /// </summary>
        public int Alpha
        {
            get { return _color.A; }
            set
            {
                _color.A = (byte)MathHelper.Clamp(value, 0, 255);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionIn"></param>
        /// <param name="sizeIn">radius of the circle</param>
        /// <param name="colourIn"></param>
        public ColouredCircle(Vector2 positionIn, int sizeIn, Color colourIn)
            : base ("COLOURED_CIRCLE")
        {
            _position = positionIn;
            _size = sizeIn;
            _color = colourIn;
        }

        public override void Load()
        {
            int outerRadius = _size * 2 + 2; // So circle doesn't go out of bounds
            _texture = new Texture2D(Screen.Engine.Graphics, outerRadius, outerRadius);

            Color[] data = new Color[outerRadius * outerRadius];

            // Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / _size;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                int x = (int)Math.Round(_size + _size * Math.Cos(angle));
                int y = (int)Math.Round(_size + _size * Math.Sin(angle));

                data[y * outerRadius + x + 1] = Color.White;
            }

            _origin = new Vector2(_size, _size);

            _texture.SetData(data);

            _loaded = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch sBatch)
        {
            sBatch.Draw(texture: _texture, position: _position, color: _color, origin: _origin);
        }
    }
}
