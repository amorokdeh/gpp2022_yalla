using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Bullet:GameObject
    {
        public GameObject GameObject;

        public string Name;
       
        public Bullet(string name, GameObject player, int w, int h):base(name, w, h)
        {
            GameObject = player;
            this.Name = name;

            //VelY = -10;
            VelY = -150;
            Img.LoadImage("image/MiniPixelPack3/Projectiles/PlayerBeam.png");

        }
    }
}
