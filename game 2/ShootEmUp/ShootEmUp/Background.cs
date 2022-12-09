using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Background : GameObject
    {
        public Background(string name) : base(name)
        {
            Img.loadImage("image/MiniPixelPack3/SpaceBG.png");
            VelX = 0;
            VelY = 10;
            PosX = 0;
            PosY = 0;
        }
    }
}
