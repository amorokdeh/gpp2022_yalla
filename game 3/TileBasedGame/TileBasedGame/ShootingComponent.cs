using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class ShootingComponent : Component
    {
        ShootingManager ShootingManager;

        protected float BulletGap = 0;
        protected float BulletGapSize = 2f;
        
        public ShootingComponent(ShootingManager sm)
        {
            this.ShootingManager = sm;
        }

        public virtual void Shoot(float deltaTime)
        {
        }
    }
}
