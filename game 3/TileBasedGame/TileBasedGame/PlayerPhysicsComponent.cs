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
            if (he.EventType == HeroEvent.Type.StopMoving)
                stopMoving();
            if (he.EventType == HeroEvent.Type.TryShooting)
                shoot();
            if (he.EventType == HeroEvent.Type.powerUp)
                powerUp();
            if (he.EventType == HeroEvent.Type.Hurt) {
                stopMoving();
                GameObject.hurt = true;
            }
            if (he.EventType == HeroEvent.Type.JumpAble)
                if (he.GameObject == this.GameObject)
                {
                    jumpAble();
                }
        }
        public override void Move(float deltaT)
        {
            base.Move(deltaT);

            if (GameObject.hurt) {
                hurt();
            }
        }

        public void moveUp() {

            if (GameObject.jumpPossibility > 0 && !GameObject.hurt)
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
            if(GameObject.CurrentVelX == 0 && !GameObject.hurt)
                GameObject.direction = "left";

            if (GameObject.CurrentVelX >= 0 && !GameObject.hurt)
            {
                GameObject.CurrentVelX -= GameObject.VelX;
            }
        }

        public void moveRight()
        {
            if (GameObject.CurrentVelX == 0 && !GameObject.hurt)
                GameObject.direction = "right";

            if (GameObject.CurrentVelX <= 0 && !GameObject.hurt)
            {
                GameObject.CurrentVelX += GameObject.VelX;
            }
        }

        public void stopMoving() 
        {
            if (GameObject.CurrentVelX < 0 && GameObject.direction == "left")
            {
                GameObject.CurrentVelX = 0;
            }
            if (GameObject.CurrentVelX > 0 && GameObject.direction == "right")
            {
                GameObject.CurrentVelX = 0;
            }
        }

        public void jumpAble() {
            GameObject.jumpPossibility = 2;
        }

        public void shoot() {
            if (GameObject.canShoot) {
                GameObject.shoot = true;
            }
        }

        public void powerUp() {
            GameObject.shootingSpeed += Globals.BulletPowerUp;
        }

        public void hurt() {

            GameObject.jumpPossibility = 0;
            GameObject.CurrentVelY = - Globals.HurtChangePosY;

            if (GameObject.direction == "right" && GameObject.hurtAmount < Globals.NormalHurtAmount)
            {
                GameObject.PosX -= Globals.HurtChangePosX;
                //GameObject.PosY -= Globals.HurtChangePosY;
                GameObject.hurtAmount++;
            }
            else if (GameObject.direction == "left" && GameObject.hurtAmount < Globals.NormalHurtAmount)
            {
                GameObject.PosX += Globals.HurtChangePosX;
                //GameObject.PosY -= Globals.HurtChangePosY;
                GameObject.hurtAmount++;
            }
            else {
                GameObject.hurt = false;
                GameObject.hurtAmount = 0;
            }
        }

    }
}
