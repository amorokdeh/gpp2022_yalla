using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Ufo:Enemy
    {
        public Ufo(string name, int w, int h) : base(name, w, h)
        {
            FlyingImg = Program.Game.loader.ufoImg;
            Img = FlyingImg;
        }

        public override void ChangeImage()
        {
            if (Died)
                base.Explode();
            else
                base.ChangeImage(3);
        }
    }
}
