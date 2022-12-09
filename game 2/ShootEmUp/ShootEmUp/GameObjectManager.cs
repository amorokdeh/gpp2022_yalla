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


        public GameObject CreateGameBackground(string name)
        {
            GameObject gameObject = new Background(name);
            _gameObjects.Add(gameObject);
            return gameObject;
        }
        public GameObject CreateGameShip(string name)
        {
            GameObject gameObject = new Ship(name);
            _gameObjects.Add(gameObject);
            return gameObject;
        }

        public GameObject CreateGameUfo(string name)
        {
            GameObject gameObject = new Ufo(name);
            _gameObjects.Add(gameObject);
            return gameObject;
        }

        public GameObject CreateGamePlayer(string name)
        {
            GameObject gameObject = new Player(name);
            _gameObjects.Add(gameObject);
            return gameObject;
        }



    }

    
}
