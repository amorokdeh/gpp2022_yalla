using System.Collections.Generic;

namespace TileBasedGame
{
    class ShootingManager
    {
        private List<ShootingComponent> _shootingComponents = new List<ShootingComponent>();

        internal Component CreatePlayerComponent()
        {
            PlayerShootingComponent sc = new PlayerShootingComponent(this);
            _shootingComponents.Add(sc);
            return sc;
        }
        internal Component CreateEnemyComponent()
        {
            EnemyShootingComponent sc = new EnemyShootingComponent(this);
            _shootingComponents.Add(sc);
            return sc;
        }

        public void Shoot(float deltaTime)
        {
            foreach (var component in _shootingComponents)
            {
                if(component.GameObject.Active && !component.GameObject.Died)
                    component.Shoot(deltaTime);
            }
        }

        public void clearObjects()
        {

            for (int i = 0; i < _shootingComponents.Count; i++)
            {
                _shootingComponents[i] = null;
            }
            _shootingComponents.Clear();
        }
    }
}
