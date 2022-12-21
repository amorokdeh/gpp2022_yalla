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
        Player player;

        float gap = 0;
        float gapSize = 1f;
        GameObject gameObject;


        float bulletGap = 0;
        float bulletGapSize = 2f;
        Bullet bullet;

        public override void run()
        {
            this.player = Program.game.Player;
            player.Reset();

            //Program.game.BuildBackground();
            //player = (Player)Program.game.BuildPlayer();
            //Program.game.BuildShip();
            //Program.game.BuildUfo();
            LevelManager.ControlQuitRequest = false;



            while (true)
            {
                Program.window.calculateFPS(); //frame limit start calculating here
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
                //Console.WriteLine(deltaTime);
                //Console.WriteLine(avDeltaTime);

                produceEnemies(avDeltaTime);
                produceBullets(avDeltaTime);
                Program.game.ControlEnemy();   
                Program.game.ControlPlayer();
                Program.game.Move(avDeltaTime);
                Program.game.Collide();
                if(player.Lives <= 0) 
                { 
                    LevelManager.display = LevelManager.GameState.GameOver;
                    Program.game.SetInactive();
                    return; 
                }
                Program.game.Render();

                if (Game.Quit) { 
                    LevelManager.display = LevelManager.GameState.Quit; 
                    LevelManager.ControlQuitRequest = true;
                    Program.game.SetInactive();
                }
                if (LevelManager.ControlQuitRequest) 
                {
                    Program.game.SetInactive();
                    return; 
                }  // press escape to quit
                Program.window.deltaFPS(); //frame limit end calculating here

            }
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
                Console.WriteLine(bullet.Active);

                bullet.PosY = bullet.Gameobject.PosY;
                bullet.PosX = bullet.Gameobject.PosX;

                bulletGap = 0;
            }
        }

    }
}
