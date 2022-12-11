using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Player:GameObject
    {
        public Player(string name, int w, int h): base(name, w, h)
        {
            Img.loadImage("image/MiniPixelPack3/PlayerShip/PlayerShip.png");
        }
    }
}
