using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{

    class GameObjectManager
    {
        private List<GameObject> _gameObjects = new List<GameObject>();

        public GameObject CreateGameBackground(string name, int w, int h, int x=0, int y=0)
        {
            GameObject gameObject = new Background(name, w, h, x, y);
            _gameObjects.Add(gameObject);
            return gameObject;
        }

        public GameObject CreateGameShip(string name, int w, int h, int x, int y)
        {
            GameObject gameObject = new Ship(name, w, h);
            gameObject.PosX= x;
            gameObject.PosY= y;
            _gameObjects.Add(gameObject);
            return gameObject;
        }

        public GameObject CreateGameUfo(string name, int w, int h)
        {
            GameObject gameObject = new Ufo(name, w, h);
            _gameObjects.Add(gameObject);
            return gameObject;
        }

        public Player CreateGamePlayer(string name, int w, int h)
        {
            Player gameObject = new Player(name, w, h);
            _gameObjects.Add(gameObject);
            return gameObject;
        }

        public GameObject CreatePlayerBullet(string name,GameObject player, int w, int h)
        {
            GameObject gameObject = new Bullet(name,player, w, h);
            _gameObjects.Add(gameObject);
            return gameObject;
        }

        public GameObject CreateEnemyBullet(string name, GameObject enemy, int w, int h)
        {
            GameObject gameObject = new EnemyBullet(name, enemy, w, h);
            _gameObjects.Add(gameObject);
            return gameObject;
        }

        public Tile CreateTile(string name, int w, int h, int x, int y)
        {
            Tile gameObject = new Tile(name, w, h, x, y);
            //gameObject.Img = imgSrc;
            _gameObjects.Add(gameObject);
            return gameObject;
        }

        public Block CreateBlock(string name, int w, int h, int x, int y)
        {
            Block gameObject = new Block(name, w, h, x, y);
            //gameObject.Img = imgSrc;
            _gameObjects.Add(gameObject);
            return gameObject;
        }

        public Spike CreateSpike(string name, int w, int h, int x, int y)
        {
            Spike gameObject = new Spike(name, w, h, x, y);
            //gameObject.Img = imgSrc;
            _gameObjects.Add(gameObject);
            return gameObject;
        }
        
        public EndDoor CreateEndDoor(string name, int w, int h, int x, int y)
        {
            EndDoor gameObject = new EndDoor(name, w, h, x, y);
            //gameObject.Img = imgSrc;
            _gameObjects.Add(gameObject);
            return gameObject;
        }

        public void clearObjects() {

            for(int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Active= false;
                _gameObjects[i].Died= false;
                _gameObjects[i] = null;
            }
            _gameObjects.Clear();
            GC.Collect();
        }
    }


}
