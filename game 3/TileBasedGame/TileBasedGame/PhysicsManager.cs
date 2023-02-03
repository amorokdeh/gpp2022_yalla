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

        public PhysicsManager()
        {
        }




        internal Component CreateComponent()
        {
            PhysicsComponent pc = new PhysicsComponent(this);
            this._physicsComponents.Add(pc);
            return pc;
        }

        internal Component CreatePlayerComponent()
        {
            PlayerPhysicsComponent pc = new PlayerPhysicsComponent(this);
            this._physicsComponents.Add(pc);
            return pc;
        }

        public void Move(float deltaT)
        {
            foreach (var component in _physicsComponents)
            {
                if(component.GameObject.Active)
                    component.Move(deltaT);
                //Console.WriteLine(component.GameObject.GetType());
                if(component.GameObject is Player)
                {
                    component.CheckBorders();
                    //Console.WriteLine("Player");
                }
            }


        }

        public void clearObjects()
        {

            for (int i = 0; i < _physicsComponents.Count; i++)
            {
                _physicsComponents[i] = null;
            }
            _physicsComponents.Clear();
            GC.Collect();
        }

    }
}
