using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class PoolManager
    {

        private int _defaultPoolSize = 10;

        private GameObject _enemyPrefab;
        private List<GameObject> _enemyPool = new List<GameObject>();

        //private GameObjectManager _enemies = new GameObjectManager();

        public PoolManager()
        {
            
        }





        public GameObject RequestEnemy()
        {
            foreach (GameObject enemy in _enemyPool)
            {
                if (enemy.Active == false)
                {
                    Console.WriteLine("Old enemy");
                    enemy.Active = true;
                    return enemy;
                }
            }
            Console.WriteLine("New enemy");
            GameObject newEnemy = Program.game.BuildUfo(_enemyPrefab);
            newEnemy.Active = true;
            _enemyPool.Add(newEnemy);

            return newEnemy;
        }

        public void DespawnEnemy(GameObject enemy)
        {
            enemy.Active = false;
        }
    }
}
