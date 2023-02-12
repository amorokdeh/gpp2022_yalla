﻿using System;
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

            int winHeight = Program.Window.Height;
            int winWidth = Program.Window.Width;

            float cameraX = Program.Game.Camera.PosX;
            float cameraY = Program.Game.Camera.PosY;

            float camLeftBorder = cameraX - winWidth / 2;
            float camRightBorder = cameraX + winWidth / 2;
            float camTopBorder = cameraY - winHeight / 2;
            float camBottomBorder = cameraY + winHeight / 2;

            bool objectOnCamera = (GameObject.PosX < camRightBorder) && (GameObject.PosX + GameObject.Width > camLeftBorder) && (GameObject.PosY < camBottomBorder) && (GameObject.PosY + GameObject.Height > camTopBorder);

            GameObject.CurrentVelY = GameObject.VelY;
            GameObject.CurrentVelX = GameObject.VelX;

            if (GameObject is Tile)
            {
                if (objectOnCamera){
                    GameObject.Active = true;
                }
                else { 
                    GameObject.Active = false; 
                }

            }

            if (GameObject is Bullet && ! objectOnCamera) {

                Program.Game.DespawnPlayerBullet(GameObject);
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
