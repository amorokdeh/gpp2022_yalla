using System;

namespace TileBasedGame
{
    class EnemyUpdateComponent : UpdateComponent
    {
        public EnemyUpdateComponent(UpdateManager um) : base(um) {}

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
            Program.Game.Player.Score++;
            GameObject.Died = true;
            GameObject.VelY= 0;
            GameObject.VelX= 0;

            MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.EnemyDead, GameObject));         
        }

        public void changeDirection()
        {
            if (GameObject.direction == "left")
            {
                moveRight();
                MessageBus.PostEvent(new MovingEvent(MovingEvent.Type.GoRight, GameObject));
            }
            else if (GameObject.direction == "right")
            {
                moveLeft();
                MessageBus.PostEvent(new MovingEvent(MovingEvent.Type.GoLeft, GameObject));
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
