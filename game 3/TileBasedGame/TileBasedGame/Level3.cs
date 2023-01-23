using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Level3:Level
    {

        public override void buildMap()
        {

            Program.Game._maps.loadMap("Level 3");
            Program.Game._maps.createMap();
        }

    }
}
