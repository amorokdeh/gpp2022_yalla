using System;

namespace TileBasedGame
{
    class EnemyShootingComponent : ShootingComponent
    {
        protected EnemyBullet Bullet;
        public EnemyShootingComponent(ShootingManager sm) : base(sm) {}

        public override void Shoot(float deltaTime)
        {
            BulletGap += deltaTime;
            if ((BulletGap > BulletGapSize))
            {
                Bullet = (EnemyBullet)Program.Game.RequestEnemyBullet(GameObject);

                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.EnemyShooting));               

                Bullet.PosY = Bullet.GameObject.PosY;
                Bullet.PosX = Bullet.GameObject.PosX;

                if (GameObject.direction == "right")
                {
                    Bullet.VelX = 200;

                }
                else if (GameObject.direction == "left")
                {
                    Bullet.VelX = -200;
                }

                BulletGap = Globals.Reset;
            }
        }
    }
}
