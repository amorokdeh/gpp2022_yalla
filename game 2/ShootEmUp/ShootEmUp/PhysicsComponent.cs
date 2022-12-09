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

        public void Move(float deltaT)
        {
            GameObject.PosX += GameObject.CurrentVelX * deltaT;
            GameObject.PosY += GameObject.CurrentVelY * deltaT;
        }
    }
}
