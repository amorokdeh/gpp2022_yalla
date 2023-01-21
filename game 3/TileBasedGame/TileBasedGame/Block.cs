using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Block : GameObject
    {


        public Block(string name, int w, int h, int x, int y, int imgNum) : base(name, w, h)
        {
            //Img.LoadImage("image/MiniPixelPack3/Maps/PC Computer - Jazz Jackrabbit 2 The Secret Files - Castle Earlong - 1.png");
            ImgChange = imgNum;
            PosX = x;
            PosY = y;
        }
    }
}
