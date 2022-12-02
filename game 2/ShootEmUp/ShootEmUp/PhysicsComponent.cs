using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class PhysicsComponent : Component
    {
        PhysicsManager PhysicsManager;
        public PhysicsComponent(PhysicsManager pm)
        {
            this.PhysicsManager = pm;
        }
    }
}
