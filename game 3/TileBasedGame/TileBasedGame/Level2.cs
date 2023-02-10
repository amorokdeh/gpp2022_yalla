using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Level2:Level
    {
        public override void buildMap()
        {

            Program.Game._maps.loadMap("Level 2");
            Program.Game._maps.createMap();
        }
    }
}
