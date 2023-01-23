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
        private GameObject mapImage;

        private String level1Json = "image/MiniPixelPack3/Maps/Level1.json";
        private String level1Img = "image/MiniPixelPack3/Maps/PC Computer - Jazz Jackrabbit 2 The Secret Files - Castle Earlong - 1.png";

        private String level2Json = "image/MiniPixelPack3/Maps/Level1.json";
        private String level2Img = "image/MiniPixelPack3/Maps/PC Computer - Jazz Jackrabbit 2 The Secret Files - Castle Earlong - 1.png";

        private String level3Json = "image/MiniPixelPack3/Maps/Level1.json";
        private String level3Img = "image/MiniPixelPack3/Maps/PC Computer - Jazz Jackrabbit 2 The Secret Files - Castle Earlong - 1.png";

        public MapManager() { }

        public void loadMap(string level) { 
            
            switch (level){

                case "Level 1": mapImage = new GameObject("Map Level 1", currentMap.tileWidth * 10, currentMap.tileHeight * 65); currentMap.load(level1Json, level1Img); break;
                case "Level 2": mapImage = new GameObject("Map Level 2", currentMap.tileWidth * 10, currentMap.tileHeight * 65); currentMap.load(level2Json, level2Img); break;
                case "Level 3": mapImage = new GameObject("Map Level 3", currentMap.tileWidth * 10, currentMap.tileHeight * 65); currentMap.load(level3Json, level3Img); break;
            }
        }

        public void createMap() {

            currentMap.buildBckground();
            currentMap.resetPlayer();
            currentMap.buildBlocks();
            currentMap.buildSpikes();
            currentMap.buildEndDoor();
        }

        public void clearObjects(){

            currentMap.clearMap();
        }
    }

}
