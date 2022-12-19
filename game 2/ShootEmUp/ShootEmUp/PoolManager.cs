using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class PoolManager
    {


        private GameObject _objectPrefab;
        private List<GameObject> _enemyUfoPool = new List<GameObject>();

        private List<GameObject> _enemyShipPool = new List<GameObject>();

        private List<GameObject> _playerBulletPool = new List<GameObject>();


        public PoolManager()
        {
            
        }


        public GameObject RequestEnemyUfo()
        {
            foreach (GameObject enemy in _enemyUfoPool)
            {
                if (enemy.Active == false)
                {
                    //Console.WriteLine("Old enemy");
                    enemy.Active = true;
                    return enemy;
                }
            }
            //Console.WriteLine("New enemy");
            GameObject newEnemy = Program.game.BuildUfo(_objectPrefab);
            newEnemy.Active = true;
            _enemyUfoPool.Add(newEnemy);

            return newEnemy;
        }

        public void DespawnEnemyUfo(GameObject enemy)
        {
            enemy.Active = false;
        }

        public GameObject RequestEnemyShip()
        {
            foreach (GameObject enemy in _enemyShipPool)
            {
                if (enemy.Active == false)
                {
                    //Console.WriteLine("Old enemy");
                    enemy.Active = true;
                    return enemy;
                }
            }
            //Console.WriteLine("New enemy");
            GameObject newEnemy = Program.game.BuildShip(_objectPrefab);
            newEnemy.Active = true;
            _enemyShipPool.Add(newEnemy);

            return newEnemy;
        }

        public void DespawnEnemyShip(GameObject enemy)
        {
            enemy.Active = false;
        }

        public GameObject RequestPlayerBullet(GameObject player)
        {
            foreach (GameObject bullet in _playerBulletPool)
            {
                if (bullet.Active == false)
                {
                    //Console.WriteLine("Old bullet");
                    bullet.Active = true;
                    return bullet;
                }
            }
            //Console.WriteLine("New bullet");
            GameObject newBullet = Program.game.BuildPlayerBullet(_objectPrefab, player);
            newBullet.Active = true;
            _playerBulletPool.Add(newBullet);

            return newBullet;
        }

        public void DespawnPlayerBullet(GameObject bullet)
        {
            bullet.Active = false;
        }
    }
}
