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

            Program.Game.Objects.clearObjects();
            Program.Game.Rendering.clearObjects();
            Program.Game.Physics.clearObjects();
            Program.Game.Controls.clearObjects();
            Program.Game.Collisions.clearObjects();
            Program.Game.Ai.clearObjects();
            Program.Game.Animations.clearObjects();
            Program.Game.Pool.clearObjects();
            Program.Game.Shootings.clearObjects();
            Program.Game.Updates.clearObjects();
            Program.Game.Maps.clearObjects();
        }
    }
}
