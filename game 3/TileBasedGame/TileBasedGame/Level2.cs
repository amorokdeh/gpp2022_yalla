using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Level2:Level
    {
        private float _sinGap = 0;

        public Level2()
        {
            GapSize = 0.3f;
        }
        public override void ProduceEnemies(float deltaTime)
        {
            Gap += deltaTime;
            _sinGap += deltaTime;
            if (Gap > GapSize)
            {
                GameObject = Program.Game.RequestEnemyShip();

                GameObject.PosY = 0;
                GameObject.PosX = (float)(Math.Sin(_sinGap) * (Program.Window.Width/2 - GameObject.Width / 2) + Program.Window.Width/2 - GameObject.Width/2);

                Console.WriteLine(GameObject.PosX);


                GameObject = Program.Game.RequestEnemyUfo();

                GameObject.PosY = 0;
                GameObject.PosX = (float)(Math.Sin(_sinGap + Math.PI) * (Program.Window.Width / 2 - GameObject.Width / 2) + Program.Window.Width / 2 - GameObject.Width / 2);

                Gap = 0;
            }
        }
    }
}
