using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Engine
{
    /// <summary>
    /// Enum to describe the type of an event
    /// </summary>
    public enum EventType
    {
        /// <summary>An Event with only user data in it</summary>
        USERDATA,
        /// <summary>An event showing a change in score</summary>
        SCORE_CHANGED,
        /// <summary>An event showing a change in ammo</summary>
        AMMO_CHANGED,
        /// <summary>An event used to set an ammo amount</summary>
        SET_AMMO,
        /// <summary>An event used to change the weapon</summary>
        WEAPON_CHANGED
    }

    /// <summary>
    /// Used to pass messages between entities without coupling them too much
    /// </summary>
    public class GameEvent
    {
        object _userData;
        /// <summary>A user data object that can be used to hold various things</summary>
        public object UserData { get { return _userData; } }

        EventType _eventType;
        /// <summary>The type of event</summary>
        public EventType EventType { get { return _eventType; } }

        int _changeInt;
        /// <summary>The int related to a change event (score, ammo, etc)</summary>
        public int ChangeInt { get { return _changeInt; } }

        /// <summary>Creates an event with only user data</summary>
        /// <param name="eventTypeIn"></param>
        /// <param name="userDataIn"></param>
        public GameEvent(EventType eventTypeIn, object userDataIn = null)
        {
            _eventType = eventTypeIn;
            _userData = userDataIn;
        }

        /// <summary>A game event used to change an int</summary>
        /// <param name="eventTypeIn"></param>
        /// <param name="intChangeIn"></param>
        /// <param name="userDataIn"></param>
        public GameEvent(EventType eventTypeIn, int intChangeIn, object userDataIn = null)
        {
            _eventType = eventTypeIn;
            _changeInt = intChangeIn;
            _userData = userDataIn;
        }
    }
}
