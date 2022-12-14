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
            if(GameObject is Ufo && GameObject.PosY > Program.window.heigh)
            {
                Program.game.DespawnEnemyUfo(GameObject);
            }
            if (GameObject is Ship && GameObject.PosY > Program.window.heigh)
            {
                Program.game.DespawnEnemyShip(GameObject);
            }
            if (GameObject is Bullet && GameObject.PosY < 0)
            {
                Program.game.DespawnPlayerBullet(GameObject);
            }
        }
    }
}
