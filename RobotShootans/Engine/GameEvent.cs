using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Engine
{
    public enum EventType { USERDATA, SCORE_CHANGED, AMMO_CHANGED, SET_AMMO }

    public class GameEvent
    {
        object _userData;

        public object UserData { get { return _userData; } }

        EventType _eventType;

        public EventType EventType { get { return _eventType; } }

        int _changeInt;

        public int ChangeInt { get { return _changeInt; } }

        public GameEvent(EventType eventTypeIn, object userDataIn = null)
        {
            _eventType = eventTypeIn;
            _userData = userDataIn;
        }

        public GameEvent(EventType eventTypeIn, int intChangeIn, object userDataIn = null)
        {
            _eventType = eventTypeIn;
            _changeInt = intChangeIn;
            _userData = userDataIn;
        }
    }
}
