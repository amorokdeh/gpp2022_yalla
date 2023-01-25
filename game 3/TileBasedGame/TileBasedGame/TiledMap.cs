using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using SDL2;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.ComponentModel;
using System.Security.Policy;
using System.Data;
using System.Collections;

namespace TileBasedGame
{
    class TiledMap
    {
        public int mapWidth;
        public int mapHeight;

        public int tileWidth;
        public int tileHeight;

        private GameObject MapImg = new GameObject("MapImg", 32 * 10, 32 * 65);

        private List<Dictionary<string, object>> layersData = new List<Dictionary<string, object>>();

        public int playerXPos;
        public int playerYPos;

        private List<int> backgroundData = new List<int>();
        private List<int> blocksData = new List<int>();
        private List<int> spikesData = new List<int>();
        private List<int> endDoorData = new List<int>();
        private List<Enemy> enemiesData = new List<Enemy>();
        public List<GameObject> tiles = new List<GameObject>();

        public void load(Dictionary<string, object> map, Image img){

            //load tiles size (32 x 32)
            tileWidth = Convert.ToInt32(map["tilewidth"]);
            tileHeight = Convert.ToInt32(map["tileheight"]);

            //load map size
            mapWidth = Convert.ToInt32(map["width"]) * tileWidth;
            mapHeight = Convert.ToInt32(map["height"]) * tileHeight;

            //load map image
            
            MapImg.Img = img;

            //load layers data (Map and objects)

            if (map.ContainsKey("layers"))
            {

                JArray layersArray = (JArray)map["layers"];
                foreach (var layer in layersArray)
                {

                    layersData.Add(layer.ToObject<Dictionary<string, object>>());
                }

                foreach (var layerData in layersData)
                {

                    var layerName = layerData["name"];

                    //load map data
                    if (layerName.Equals("Background") && layerData.ContainsKey("data"))
                    {

                        JArray dataArray = (JArray)layerData["data"];
                        foreach (var data in dataArray)
                        {

                            int number = (int)data;
                            backgroundData.Add(number);
                        }
                    }

                    //load bolcks data
                    if (layerName.Equals("Blocks") && layerData.ContainsKey("data"))
                    {

                        JArray dataArray = (JArray)layerData["data"];
                        foreach (var data in dataArray)
                        {

                            int number = (int)data;
                            blocksData.Add(number);
                        }
                    }

                    //load player
                    if (layerName.Equals("Player") && layerData.ContainsKey("objects"))
                    {

                        JArray layerObject = (JArray)layerData["objects"];
                        foreach (var obj in layerObject)
                        {

                            var objX = obj["x"];
                            var objY = obj["y"];
                            playerXPos = Convert.ToInt32(objX);
                            playerYPos = Convert.ToInt32(objY);

                        }

                    }

                    //load Enemies
                    if (layerName.Equals("Enemy") && layerData.ContainsKey("objects"))
                    {

                        JArray layerObject = (JArray)layerData["objects"];
                        foreach (var obj in layerObject)
                        {

                            var objX = obj["x"];
                            var objY = obj["y"];
                            Enemy enemy = new Enemy("Enemy", tileWidth, tileHeight);
                            enemy.PosX = Convert.ToInt32(objX);
                            enemy.PosY = Convert.ToInt32(objY);
                            enemiesData.Add(enemy);

                        }

                    }

                    //load Spikes
                    if (layerName.Equals("Spike") && layerData.ContainsKey("data"))
                    {

                        JArray dataArray = (JArray)layerData["data"];
                        foreach (var data in dataArray)
                        {

                            int number = (int)data;
                            spikesData.Add(number);
                        }
                    }

                    //load End door
                    if (layerName.Equals("End") && layerData.ContainsKey("data"))
                    {

                        JArray dataArray = (JArray)layerData["data"];
                        foreach (var data in dataArray)
                        {

                            int number = (int)data;
                            endDoorData.Add(number);
                        }
                    }

                    Console.WriteLine(layerName);

                }
            }

        }

        public void buildBckground()
        {
            int line = 0;
            int col = 0;

            //build blocks
            foreach (int data in backgroundData){
                if (data != 0){

                    int x = col * tileWidth;
                    int y = line * tileHeight;
                    if (data != 0)
                    {
                        Tile tile = new Tile("Tile", tileWidth, tileHeight, x, y, data);
                        tile.Img = MapImg.Img;
                        Program.Game.BuildTiles(tile);
                    }
                    col++;
                    //new line
                    if (col == mapWidth / tileWidth)
                    {

                        line++;
                        col = 0;
                    }

                }
            }

        }

        public void buildBlocks()
        {
            int line = 0;
            int col = 0;

            //build blocks
            foreach (int data in blocksData)
            {
                
                int x = col * tileWidth;
                int y = line * tileHeight;
                if (data != 0)
                {

                    Block block = new Block("Block", tileWidth, tileHeight, x, y, data);
                    block.Img = MapImg.Img;
                    Program.Game.BuildBlocks(block);
                }
                col++;
                //new line
                if (col == mapWidth / tileWidth)
                {

                    line++;
                    col = 0;
                }
                
            }

        }

        public void buildSpikes()
        {
            int line = 0;
            int col = 0;

            //build spikes
            foreach (int data in spikesData)
            {

                int x = col * tileWidth;
                int y = line * tileHeight;
                if (data != 0)
                {
                    Spike spike = new Spike("Spike", tileWidth, tileHeight, x, y, data);
                    spike.Img = MapImg.Img;
                    Program.Game.BuildSpikes(spike);
                }

                col++;
                //new line
                if (col == mapWidth / tileWidth)
                {

                    line++;
                    col = 0;
                }

            }

        }
        public void buildEndDoor()
        {
            int line = 0;
            int col = 0;

            //build end door
            foreach (int data in endDoorData)
            {

                int x = col * tileWidth;
                int y = line * tileHeight;
                if (data != 0) {
                    EndDoor endDoor = new EndDoor("End door", tileWidth, tileHeight, x, y, data);
                    endDoor.Img = MapImg.Img;
                    Program.Game.BuildEndDoor(endDoor);
                }

                col++;
                //new line
                if (col == mapWidth / tileWidth)
                {

                    line++;
                    col = 0;
                }

            }

        }

        public void resetPlayer() {

            Program.Game.Player = (Player) Program.Game.BuildPlayer();
            Program.Game.Player.Reset();
            Program.Game.Player.PosX = playerXPos;
            Program.Game.Player.PosY = playerYPos;
        }

        public void buildEnemies() {

            foreach (Enemy enemy in enemiesData) {
            
            }
        }

        public void clearMap() {


            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].Active = false;
                tiles[i].Died = true;
                tiles[i] = null;
                
            }

            for (int i = 0; i < enemiesData.Count; i++)
            {
                enemiesData[i].Active = false;
                enemiesData[i].Died = true;
                enemiesData[i] = null;
            }

            tiles.Clear();
            backgroundData.Clear();
            blocksData.Clear();
            spikesData.Clear();
            endDoorData.Clear();
            enemiesData.Clear();

            tiles = null;
            backgroundData = null;
            blocksData = null;
            spikesData = null;
            endDoorData = null;
            enemiesData = null;


            GC.Collect();

        }

    }
}
