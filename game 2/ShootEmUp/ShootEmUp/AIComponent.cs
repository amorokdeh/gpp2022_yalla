using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class AIComponent : Component
    {
        AIManager AIManager;
        public AIComponent(AIManager am)
        {
            this.AIManager = am;
        }

        public void Control()
        {
            GameObject.CurrentVelY = GameObject.VelY;

            //nur zum Testen
            if(GameObject is Ufo && GameObject.PosY > 300)
            {
                GameObject.Active = false;
            }
        }
    }
}
