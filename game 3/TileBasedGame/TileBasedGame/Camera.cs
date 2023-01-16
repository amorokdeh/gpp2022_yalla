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

        public Camera(GameObject player)
        {
            PosX = player.PosX;
            PosY = player.PosY;
        }

        public void UpdateCamera(GameObject player)
        {
            PosX = player.PosX;
            PosY = player.PosY;
        }
    }
}
