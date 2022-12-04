using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Level1:Level
    {

        public override void run()
        {
            Program.game.BuildPlayer();
            Program.game.BuildShip();
            Program.game.BuildUfo();

            while (true)
            {
                Program.game.ControlEnemy();   
                Program.game.ControlPlayer();
                Program.game.Move();
                Program.game.Render();
            }
        }
        
    }
}
