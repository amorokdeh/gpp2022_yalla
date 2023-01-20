using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Block:GameObject
    {


        public Block(string name, int w, int h) : base(name, w, h)
        {
            Img.LoadImage("image/MiniPixelPack3/Maps/Level1.png");
            //ImgChange = 32;

        }
    }
}
