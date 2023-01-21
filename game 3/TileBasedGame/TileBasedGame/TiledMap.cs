using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using SDL2;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.ComponentModel;

namespace TileBasedGame
{
    class TiledMap
    {
        public int mapWidth;
        public int mapHeight;

        private int tileWidth;
        private int tileHeight;

        private GameObject MapImg;

        private List<Dictionary<string, object>> layersData = new List<Dictionary<string, object>>();

        private List<int> mapData = new List<int>();
        public int playerXPos;
        public int playerYPos;
        private List<Enemy> enemiesData = new List<Enemy>();


        public void load(string jsonFile, string imgSrc)
        {
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
                    if (layerName.Equals("Map") && layerData.ContainsKey("data"))
                    {

                        JArray dataArray = (JArray)layerData["data"];
                        foreach (var data in dataArray)
                        {

                            int number = (int)data;
                            mapData.Add(number);
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
                            enemiesData.Add(enemy);

                        }

                    }

                    //load Spikes
                    if (layerName.Equals("Spike") && layerData.ContainsKey("objects"))
                    {

                        JArray layerObject = (JArray)layerData["objects"];
                        foreach (var obj in layerObject)
                        {

                            var objX = obj["x"];
                            var objY = obj["y"];

                        }

                    }

                    Console.WriteLine(layerName);

                }
            }

        }

        public void build()
        {
            int line = 0;
            int col = 0;

            //build blocks
            foreach (int data in mapData)
            {

                int x = col * tileWidth;
                int y = line * tileHeight;

                Block block = new Block("Block", tileWidth, tileHeight, x, y, data + 32);
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

            //player position
            Program.Game.Player.PosX = playerXPos;
            Program.Game.Player.PosY = playerYPos;

            //build enemies
            foreach (Enemy enemy in enemiesData)
            {

            }
        }
    }
}
