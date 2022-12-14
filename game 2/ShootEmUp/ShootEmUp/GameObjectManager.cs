using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{

    class GameObjectManager
    {
        List<GameObject> _gameObjects = new List<GameObject>();



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

        public GameObject CreateGamePlayer(string name, int w, int h)
        {
            GameObject gameObject = new Player(name, w, h);
            _gameObjects.Add(gameObject);
            return gameObject;
        }

        public GameObject CreatePlayerBullet(string name,GameObject player, int w, int h)
        {
            GameObject gameObject = new Bullet(name,player, w, h);
            _gameObjects.Add(gameObject);
            return gameObject;
        }



    }

    
}
