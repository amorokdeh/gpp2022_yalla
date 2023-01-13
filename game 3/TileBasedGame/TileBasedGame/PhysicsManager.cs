using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    
    class PhysicsManager
    {
        private List<PhysicsComponent> _physicsComponents = new List<PhysicsComponent>();
        

        internal Component CreateComponent()
        {
            PhysicsComponent pc = new PhysicsComponent(this);
            this._physicsComponents.Add(pc);
            return pc;
        }

        internal Component CreateBGComponent()
        {
            PhysicsBGComponent pc = new PhysicsBGComponent(this);
            this._physicsComponents.Add(pc);
            return pc;
        }

        public void Move(float deltaT)
        {
            foreach (var component in _physicsComponents)
            {
                component.Move(deltaT);
                //Console.WriteLine(component.GameObject.GetType());
                if(component.GameObject is Player)
                {
                    component.CheckBorders();
                    //Console.WriteLine("Player");
                }
            }


        }

    }
}
