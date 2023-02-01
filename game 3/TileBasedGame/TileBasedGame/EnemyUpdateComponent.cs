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
            if (he.GameObject == this.GameObject)
            {
                if (he.EventType == HeroEvent.Type.Collision)
                {
                    hero = he;
                }
                else if (he.EventType == HeroEvent.Type.NeutralCollision)
                {
                    UpdatePosition(he);
                }
                else if (he.EventType == HeroEvent.Type.ChangeDirection)
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

        public void changeDirection()
        {
            if (GameObject.direction == "left")
            {
                moveRight();
            }
            else if (GameObject.direction == "right")
            {
                moveLeft();
            }
        }

        public void moveLeft()
        {
            GameObject.direction = "left";
            GameObject.VelX *= -1;

        }

        public void moveRight()
        {
            GameObject.direction = "right";
            GameObject.VelX *= -1;
        }

    }
}
