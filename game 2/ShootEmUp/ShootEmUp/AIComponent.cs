using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class AIComponent : Component
    {
        AIManager AIManager;
        public AIComponent(AIManager am)
        {
            this.AIManager = am;
        }

        public void Control()
        {
            GameObject.CurrentVelY = GameObject.VelY;

            //nur zum Testen
            if(GameObject is Enemy && GameObject.PosY > Program.window.height)
            {
                Program.game.DespawnEnemy(GameObject);
            }
            if (GameObject is Bullet && GameObject.PosY < 0)
            {
                Program.game.DespawnPlayerBullet(GameObject);
            }
        }
    }
}
