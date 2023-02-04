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
            GameObject go = GameObject;

            int winHeight = Program.Window.Height;
            int winWidth = Program.Window.Width;

            float cameraX = Program.Game.Camera.PosX;
            float cameraY = Program.Game.Camera.PosY;

            float camLeftBorder = cameraX - winWidth / 2;
            float camRightBorder = cameraX + winWidth / 2;
            float camTopBorder = cameraY - winHeight / 2;
            float camBottomBorder = cameraY + winHeight / 2;

            bool objectOnCamera = (go.PosX < camRightBorder) && (go.PosX + go.Width > camLeftBorder) && (go.PosY < camBottomBorder) && (go.PosY + go.Height > camTopBorder);

            go.CurrentVelY = go.VelY;
            go.CurrentVelX = go.VelX;

            if (go is Tile)
            {
                if (objectOnCamera){
                    go.Active = true;
                }
                else { 
                    go.Active = false; 
                }

            }

            if (go is Bullet && ! objectOnCamera) {

                Program.Game.DespawnPlayerBullet(go);
            }
            /*
            if (GameObject is EnemyBullet && GameObject.PosY > Program.Window.Height)
            {
                Program.Game.DespawnEnemyBullet(GameObject);
            }
            */
        }
    }
}
