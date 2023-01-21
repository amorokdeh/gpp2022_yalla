using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Block : GameObject
    {

        public int imgFrame;
        public Block(string name, int w, int h, int x, int y, int imgNum) : base(name, w, h)
        {
            PosX = x;
            PosY = y;
            this.imgFrame = imgNum;
            //Console.WriteLine(imgNum);
            //set image
            int line = 0;
            int col = 0;

            int i = 0;
            while (i < (imgNum - 10)){
                i += 10;
            }
            line = (i / 10);
            col = (imgNum - i) - 1;
            //Console.WriteLine(imgNum);
            Console.WriteLine(line);

            ImgChange = 32 * col;
            ImgChangeY = 32 * line;
        }
    }
}
