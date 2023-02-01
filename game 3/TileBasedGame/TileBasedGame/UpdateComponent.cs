using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class UpdateComponent : Component
    {
        UpdateManager UpdateManager;

        public HeroEvent hero;
        public UpdateComponent(UpdateManager um):base()
        {
            this.UpdateManager = um;
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
        }

        public void DoUpdate()
        {
            if (hero != null)
                Update();
            hero = null;

            
                
        }

        public virtual void Update()
        {

        }
        public virtual void UpdatePosition(HeroEvent heroEv)
        {
            GameObject.PosY = heroEv.NewY;
            GameObject.PosX = heroEv.NewX;
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

