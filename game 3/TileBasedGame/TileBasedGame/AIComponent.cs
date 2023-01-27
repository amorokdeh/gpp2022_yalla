using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
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
            GameObject.CurrentVelX = GameObject.VelX;

            //nur zum Testen
            if (GameObject is Enemy && GameObject.PosY > Program.Window.Height)
            {
                Program.Game.DespawnEnemy(GameObject);
            }
            if (GameObject is Bullet && GameObject.PosY < Globals.LeftBoundary)
            {
                Program.Game.DespawnPlayerBullet(GameObject);
            }
            if (GameObject is EnemyBullet && GameObject.PosY > Program.Window.Height)
            {
                Program.Game.DespawnEnemyBullet(GameObject);
            }
        }
    }
}
