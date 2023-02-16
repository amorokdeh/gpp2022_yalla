using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Block : Tile
    {
        public Block(string name, int w, int h, int x, int y) : base(name, w, h, x, y) {}
    }
}
