﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Level1:Level
    {

        public override void buildMap() {

            Program.Game._maps.loadMap("Level 1");
            Program.Game._maps.createMap();
        }

    }
}
