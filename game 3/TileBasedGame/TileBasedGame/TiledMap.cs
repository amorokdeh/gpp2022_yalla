using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;


namespace TileBasedGame
{
    class TiledMap
    {
        public int MapWidth;
        public int MapHeight;

        public int TileWidth;
        public int TileHeight;

        Image Image;

        private List<Dictionary<string, object>> _layersData = new List<Dictionary<string, object>>();

        public int PlayerXPos;
        public int PlayerYPos;

        private List<int> _backgroundData = new List<int>();
        private List<int> _blocksData = new List<int>();
        private List<int> _spikesData = new List<int>();
        private List<int> _endDoorData = new List<int>();
        private List<Enemy> _enemiesData = new List<Enemy>();
        private List<Coin> _coinsData = new List<Coin>();
        private List<Power> _powerData = new List<Power>();
        public List<GameObject> Tiles = new List<GameObject>();

        public void Load(Dictionary<string, object> map, Image img){

            //load tiles size (32 x 32)
            TileWidth = Convert.ToInt32(map["tilewidth"]);
            TileHeight = Convert.ToInt32(map["tileheight"]);

            //load map size
            MapWidth = Convert.ToInt32(map["width"]) * TileWidth;
            MapHeight = Convert.ToInt32(map["height"]) * TileHeight;

            //load map image
            Image = img;

            //load layers data (Map and objects)

            if (map.ContainsKey("layers"))
            {
                JArray layersArray = (JArray)map["layers"];
                foreach (var layer in layersArray)
                {

                    _layersData.Add(layer.ToObject<Dictionary<string, object>>());
                }

                foreach (var layerData in _layersData)
                {

                    var layerName = layerData["name"];

                    //load map data
                    if (layerName.Equals("Background") && layerData.ContainsKey("data"))
                    {

                        JArray dataArray = (JArray)layerData["data"];
                        foreach (var data in dataArray)
                        {
                            int number = (int)data;
                            _backgroundData.Add(number);
                        }
                    }

                    //load bolcks data
                    if (layerName.Equals("Blocks") && layerData.ContainsKey("data"))
                    {
                        JArray dataArray = (JArray)layerData["data"];
                        foreach (var data in dataArray)
                        {

                            int number = (int)data;
                            _blocksData.Add(number);
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
                            PlayerXPos = Convert.ToInt32(objX);
                            PlayerYPos = Convert.ToInt32(objY);
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
                            Enemy enemy = new Enemy("Enemy", TileWidth, TileHeight);
                            enemy.PosX = Convert.ToInt32(objX);
                            enemy.PosY = Convert.ToInt32(objY);
                            _enemiesData.Add(enemy);
                        }
                    }
                    //load Coins
                    if (layerName.Equals("Coin") && layerData.ContainsKey("objects"))
                    {
                        JArray layerObject = (JArray)layerData["objects"];
                        foreach (var obj in layerObject)
                        {
                            var objX = obj["x"];
                            var objY = obj["y"];
                            Coin coin = new Coin("Coin", TileWidth, TileHeight);
                            coin.PosX = Convert.ToInt32(objX);
                            coin.PosY = Convert.ToInt32(objY);
                            _coinsData.Add(coin);
                        }
                    }
                    //load Coins
                    if (layerName.Equals("Power") && layerData.ContainsKey("objects"))
                    {
                        JArray layerObject = (JArray)layerData["objects"];
                        foreach (var obj in layerObject)
                        {
                            var objX = obj["x"];
                            var objY = obj["y"];
                            Power power = new Power("Power", TileWidth, TileHeight);
                            power.PosX = Convert.ToInt32(objX);
                            power.PosY = Convert.ToInt32(objY);
                            _powerData.Add(power);
                        }
                    }

                    //load Spikes
                    if (layerName.Equals("Spike") && layerData.ContainsKey("data"))
                    {
                        JArray dataArray = (JArray)layerData["data"];
                        foreach (var data in dataArray)
                        {

                            int number = (int)data;
                            _spikesData.Add(number);
                        }
                    }

                    //load End door
                    if (layerName.Equals("End") && layerData.ContainsKey("data"))
                    {
                        JArray dataArray = (JArray)layerData["data"];
                        foreach (var data in dataArray)
                        {

                            int number = (int)data;
                            _endDoorData.Add(number);
                        }
                    }
                }
            }

        }

