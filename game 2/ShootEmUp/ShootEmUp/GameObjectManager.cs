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

        public GameObject CreateGameObject(string name)
        {
            GameObject gameObject = new GameObject(name);
            _gameObjects.Add(gameObject);
            return gameObject;
        }



    }

    
}
