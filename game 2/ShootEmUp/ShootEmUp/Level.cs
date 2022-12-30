using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Level
    {
        
        DateTime timeBefore = DateTime.Now;
        DateTime timeNow = DateTime.Now;
        float deltaTime;
        float avDeltaTime = -1;
        Player player;

        public float gap = 0;
        public float gapSize = 1f;
        public GameObject gameObject;


        float bulletGap = 0;
        float bulletGapSize = 2f;
        Bullet bullet;

        public Random rand;

        public virtual void run()
        {
            this.player = Program.game.Player;
            player.Reset();

            LevelManager.ControlQuitRequest = false;
            rand = new Random();



            while (true)
            {
                Program.window.calculateFPS(); //frame limit start calculating here
                timeNow = DateTime.Now;
                deltaTime = (timeNow.Ticks - timeBefore.Ticks) / 10000000f;
                if (avDeltaTime == -1)
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
                Program.game.Animate(avDeltaTime);
                Program.game.Collide();
                if (player.Lives <= 0)
                {
                    LevelManager.display = LevelManager.GameState.GameOver;
                    Program.game._audio.stopMusic();
                    Program.game._audio.runSound("Game Over");
                    Program.game.SetInactive();
                    return;
                }
                Program.game.Render();

                if (Game.Quit)
                {
                    LevelManager.display = LevelManager.GameState.Quit;
                    LevelManager.ControlQuitRequest = true;
                    Program.game.SetInactive();
                    Program.game._audio.stopMusic();

                }
                if (LevelManager.ControlQuitRequest)
                {
                    Program.game.SetInactive();
                    Program.game._audio.stopMusic();
                    return;
                }  // press escape to quit
                Program.window.deltaFPS(); //frame limit end calculating here

            }
        }


        public virtual void produceEnemies(float deltaTime)
        {
            
            gap += deltaTime;
            if (gap > gapSize)
            {
                gameObject = Program.game.RequestEnemyShip();

                gameObject.PosY = 0;
                gameObject.PosX = rand.Next(0, Program.window.width); // Enemy Random Position 
                gameObject = Program.game.RequestEnemyUfo();

                gameObject.PosY = 0;
                gameObject.PosX = rand.Next(0, Program.window.width); // Enemy Random Position 

                gap = 0;
            }
        }


        public virtual void produceBullets(float deltaTime)
        {
            bulletGap += deltaTime;
            if ((bulletGap > bulletGapSize) || (Program.game.bulletReloadable))
            {
                bullet = (Bullet)Program.game.RequestPlayerBullet(player);
                Console.WriteLine(bullet.Active);

                Program.game._audio.runSound("Shooting");

                bullet.PosY = bullet.Gameobject.PosY;
                bullet.PosX = bullet.Gameobject.PosX;

                bulletGap = 0;
                Program.game.bulletReloadable = false;
            }
        }
    }
}
