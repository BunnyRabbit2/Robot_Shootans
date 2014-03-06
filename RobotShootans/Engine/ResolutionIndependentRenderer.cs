using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RobotShootans.Engine
{
    /// <summary>
    /// Sets up a viewport for drawing that is resolution independent. Will create pillars if needed
    /// </summary>
    public class ResolutionIndependentRenderer
    {
        private readonly GraphicsDevice _graphicsDevice;
        private Viewport _viewport;
        private float _ratioX;
        private float _ratioY;
        private Vector2 _virtualMousePosition = new Vector2();

        /// <summary>
        /// The background colour of the viewport
        /// </summary>
        public Color BackgroundColor = Color.Magenta;

        /// <summary>
        /// The colour of the Pillars if the Aspect Ratios don't match
        /// </summary>
        public Color PillarColor = Color.Black;

        /// <summary>
        /// Creates a renderer with default settings
        /// </summary>
        /// <param name="graphicsDevice"></param>
        public ResolutionIndependentRenderer(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            VirtualWidth = 1366;
            VirtualHeight = 768;

            ScreenWidth = 1024;
            ScreenHeight = 768;
        }

        /// <summary>The Height of the viewport rendered to</summary>
        public int VirtualHeight;
        /// <summary>The Width of the viewport rendered to</summary>
        public int VirtualWidth;

        /// <summary>The Width of the screen</summary>
        public int ScreenWidth;
        /// <summary>The Height of the screen</summary>
        public int ScreenHeight;

        /// <summary>
        /// Initialises the renderer and creates the viewport
        /// </summary>
        public void Initialize()
        {
            SetupVirtualScreenViewport();

            _ratioX = (float)_viewport.Width / VirtualWidth;
            _ratioY = (float)_viewport.Height / VirtualHeight;

            _dirtyMatrix = true;
        }

        /// <summary>
        /// Sets up the viewport that will be rendered to
        /// </summary>
        public void SetupVirtualScreenViewport()
        {
            var targetAspectRatio = VirtualWidth / (float)VirtualHeight;
            // figure out the largest area that fits in this resolution at the desired aspect ratio
            var width = ScreenWidth;
            var height = (int)(width / targetAspectRatio);

            if (height > ScreenHeight)
            {
                height = ScreenHeight;
                // PillarBox
                width = (int)(height * targetAspectRatio);
            }

            // set up the new viewport centered in the backbuffer
            _viewport = new Viewport
            {
                X = (ScreenWidth / 2) - (width / 2),
                Y = (ScreenHeight / 2) - (height / 2),
                Width = width,
                Height = height
            };

            _graphicsDevice.Viewport = _viewport;
        }

        /// <summary>
        /// No idea
        /// </summary>
        public void SetupFullViewport()
        {
            var vp = new Viewport();
            vp.X = vp.Y = 0;
            vp.Width = ScreenWidth;
            vp.Height = ScreenHeight;
            _graphicsDevice.Viewport = vp;
            _dirtyMatrix = true;
        }

        /// <summary>
        /// Must be called before any drawing to set the viewport.
        /// </summary>
        public void BeginDraw()
        {
            // Start by reseting viewport to (0,0,1,1)
            SetupFullViewport();
            // Clear to Black
            _graphicsDevice.Clear(PillarColor);
            // Calculate Proper Viewport according to Aspect Ratio
            SetupVirtualScreenViewport();
            // and clear that
            
            // This way we are gonna have black bars if aspect ratio requires it and
            // the clear color on the rest
        }

        /// <summary>
        /// Don't know what this is for. Doesn't seem to be used.
        /// </summary>
        public bool RenderingToScreenIsFinished;
        private static Matrix _scaleMatrix;
        private bool _dirtyMatrix = true;

        /// <summary>
        /// Gets some sort of matrix to scale the rendering
        /// </summary>
        /// <returns></returns>
        public Matrix GetTransformationMatrix()
        {
            if (_dirtyMatrix)
                RecreateScaleMatrix();

            return _scaleMatrix;
        }

        private void RecreateScaleMatrix()
        {
            Matrix.CreateScale((float)ScreenWidth / VirtualWidth, (float)ScreenWidth / VirtualWidth, 1f, out _scaleMatrix);
            _dirtyMatrix = false;
        }

        /// <summary>
        /// Translates the actual mouse position to the correct position on the scaled viewport
        /// </summary>
        /// <param name="screenPosition"></param>
        /// <returns></returns>
        public Vector2 ScaleMouseToScreenCoordinates(Vector2 screenPosition)
        {
            var realX = screenPosition.X - _viewport.X;
            var realY = screenPosition.Y - _viewport.Y;

            _virtualMousePosition.X = realX / _ratioX;
            _virtualMousePosition.Y = realY / _ratioY;

            return _virtualMousePosition;
        }

        /// <summary>
        /// Sets the widths and heights needed
        /// </summary>
        /// <param name="virtualWidth"></param>
        /// <param name="virtualHeight"></param>
        /// <param name="realWidth"></param>
        /// <param name="realHeight"></param>
        public void SetWidthAndHeight(int virtualWidth, int virtualHeight, int realWidth, int realHeight)
        {
            VirtualWidth = virtualWidth;
            VirtualHeight = virtualHeight;

            ScreenWidth = realWidth;
            ScreenHeight = realHeight;
        }

    }
}
