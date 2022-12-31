using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Ufo:Enemy
    {
        public Ufo(string name, int w, int h) : base(name, w, h)
        {
            PosX = 500;
            PosY = 0;

            Img.loadImage("image/MiniPixelPack3/Enemies/Alan.png");
        }

        public override void ChangeImage()
        {
            base.ChangeImage(3);
        }
    }
}
