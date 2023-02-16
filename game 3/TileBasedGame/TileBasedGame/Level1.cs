
namespace TileBasedGame
{
    class Level1:Level
    {
        public override void BuildMap() {

            Program.Game.Maps.loadMap("Level 1");
            Program.Game.Maps.createMap();
        }
    }
}
