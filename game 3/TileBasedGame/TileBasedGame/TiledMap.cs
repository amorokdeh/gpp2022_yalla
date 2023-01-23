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

        private GameObject MapImg;

        private List<Dictionary<string, object>> layersData;

        public int playerXPos;
        public int playerYPos;

        private List<int> backgroundData;
        private List<int> blocksData;
        private List<int> spikesData;
        private List<int> endDoorData;
        private List<Enemy> enemiesData;
        public List<GameObject> tiles = new List<GameObject>();

        public void load(string jsonFile, string imgSrc)
        {
            layersData = new List<Dictionary<string, object>>();
            backgroundData = new List<int>();
            blocksData = new List<int>();
            spikesData = new List<int>();
            endDoorData = new List<int>();
            enemiesData = new List<Enemy>();
            //load the JSON Tiled map file

            var json = File.ReadAllText(jsonFile);
            var map = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            //load tiles size (32 x 32)
            tileWidth = Convert.ToInt32(map["tilewidth"]);
            tileHeight = Convert.ToInt32(map["tileheight"]);

            //load map size
            mapWidth = Convert.ToInt32(map["width"]) * tileWidth;
            mapHeight = Convert.ToInt32(map["height"]) * tileHeight;

            //load map image
            MapImg = new GameObject("MapImg", tileWidth * 10, tileHeight * 65);
            MapImg.Img.LoadImage(imgSrc);

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

                    Tile tile = new Tile("Tile", tileWidth, tileHeight, x, y, data);
                    tile.Img = MapImg.Img;
                    Program.Game.BuildTiles(tile);

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

                Block block = new Block("Block", tileWidth, tileHeight, x, y, data);
                block.Img = MapImg.Img;
                Program.Game.BuildBlocks(block);

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

            //build blocks
            foreach (int data in spikesData)
            {

                int x = col * tileWidth;
                int y = line * tileHeight;

                Spike spike = new Spike("Spike", tileWidth, tileHeight, x, y, data);
                spike.Img = MapImg.Img;
                Program.Game.BuildSpikes(spike);

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

            //build blocks
            foreach (int data in endDoorData)
            {

                int x = col * tileWidth;
                int y = line * tileHeight;

                EndDoor endDoor = new EndDoor("End door", tileWidth, tileHeight, x, y, data);
                endDoor.Img = MapImg.Img;
                Program.Game.BuildEndDoor(endDoor);

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
                tiles[i].Died = false;
                tiles[i] = null;
            }
            tiles.Clear();
            GC.Collect();
        }
    }
}
