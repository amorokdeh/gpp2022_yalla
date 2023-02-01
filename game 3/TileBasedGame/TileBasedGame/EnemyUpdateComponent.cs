using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class EnemyUpdateComponent : UpdateComponent
    {
        public EnemyUpdateComponent(UpdateManager um) : base(um)
        {
            
        }

        public override void OnEvent(Event e)
        {
            HeroEvent he = e as HeroEvent;
            if (he == null)
                return;
            if (he.EventType == HeroEvent.Type.Collision)
            {
                if (he.GameObject == this.GameObject)
                {
                    hero = he;
                }
            }
            else if (he.EventType == HeroEvent.Type.NeutralCollision)
            {
                if (he.GameObject == this.GameObject)
                {
                    UpdatePosition(he);
                }
            }
            else if (he.EventType == HeroEvent.Type.ChangeDirection)
            {

                if (he.GameObject == this.GameObject)
                {
                    changeDirection();
                }

            }
        }


        public override void Update()
        {
            Console.WriteLine("ENEMY");
            Program.Game.BulletReloadable = true;
            Program.Game.Player.Score++;
            GameObject.Died = true;

            //TODO überarbeiten
            //Enemy enemy = (Enemy)GameObject;
            //enemy.Img = enemy.ExplodingImg;

            MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.EnemyDead));         
        }

    }
}
