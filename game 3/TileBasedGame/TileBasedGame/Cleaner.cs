using System;

namespace TileBasedGame
{
    class Cleaner
    {
        public void clean() {

            Program.Game.Objects.clearObjects();
            Program.Game.Rendering.ClearObjects();
            Program.Game.Physics.clearObjects();
            Program.Game.Controls.clearObjects();
            Program.Game.Collisions.clearObjects();
            Program.Game.Ai.clearObjects();
            Program.Game.Pool.clearObjects();
            Program.Game.Shootings.clearObjects();
            Program.Game.Updates.clearObjects();
            Program.Game.Maps.clearObjects();
            GC.Collect();
        }
    }
}
