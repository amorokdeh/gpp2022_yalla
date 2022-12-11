using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Background : GameObject
    {
        public Background(string name,int w, int h, int x = 0, int y = 0) : base(name, w, h)
        {
            Img.loadImage("image/MiniPixelPack3/SpaceBG.png");
            VelX = 0;
            VelY = 10;
            PosX = x;
            PosY = y;
        }
    }
}
