using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Ship : Enemy
    {
        public Ship(string name, int w, int h) : base(name, w, h)
        {
            PosX = 600;
            PosY = 0;

            FlyingImg.LoadImage("image/MiniPixelPack3/Enemies/Lips.png");
            Img = FlyingImg;
        }

        public override void ChangeImage()
        {
            if (Died)
                base.Explode();
            else
                base.ChangeImage(4);
        }
    }
}
