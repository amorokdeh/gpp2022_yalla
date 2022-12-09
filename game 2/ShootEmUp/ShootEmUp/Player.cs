using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Player:GameObject
    {
        public Player(string name): base(name)
        {
            Img.loadImage("image/MiniPixelPack3/PlayerShip/PlayerShip.png");
        }
    }
}
