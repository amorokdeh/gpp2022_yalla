using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    
    class PhysicsManager
    {
        List<PhysicsComponent> _physicsComponents = new List<PhysicsComponent>();
        

        internal Component CreateComponent()
        {
            PhysicsComponent pc = new PhysicsComponent(this);
            this._physicsComponents.Add(pc);
            return pc;
        }

    }
}
