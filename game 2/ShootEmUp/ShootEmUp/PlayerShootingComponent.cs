using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class PlayerShootingComponent : ShootingComponent
    {
        protected Bullet Bullet;
        public PlayerShootingComponent(ShootingManager sm) : base(sm)
        {
        }

        public override void Shoot(float deltaTime)
        {
            BulletGap += deltaTime;
            if ((BulletGap > BulletGapSize) || (Program.Game.BulletReloadable))
            {
                Bullet = (Bullet)Program.Game.RequestPlayerBullet(GameObject);
                Console.WriteLine(Bullet.Active);

                Program.Game._audio.RunSound("Shooting");

                Bullet.PosY = Bullet.GameObject.PosY;
                Bullet.PosX = Bullet.GameObject.PosX;

                BulletGap = 0;
                Program.Game.BulletReloadable = false;
            }

        }
    }
}
