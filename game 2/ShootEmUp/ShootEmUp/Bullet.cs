using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Bullet:GameObject
    {
        public GameObject Gameobject;

        public string name;
       
        public Bullet(string name, GameObject player, int w, int h):base(name, w, h)
        {
            Gameobject = player;
            this.name = name;

            //VelY = -10;
            VelY = -150;
            Img.loadImage("image/MiniPixelPack3/Projectiles/PlayerBeam.png");

        }
    }
}
