using System.Collections.Generic;

namespace TileBasedGame
{
    class PoolManager
    {
        private GameObject _objectPrefab;
        private List<GameObject> _enemyUfoPool = new List<GameObject>();
        private List<GameObject> _enemyShipPool = new List<GameObject>();
        private List<GameObject> _playerBulletPool = new List<GameObject>();
        private List<GameObject> _enemyBulletPool = new List<GameObject>();

        public PoolManager() {}

        public void DespawnEnemy(GameObject enemy)
        {
            Enemy enemy1 = (Enemy)enemy;
            enemy1.Active = false;
            enemy1.Died = false;

            enemy1.ExplosionStep = Globals.Reset;
        }
   
        public GameObject RequestPlayerBullet(GameObject player)
        {
            foreach (GameObject bullet in _playerBulletPool)
            {
                if (bullet.Active == false)
                {
                    bullet.Active = true;
                    return bullet;
                }
            }
            GameObject newBullet = Program.Game.BuildPlayerBullet(_objectPrefab, player);
            newBullet.Active = true;
            _playerBulletPool.Add(newBullet);

            return newBullet;
        }
        //Enemy Bullet
        public GameObject RequestEnemyBullet(GameObject enemy)
        {
            foreach (GameObject bullet in _enemyBulletPool)
            {
                if (bullet.Active == false)
                {
                    bullet.Active = true;
                    return bullet;
                }
            }
            GameObject newBullet = Program.Game.BuildEnemyBullet(_objectPrefab, enemy);
            newBullet.Active = true;
            _enemyBulletPool.Add(newBullet);

            return newBullet;
        }

        public void DespawnPlayerBullet(GameObject bullet)
        {
            bullet.Active = false;
        }

        public void DespawnEnemyBullet(GameObject bullet)
        {
            bullet.Active = false;
        }

        public void DespawnBullet(GameObject bullet)
        {
            bullet.Active = false;
        }

        public void SetInactive()
        {
            foreach (var gameObject in _enemyUfoPool)
            {
                DespawnEnemy(gameObject);
            }
            foreach (var gameObject in _enemyShipPool)
            {
                DespawnEnemy(gameObject);
            }
            foreach (var gameObject in _playerBulletPool)
            {
                gameObject.Active = false;
                gameObject.Died = false;
            }
            foreach (var gameObject in _enemyBulletPool)
            {
                gameObject.Active = false;
                gameObject.Died = false;
            }
        }

        public void clearObjects()
        {

            for (int i = 0; i < _enemyUfoPool.Count; i++)
            {
                _enemyUfoPool[i] = null;
            }
            _enemyUfoPool.Clear();


            for (int i = 0; i < _enemyShipPool.Count; i++)
            {
                _enemyShipPool[i] = null;
            }
            _enemyShipPool.Clear();


            for (int i = 0; i < _playerBulletPool.Count; i++)
            {
                _playerBulletPool[i] = null;
            }
            _playerBulletPool.Clear();


            for (int i = 0; i < _enemyBulletPool.Count; i++)
            {
                _enemyBulletPool[i] = null;
            }
            _enemyBulletPool.Clear();
        }
    }
}
