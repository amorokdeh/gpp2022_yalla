using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Level2:Level
    {
        private float _sinGap = Globals.Reset;

        public Level2()
        {
            GapSize = 0.3f;
        }

        public override void buildMap()
        {

            Program.Game._maps.loadMap("Level 2");
            Program.Game._maps.createMap();
        }


        public override void ProduceEnemies(float deltaTime)
        {
            Gap += deltaTime;
            _sinGap += deltaTime;
            if (Gap > GapSize)
            {
                GameObject = Program.Game.RequestEnemyShip();

                GameObject.PosY = Globals.Reset;
                GameObject.PosX = (float)(Math.Sin(_sinGap) * (Program.Window.Width/2 - GameObject.Width / 2) + Program.Window.Width/2 - GameObject.Width/2);

                Console.WriteLine(GameObject.PosX);


                GameObject = Program.Game.RequestEnemyUfo();

                GameObject.PosY = Globals.Reset;
                GameObject.PosX = (float)(Math.Sin(_sinGap + Math.PI) * (Program.Window.Width / 2 - GameObject.Width / 2) + Program.Window.Width / 2 - GameObject.Width / 2);

                Gap = 0;
            }
        }
    }
}
