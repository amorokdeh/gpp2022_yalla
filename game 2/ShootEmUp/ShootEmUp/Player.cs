using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Player:GameObject
    {
        public int Lives = 3;
        public int Score = 0;
        public Player(string name, int w, int h): base(name, w, h)
        {
            ImgChange = 16;
            Img.loadImage("image/MiniPixelPack3/PlayerShip/PlayerShip.png");
        }

        public void FlyLeft()
        {
            ImgChange = 0;
        }
        public void FlyStraight()
        {
            ImgChange = 16;
        }
        public void FlyRight()
        {
            ImgChange = 16*2;
        }
    }
}
