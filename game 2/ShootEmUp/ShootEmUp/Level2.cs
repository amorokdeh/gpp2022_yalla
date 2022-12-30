using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Level2:Level
    {
        private float sinGap = 0;

        public Level2()
        {
            gapSize = 0.3f;
        }
        public override void produceEnemies(float deltaTime)
        {
            gap += deltaTime;
            sinGap += deltaTime;
            if (gap > gapSize)
            {
                gameObject = Program.game.RequestEnemyShip();

                gameObject.PosY = 0;
                gameObject.PosX = (float)(Math.Sin(sinGap) * (Program.window.width/2 - gameObject.Width / 2) + Program.window.width/2 - gameObject.Width/2);

                Console.WriteLine(gameObject.PosX);


                gameObject = Program.game.RequestEnemyUfo();

                gameObject.PosY = 0;
                gameObject.PosX = (float)(Math.Sin(sinGap + Math.PI) * (Program.window.width / 2 - gameObject.Width / 2) + Program.window.width / 2 - gameObject.Width / 2);

                gap = 0;
            }
        }
    }
}
