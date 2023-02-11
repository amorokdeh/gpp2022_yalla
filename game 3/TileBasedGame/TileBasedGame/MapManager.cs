using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class MapManager
    {
        public TiledMap currentMap = new TiledMap();

        public MapManager() { }

        public void loadMap(string level) {

            currentMap = new TiledMap();

            switch (level){

                case "Level 1": currentMap.load(Program.Game.Loader.Level1Json, Program.Game.Loader.Level1Img); break;
                case "Level 2": currentMap.load(Program.Game.Loader.Level2Json, Program.Game.Loader.Level2Img); break;
                case "Level 3": currentMap.load(Program.Game.Loader.Level3Json, Program.Game.Loader.Level3Img); break;
            }
        }

        public void createMap() {

            currentMap.buildBckground();
            currentMap.buildBlocks();
            currentMap.buildSpikes();
            currentMap.buildEnemies();
            currentMap.buildCoins();
            currentMap.buildPowers();
            currentMap.buildEndDoor();
            currentMap.resetPlayer();
        }

        public void clearObjects(){

            currentMap.clearMap();
            currentMap = null;
        }
    }

}
