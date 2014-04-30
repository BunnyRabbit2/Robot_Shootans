using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RobotShootans.Engine
{
    /// <summary>
    /// A class for handling animation frames
    /// </summary>
    public class Animation
    {
        private string _animationName;
        /// <summary>The name of the animation</summary>
        public string Name { get { return _animationName; } }

        private int _frameTime;
        private Rectangle[] _frames;
        // possibly replace this later with stuff using GameTime instead
        private Stopwatch _timer;

        bool _isRunning;
        /// <summary>Returns if the animation is running or not</summary>
        public bool Running { get { return _isRunning; } }

        /// <summary>
        /// Creates the animation with values set so it can be used
        /// </summary>
        /// <param name="nameIn"></param>
        /// <param name="frameTimeIn"></param>
        /// <param name="framesIn"></param>
        public Animation(string nameIn, int frameTimeIn, Rectangle[] framesIn)
        {
            _animationName = nameIn;
            _frameTime = frameTimeIn;
            _frames = framesIn;
            _timer = new Stopwatch();
            _isRunning = false;
        }

        /// <summary>Starts the timer on the animation</summary>
        public void startAnimation()
        {
            _timer.Restart();
            _isRunning = true;
        }

        /// <summary>Stops the animation</summary>
        public void stopAnimation()
        {
            _timer.Reset();
            _isRunning = false;
        }

        /// <summary>
        /// Gets the current frame in the animation
        /// </summary>
        /// <returns></returns>
        public Rectangle getCurrentframe()
        {
            int frameNumber = (int)_timer.ElapsedMilliseconds / _frameTime;

            if(frameNumber >= _frames.Length)
            {
                frameNumber = _frames.Length - 1;
                _timer.Restart();
            }

            return _frames[frameNumber];
        }
    }
}
