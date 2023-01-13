using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Background : GameObject
    {
        public Background(string name,int w, int h, int x = 0, int y = 0) : base(name, w, h)
        {
            switch (name) {
                case "level 1":  Img.LoadImage("image/MiniPixelPack3/SpaceBG.png"); break ;
                case "main menu": Img.LoadImage("image/MiniPixelPack3/mm.png"); break;
                case "level_2": Img.LoadImage("image/MiniPixelPack3/level_2.png"); break;
            }

            VelX = 0;
            VelY = 10;
            PosX = x;
            PosY = y;
        }
    }
}
