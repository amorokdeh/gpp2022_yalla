using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Level3:Level
    {
        public override void BuildMap()
        {

            Program.Game.Maps.loadMap("Level 3");
            Program.Game.Maps.createMap();
        }
    }
}
