
namespace TileBasedGame
{
    class ShootingComponent : Component
    {
        ShootingManager ShootingManager;

        protected float BulletGap = Globals.Reset;
        protected float BulletGapSize = Globals.BulletGap;
        
        public ShootingComponent(ShootingManager sm)
        {
            this.ShootingManager = sm;
        }

        public virtual void Shoot(float deltaTime) {}
    }
}
