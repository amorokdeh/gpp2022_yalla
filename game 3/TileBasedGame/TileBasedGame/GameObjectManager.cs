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

        public GameObject CreateGameShip(string name, int w, int h)
        {
            GameObject gameObject = new Ship(name, w, h);
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

        public Tile CreateTile(string name, int w, int h, int x, int y, Image imgSrc, int imgFrame)
        {
            Tile gameObject = new Tile(name, w, h, x, y, imgFrame);
            gameObject.Img = imgSrc;
            _gameObjects.Add(gameObject);
            return gameObject;
        }

        public Block CreateBlock(string name, int w, int h, int x, int y, Image imgSrc, int imgFrame)
        {
            Block gameObject = new Block(name, w, h, x, y, imgFrame);
            gameObject.Img = imgSrc;
            _gameObjects.Add(gameObject);
            return gameObject;
        }

    }


}
