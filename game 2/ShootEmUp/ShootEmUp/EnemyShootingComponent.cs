using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class EnemyShootingComponent : ShootingComponent
    {
        protected EnemyBullet Bullet;
        public EnemyShootingComponent(ShootingManager sm) : base(sm)
        {


        }

        public override void Shoot(float deltaTime)
        {
            BulletGap += deltaTime;
            if ((BulletGap > BulletGapSize))
            {
                Bullet = (EnemyBullet)Program.Game.RequestEnemyBullet(GameObject);
                Console.WriteLine(Bullet.Active);

                //Program.game._audio.runSound("Shooting");

                Bullet.PosY = Bullet.GameObject.PosY;
                Bullet.PosX = Bullet.GameObject.PosX;

                BulletGap = 0;

            }
        }
    }
}
