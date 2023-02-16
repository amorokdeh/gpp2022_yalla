using System.Collections.Generic;

namespace TileBasedGame
{
    class UpdateManager
    {
        private List<UpdateComponent> _updateComponents = new List<UpdateComponent>();

        internal Component CreatePlayerComponent()
        {
            PlayerUpdateComponent uc = new PlayerUpdateComponent(this);
            _updateComponents.Add(uc);
            return uc;
        }
        internal Component CreateBulletComponent()
        {
            BulletUpdateComponent uc = new BulletUpdateComponent(this);
            _updateComponents.Add(uc);
            return uc;
        }
        internal Component CreateEnemyComponent()
        {
            EnemyUpdateComponent uc = new EnemyUpdateComponent(this);
            _updateComponents.Add(uc);
            return uc;
        }

        public void DoUpdate()
        {
            foreach (var component in _updateComponents)
            {
                if (component.GameObject.Active && !component.GameObject.Died)
                    component.DoUpdate();
            }
        }

        public void clearObjects()
        {

            for (int i = 0; i < _updateComponents.Count; i++)
            {
                _updateComponents[i] = null;
            }
            _updateComponents.Clear();
            //GC.Collect();
        }
    }
}
