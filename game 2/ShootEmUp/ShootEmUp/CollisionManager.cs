using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class CollisionManager
    {
        List<CollisionComponent> _collisionComponents = new List<CollisionComponent>();

        internal Component CreateComponent()
        {
            CollisionComponent cc = new CollisionComponent(this);
            _collisionComponents.Add(cc);
            return cc;
        }


        public void Collide()
        {
            foreach (var component in _collisionComponents)
            {
                if (component.GameObject is Enemy && component.GameObject.Active)
                {
                    foreach (var colObject in _collisionComponents)

                        if ((colObject.GameObject is Bullet || colObject.GameObject is Player) && colObject.GameObject.Active)
                            component.Collide(colObject.GameObject);

                } 


                    
            }
        }
    }
}
