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
        private bool relaod = false;
        private Random _rand = new Random();
        public PlayerShootingComponent(ShootingManager sm) : base(sm) {}

        public override void Shoot(float deltaTime)
        {
            BulletGap += deltaTime;
            BulletGapSize = Globals.BulletGap - GameObject.ShootingSpeed;

            if (BulletGap > BulletGapSize) //reload
            {
                if(!relaod)
                {
                    MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.ReloadShooting));
                    GameObject.CanShoot = true;
                    relaod = true;
                }

                if (GameObject.Shoot)
                {
                    Bullet = (Bullet)Program.Game.RequestPlayerBullet(GameObject);
                    MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.Shooting));
                    Bullet.PosY = Bullet.GameObject.PosY + (Bullet.GameObject.Height / 4);
                    Bullet.PosX = Bullet.GameObject.PosX + (Bullet.GameObject.Width / 2);

                    if (GameObject.direction == "right")
                    {
                        Bullet.VelX = Globals.Velocity*2.5f;

                    }
                    else if (GameObject.direction == "left")
                    {
                        Bullet.VelX = -Globals.Velocity*2.5f;
                    }

                    Bullet.VelY = _rand.Next(-20, 2);

                    BulletGap = Globals.Reset;
                    GameObject.Shoot = false;
                    GameObject.CanShoot = false;
                    relaod = false;
                }
            }
        }
    }
}
