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

        }
    }
}

