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

                case "Level 1": currentMap.load(Program.Game.loader.level1Json, Program.Game.loader.level1Img); break;
                case "Level 2": currentMap.load(Program.Game.loader.level2Json, Program.Game.loader.level2Img); break;
                case "Level 3": currentMap.load(Program.Game.loader.level3Json, Program.Game.loader.level3Img); break;
            }
        }

        public void createMap() {

            currentMap.buildBckground();
            currentMap.buildBlocks();
            currentMap.buildSpikes();
            currentMap.buildEndDoor();
            currentMap.resetPlayer();
        }

        public void clearObjects(){

            currentMap.clearMap();
            currentMap = null;
        }
    }

}
