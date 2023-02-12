using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class MovingEvent : Event
    {
        public enum Type { GoUp, GoDown, GoLeft, GoRight, JumpAble}
        public Type EventType;
        public GameObject GameObject;

        public MovingEvent(Type t, GameObject gameObject)
        {
            EventType = t;
            GameObject = gameObject;
        }
    }
}
