using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class AnimationEvent : Event
    {
        public enum Type { Animation }
        public Type EventType;
        public GameObject GameObject;
        public int Frame;
        public bool Flipped;

        public AnimationEvent(Type t, GameObject gameObject, int frame, bool flipped)
        {
            EventType = t;
            GameObject = gameObject;
            Frame = frame;
            Flipped = flipped;
        }
    }
}
