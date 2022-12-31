using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Ship : Enemy
    {
        public Ship(string name, int w, int h) : base(name, w, h)
        {
            PosX = 600;
            PosY = 0;

            Img.loadImage("image/MiniPixelPack3/Enemies/Lips.png");
        }

        public override void ChangeImage()
        {
            base.ChangeImage(4);
        }
    }
}
