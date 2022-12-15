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


        public virtual void Move(float deltaT)
        {
            GameObject.PosX += GameObject.CurrentVelX * deltaT;
            GameObject.PosY += GameObject.CurrentVelY * deltaT;

            if(GameObject is Player)
            {
                Player p = (Player)GameObject;
                if (GameObject.CurrentVelX > 0)
                {
                    p.FlyRight();
                } else if(GameObject.CurrentVelX < 0)
                {
                    p.FlyLeft();
                } else
                {
                    p.FlyStraight();
                }

                
            }

        }

        public void CheckBorders()
        {
            if(GameObject.PosX < 0 - 16)
            {
                GameObject.PosX = 0 - 16;
            } else if(GameObject.PosX > Program.window.width - 16)
            {
                GameObject.PosX = Program.window.width - 16;
            }
            if (GameObject.PosY < 0 - 16)
            {
                GameObject.PosY = 0 - 16;
            }
            else if (GameObject.PosY > Program.window.heigh - 16)
            {
                GameObject.PosY = Program.window.heigh - 16;
            }


        }
    }
}
