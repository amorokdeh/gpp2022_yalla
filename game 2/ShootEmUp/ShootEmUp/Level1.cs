using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Level1:Level
    {
        DateTime timeBefore = DateTime.Now;
        DateTime timeNow = DateTime.Now;
        float deltaTime;
        float avDeltaTime = -1;
        GameObject player;

        float gap = 0;
        float gapSize = 1f;
        GameObject gameObject;


        float bulletGap = 0;
        float bulletGapSize = 0.8f;
        Bullet bullet;

        public override void run()
        {


            Program.game.BuildBackground();
            player = Program.game.BuildPlayer();
            //Program.game.BuildShip();
            //Program.game.BuildUfo();
            

            while (!Program.game.Quit)
            {
                timeNow = DateTime.Now;
                deltaTime = (timeNow.Ticks - timeBefore.Ticks) / 10000000f;
                if(avDeltaTime == -1) 
                {
                    avDeltaTime = deltaTime;
                }
                else
                {
                    avDeltaTime = (deltaTime + avDeltaTime) / 2f;
                }               
                timeBefore = timeNow;

                produceEnemies(avDeltaTime);
                produceBullets(avDeltaTime);
                Program.game.ControlEnemy();   
                Program.game.ControlPlayer();
                Program.game.Move(avDeltaTime);
                Program.game.Collide();
                Program.game.Render();
                
            }
            Program.game.quit();
        }


        public void produceEnemies(float deltaTime)
        {
            gap += deltaTime;
            if (gap > gapSize)
            {
                gameObject = Program.game.RequestEnemyShip();

                gameObject.PosY = 0;
                gameObject.PosX = 0;
                gameObject = Program.game.RequestEnemyUfo();

                gameObject.PosY = 0;
                gameObject.PosX = 300;
                
                gap = 0;
            }
        }

        
        public void produceBullets(float deltaTime)
        {
            bulletGap += deltaTime;
            if (bulletGap > bulletGapSize)
            {
                bullet = (Bullet)Program.game.RequestPlayerBullet(player);

                bullet.PosY = bullet.Gameobject.PosY;
                bullet.PosX = bullet.Gameobject.PosX;

                bulletGap = 0;
            }
        }

    }
}
