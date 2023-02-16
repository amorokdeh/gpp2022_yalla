
namespace TileBasedGame
{
    class MapManager
    {
        public TiledMap currentMap = new TiledMap();

        public MapManager() { }

        public void loadMap(string level) {

            currentMap = new TiledMap();

            switch (level){

                case "Level 1": currentMap.Load(Program.Game.Loader.Level1Json, Program.Game.Loader.Level1Img); break;
                case "Level 2": currentMap.Load(Program.Game.Loader.Level2Json, Program.Game.Loader.Level2Img); break;
                case "Level 3": currentMap.Load(Program.Game.Loader.Level3Json, Program.Game.Loader.Level3Img); break;
            }
        }

        public void createMap() {

            currentMap.BuildBackground();
            currentMap.BuildBlocks();
            currentMap.BuildSpikes();
            currentMap.BuildEnemies();
            currentMap.BuildCoins();
            currentMap.BuildPowers();
            currentMap.BuildEndDoor();
            currentMap.ResetPlayer();
            Program.Game.InfoBox.levelInfoRunning = true;
        }

        public void clearObjects(){

            currentMap.ClearMap();
            currentMap = null;
        }
    }

}
