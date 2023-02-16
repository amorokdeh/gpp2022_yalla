using System.Collections.Generic;

namespace TileBasedGame
{
    class CollisionManager
    {
        private List<CollisionComponent> _collisionComponents = new List<CollisionComponent>();

        internal Component CreateComponent(string role)
        {
            CollisionComponent cc = new CollisionComponent(this, role);
            _collisionComponents.Add(cc);
            return cc;
        }

        public void Collide(float deltaT)
        {
            foreach (var component in _collisionComponents)
            {
                if (!component.GameObject.Died && component.GameObject.Active)
                //if (component.GameObject.Active)
                {
                    foreach (var colObject in _collisionComponents)
                    {
                        if ((!colObject.GameObject.Died && colObject.GameObject.Active) && colObject.GameObject != component.GameObject)
                        //if ((component.GameObject.Active) && colObject.GameObject != component.GameObject)
                        {

                            component.Collide(colObject, deltaT);

                        }
                    }                       
                }
                component.OldX = -1;
                component.OldY = -1;
            }
        }

        public void clearObjects()
        {

            for (int i = 0; i < _collisionComponents.Count; i++)
            {
                _collisionComponents[i] = null;
            }
            _collisionComponents.Clear();
            //GC.Collect();
        }
    }
}
