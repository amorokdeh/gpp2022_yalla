using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Tile:GameObject
    {
        public int imgFrame;
        public Tile(string name, int w, int h, int x, int y, int imgNum) : base(name, w, h)
        {
            PosX = x;
            PosY = y;
            this.imgFrame = imgNum;

            //set image
            int line = 0;
            int col = 0;

            int i = 0;
            while (i < (imgNum - 10))
            {
                i += 10;
            }
            line = (i / 10);
            col = (imgNum - i) - 1;

            ImgChange = 32 * col;
            ImgChangeY = 32 * line;

            Program.Game._maps.currentMap.tiles.Add(this);
        }
    }
}
