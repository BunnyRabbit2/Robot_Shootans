using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Engine
{
    /// <summary>
    /// Has some helper methods for retrieving things like if a key press is a new press or not
    /// </summary>
    public static class InputHelper
    {
        private static KeyboardState _oldKeystate, _newKeyState;
        private static GamePadState[] _oldPadState, _newPadState;

        /// <summary>
        /// Initialises the helper
        /// </summary>
        public static void InitialiseInputHelper()
        {
            _oldKeystate = Keyboard.GetState();
            _oldPadState = new[] { GamePad.GetState(PlayerIndex.One), GamePad.GetState(PlayerIndex.Two), GamePad.GetState(PlayerIndex.Three), GamePad.GetState(PlayerIndex.Four) };

            _newKeyState = Keyboard.GetState();
            _newPadState = new[] { GamePad.GetState(PlayerIndex.One), GamePad.GetState(PlayerIndex.Two), GamePad.GetState(PlayerIndex.Three), GamePad.GetState(PlayerIndex.Four) };
        }

        /// <summary>
        /// Updates the helpers states. MUST BE CALLED EACH LOOP
        /// </summary>
        public static void UpdateInput()
        {
            _oldKeystate = _newKeyState;
            _oldPadState = _newPadState;

            _newKeyState = Keyboard.GetState();
            _newPadState = new[] { GamePad.GetState(PlayerIndex.One), GamePad.GetState(PlayerIndex.Two), GamePad.GetState(PlayerIndex.Three), GamePad.GetState(PlayerIndex.Four) };
        }

        /// <summary>
        /// Returns if a key press is new or not
        /// </summary>
        /// <param name="keyIn">The key to check</param>
        /// <returns></returns>
        public static bool isKeyPressNew(Keys keyIn)
        {
            if (_oldKeystate.IsKeyUp(keyIn) && _newKeyState.IsKeyDown(keyIn))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns if a key release is new or not
        /// </summary>
        /// <param name="keyIn">The key to check</param>
        /// <returns></returns>
        public static bool isKeyUpNew(Keys keyIn)
        {
            if (_oldKeystate.IsKeyDown(keyIn) && _newKeyState.IsKeyUp(keyIn))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyIn"></param>
        /// <returns></returns>
        public static bool isKeyDown(Keys keyIn)
        {
            return _newKeyState.IsKeyDown(keyIn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyIn"></param>
        /// <returns></returns>
        public static bool isKeyUp(Keys keyIn)
        {
            return _newKeyState.IsKeyUp(keyIn);
        }

        /// <summary>
        /// Checks to see if a button is pressed on any pad
        /// </summary>
        /// <param name="buttonIn">The button to check</param>
        /// <returns></returns>
        public static bool isButtonDownAnyPad(Buttons buttonIn)
        {
            if (_newPadState[0].IsButtonDown(buttonIn) || _newPadState[1].IsButtonDown(buttonIn)
                || _newPadState[2].IsButtonDown(buttonIn) || _newPadState[3].IsButtonDown(buttonIn))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns if a button pressed on the selcted pad is a new press
        /// </summary>
        /// <param name="buttonIn">The button to check</param>
        /// <param name="pi">The player index of the pad to be checked</param>
        /// <returns></returns>
        public static bool isButtonPressNew(Buttons buttonIn, PlayerIndex pi)
        {
            if (_oldPadState[(int)pi].IsButtonUp(buttonIn) && _newPadState[(int)pi].IsButtonDown(buttonIn))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns if a button pressed on any pad is a new press
        /// </summary>
        /// <param name="buttonIn">The button to check</param>
        /// <returns></returns>
        public static bool isButtonPressNewAnyPad(Buttons buttonIn)
        {
            if ((_oldPadState[0].IsButtonUp(buttonIn) && _newPadState[0].IsButtonDown(buttonIn))
                || (_oldPadState[1].IsButtonUp(buttonIn) && _newPadState[1].IsButtonDown(buttonIn))
                || (_oldPadState[2].IsButtonUp(buttonIn) && _newPadState[2].IsButtonDown(buttonIn))
                || (_oldPadState[3].IsButtonUp(buttonIn) && _newPadState[3].IsButtonDown(buttonIn))
                )
                return true;
            else
                return false;
        }
    }
}