        public void BuildBackground()
        {
            int line = 0;
            int col = 0;

            //build blocks
            foreach (int data in _backgroundData){
                if (data != 0){

                    int x = col * TileWidth;
                    int y = line * TileHeight;
                    if (data != 0)
                    {
                        Program.Game.BuildTiles("Tile", TileWidth, TileHeight, x, y, data, Image);
                    }
                    col++;
                    //new line
                    if (col == MapWidth / TileWidth)
                    {
                        line++;
                        col = 0;
                    }
                }
            }
        }

        public void BuildBlocks()
        {
            int line = 0;
            int col = 0;

            //build blocks
            foreach (int data in _blocksData)
            { 
                int x = col * TileWidth;
                int y = line * TileHeight;
                if (data != 0)
                {
                    Program.Game.BuildBlocks("Block", TileWidth, TileHeight, x, y, data, Image);
                }
                col++;
                //new line
                if (col == MapWidth / TileWidth)
                {
                    line++;
                    col = 0;
                }               
            }
        }

        public void BuildSpikes()
        {
            int line = 0;
            int col = 0;

            //build spikes
            foreach (int data in _spikesData)
            {
                int x = col * TileWidth;
                int y = line * TileHeight;
                if (data != 0)
                {
                    Program.Game.BuildSpikes("Block", TileWidth, TileHeight, x, y, data, Image);
                }

                col++;
                //new line
                if (col == MapWidth / TileWidth)
                {
                    line++;
                    col = 0;
                }
            }
        }
        public void BuildEndDoor()
        {
            int line = 0;
            int col = 0;

            //build end door
            foreach (int data in _endDoorData)
            {
                int x = col * TileWidth;
                int y = line * TileHeight;
                if (data != 0) {
                    Program.Game.BuildEndDoor("End door", TileWidth, TileHeight, x, y, data, Image);
                }

                col++;
                //new line
                if (col == MapWidth / TileWidth)
                {
                    line++;
                    col = 0;
                }
            }
        }

        public void ResetPlayer() {

            Program.Game.Player = (Player) Program.Game.BuildPlayer();
            Program.Game.Player.Reset();
            Program.Game.Player.PosX = PlayerXPos;
            Program.Game.Player.PosY = PlayerYPos -5;

        }

        public void BuildEnemies() {
            int i = 0;
            foreach (Enemy enemy in _enemiesData) {
                if (i == 0)
                {
                    Program.Game.BuildEnemy(enemy, 1);
                    i++;
                }
                else {
                    Program.Game.BuildEnemy(enemy, 2);
                    i = 0;
                }                
            }
        }
        public void BuildCoins()
        {
            int line = 0;
            int col = 0;
            foreach (Coin coin in _coinsData)
            {
                int x = col * TileWidth;
                int y = line * TileHeight;
                Program.Game.BuildCoin(coin, x, y);
                col++;
                //new line
                if (col == 3)
                {
                    line++;
                    col = 0;
                }
                if (line == 3)
                {
                    line = 0;
                }
            }
        }
        public void BuildPowers()
        {
            foreach (Power power in _powerData)
            {
                Program.Game.BuildPower(power);
            }
        }

        public void ClearMap() {


            for (int i = 0; i < Tiles.Count; i++)
            {
                Tiles[i].Active = false;
                Tiles[i].Died = true;
                Tiles[i] = null;                
            }

            for (int i = 0; i < _enemiesData.Count; i++)
            {
                _enemiesData[i].Active = false;
                _enemiesData[i].Died = true;
                _enemiesData[i] = null;
            }

            for (int i = 0; i < _coinsData.Count; i++)
            {
                _coinsData[i].Active = false;
                _coinsData[i].Died = true;
                _coinsData[i] = null;
            }

            for (int i = 0; i < _powerData.Count; i++)
            {
                _powerData[i].Active = false;
                _powerData[i].Died = true;
                _powerData[i] = null;
            }

            Tiles.Clear();
            _backgroundData.Clear();
            _blocksData.Clear();
            _spikesData.Clear();
            _endDoorData.Clear();
            _enemiesData.Clear();
            _coinsData.Clear();
            _powerData.Clear();

            Tiles = null;
            _backgroundData = null;
            _blocksData = null;
            _spikesData = null;
            _endDoorData = null;
            _enemiesData = null;
            _coinsData = null;
            _powerData = null;

            _layersData.Clear();
            _layersData = null;

            MessageBus.Clean();
        }
    }
}
