using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Cleaner
    {
        public void clean() {

            Program.Game._objects.clearObjects();
            Program.Game._rendering.clearObjects();
            Program.Game._physics.clearObjects();
            Program.Game._controls.clearObjects();
            Program.Game._collisions.clearObjects();
            Program.Game._ai.clearObjects();
            Program.Game._animations.clearObjects();
            Program.Game._pool.clearObjects();
            Program.Game._shootings.clearObjects();
            Program.Game._updates.clearObjects();
            Program.Game._maps.clearObjects();
        }
    }
}
