using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class HeroEvent : Event
    {
        public enum Type { MonsterHurt, MonsterKilled}
        public Type EventType;
        public int Arg0;
        public HeroEvent(Type t, int arg0 = 0)
        {
            EventType = t;
            Arg0 = arg0;
        }
    }
}
