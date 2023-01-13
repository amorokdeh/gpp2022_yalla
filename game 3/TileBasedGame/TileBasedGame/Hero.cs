using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Hero : Observable
    {
        int monsterHP = 100;

        public void SlayMonsters()
        {
            monsterHP -= 35;
            MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.MonsterHurt, 35));
            if (monsterHP <= 0)
            {
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.MonsterKilled, 35));
                monsterHP = 100;
            }
        }

        /*
        public void SlayMonsters()
        {
            monsterHP -= 35;
            Notify(new HeroEvent(HeroEvent.Type.MonsterHurt, 35));
            if(monsterHP <= 0)
            {
                Notify(new HeroEvent(HeroEvent.Type.MonsterKilled, 35));
                monsterHP = 100;
            }
        }*/
    }
}
