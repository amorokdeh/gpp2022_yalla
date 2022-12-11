using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class PhysicsBGComponent : PhysicsComponent
    {

        public PhysicsBGComponent(PhysicsManager pm):base(pm)
        {

        }

        public override void Move(float deltaT)
        {
            GameObject.PosX += GameObject.CurrentVelX * deltaT;
            GameObject.PosY += GameObject.CurrentVelY * deltaT;
            if(GameObject.PosY > 64 * 8)
            {
                GameObject.PosY = GameObject.PosX - 64 * 4;
            }
        }
    }
}
