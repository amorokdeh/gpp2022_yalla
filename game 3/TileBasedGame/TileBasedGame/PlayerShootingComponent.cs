using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
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
            if ((BulletGap > BulletGapSize))
            {
                Bullet = (Bullet)Program.Game.RequestPlayerBullet(GameObject);
                Console.WriteLine(Bullet.Active);

                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.Shooting));

                Bullet.PosY = Bullet.GameObject.PosY + (Bullet.GameObject.Height / 3);
                Bullet.PosX = Bullet.GameObject.PosX + (Bullet.GameObject.Width / 3);

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
