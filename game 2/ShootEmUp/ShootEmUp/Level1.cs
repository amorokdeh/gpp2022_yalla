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
        public override void run()
        {

            Program.game.BuildBackground();
            Program.game.BuildPlayer();
            Program.game.BuildShip();
            Program.game.BuildUfo();
            

            while (true)
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

                Program.game.ControlEnemy();   
                Program.game.ControlPlayer();
                Program.game.Move(avDeltaTime);
                Program.game.Render();
            }
        }
        
    }
}
