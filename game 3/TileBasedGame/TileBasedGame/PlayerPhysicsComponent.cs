using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
                moveUp();
            if (he.EventType == HeroEvent.Type.GoDown)
                moveDown();
            if (he.EventType == HeroEvent.Type.GoLeft)
                moveLeft();
            if (he.EventType == HeroEvent.Type.GoRight)
                moveRight();
            if (he.EventType == HeroEvent.Type.JumpAble)
                if (he.GameObject == this.GameObject)
                {
                    jumpAble();
                }
        }
        public override void Move(float deltaT)
        {
            base.Move(deltaT);
            /*
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
            }*/
                
        }

        public void moveUp() {

            if (GameObject.jumpPossibility > 0)
            {
                GameObject.CurrentVelY = Globals.JUMP_VELOCITY;
                GameObject.jumpPossibility--;
            }
            
        }
        public void moveDown()
        {
            if (GameObject.CurrentVelY <= 0)
            {
                GameObject.CurrentVelY += GameObject.VelY;
            }
        }

        public void moveLeft()
        {
            if(GameObject.CurrentVelX == 0)
                GameObject.direction = "left";

            if (GameObject.CurrentVelX >= 0)
            {
                GameObject.CurrentVelX -= GameObject.VelX;
            }
        }

        public void moveRight()
        {
            if (GameObject.CurrentVelX == 0)
                GameObject.direction = "right";

            if (GameObject.CurrentVelX <= 0)
            {
                GameObject.CurrentVelX += GameObject.VelX;
            }
        }
        public void jumpAble() {
            GameObject.jumpPossibility = 2;
        }

    }
}
