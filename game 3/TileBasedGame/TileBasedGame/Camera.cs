using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{

    class Camera
    {
        public float PosX;
        public float PosY;

        private int mapWidth = Program.Game._maps.currentMap.mapWidth;
        private int mapHeight = Program.Game._maps.currentMap.mapHeight;
        private int winWidth = Program.Window.Width;
        private int winHeight = Program.Window.Height;

        public Camera(GameObject player)
        {
            PosX = player.PosX;
            PosY = player.PosY;
        }

        public void UpdateCamera(GameObject player)
        {
            PosX = player.PosX;
            PosY = player.PosY;

            //Fix position
            if (PosX - (winWidth / 2) < 0) { PosX = (winWidth / 2); }
            if (PosX + (winWidth / 2) > mapWidth) { PosX = mapWidth - (winWidth / 2); }

            if (PosY - (winHeight / 2) < 0) { PosY = (winHeight / 2); }
            if (PosY + (winHeight / 2) > mapHeight) { PosY = mapHeight - (winHeight / 2); }
        }
    }
}
