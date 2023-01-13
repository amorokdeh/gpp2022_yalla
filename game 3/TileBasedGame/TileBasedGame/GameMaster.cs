using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class GameMaster : Observer
    {
        public GameMaster()
        {
            MessageBus.Register(this);
            

        }
        public void OnEvent(Event e)
        {
            HeroEvent he = e as HeroEvent;
            if (he == null)
                return;
            if (he.EventType == HeroEvent.Type.MonsterHurt)
                Console.WriteLine("SLASH - The valiant hero does " + he.Arg0 + " damage!");
            if (he.EventType == HeroEvent.Type.MonsterKilled)
                Console.WriteLine("LOOT - The vile monster dies and drops treasure");
        }

        /*
        Queue<HeroEvent> _eventQueue = new Queue<HeroEvent>();

        public void DoTurn()
        {
            foreach (HeroEvent e in _eventQueue)
                HandleEvent(e);
        }
        public void HandleEvent(Event e)
        {
            HeroEvent he = e as HeroEvent;
            if (he.EventType == HeroEvent.Type.MonsterHurt)
                Console.WriteLine("SLASH - The valiant hero does " + he.Arg0 + " damage!");
            if (he.EventType == HeroEvent.Type.MonsterKilled)
                Console.WriteLine("LOOT - The vile monster dies and drops treasure");
        }
        public void OnEvent(Event e)
        {
            HeroEvent he = e as HeroEvent;
            if (he == null)
                return;
            _eventQueue.Enqueue(he);
        }*/
    }
}
