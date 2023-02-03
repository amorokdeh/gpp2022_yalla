using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Tile:GameObject
    {
        public Tile(string name, int w, int h, int x, int y) : base(name, w, h)
        {
            this._name = name;
            PosX = x;
            PosY = y;
        }
    }
}
