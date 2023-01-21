using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class PlayerPhysicsComponent : PhysicsComponent
    {
        public PlayerPhysicsComponent(PhysicsManager pm) : base(pm)
        {
        }
        public override void OnEvent(Event e)
        {

            HeroEvent he = e as HeroEvent;
            if (he == null)
                return;
            if (he.EventType == HeroEvent.Type.GoUp)
                GameObject.CurrentVelY -= GameObject.VelY;
            if (he.EventType == HeroEvent.Type.GoDown)
                GameObject.CurrentVelY += GameObject.VelY;
            if (he.EventType == HeroEvent.Type.GoLeft)
                GameObject.CurrentVelX -= GameObject.VelX;
            if (he.EventType == HeroEvent.Type.GoRight)
                GameObject.CurrentVelX += GameObject.VelX;
        }
        public override void Move(float deltaT)
        {
            base.Move(deltaT);
            Player p = (Player)GameObject;
            if (GameObject.CurrentVelX > 0)
            {
                p.FlyRight();
            }
            else if (GameObject.CurrentVelX < 0)
            {
                p.FlyLeft();
            }
            else
            {
                p.FlyStraight();
            }
                
        }
    }
}
