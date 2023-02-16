using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class BulletUpdateComponent : UpdateComponent
    {
        public BulletUpdateComponent(UpdateManager um) : base(um)
        {
            MessageBus.Register(this);
        }

        public override void OnEvent(Event e)
        {
            HeroEvent he = e as HeroEvent;
            if (he == null)
                return;

            if (he.GameObject == this.GameObject)
            {
                hero = he;
            }
        }

        public override void Update()
        {
            Program.Game.DespawnBullet(GameObject);
        }
    }
}
