using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class PhysicsComponent : Component
    {
        State Next;
        PhysicsManager PhysicsManager;
        public PhysicsComponent(PhysicsManager pm):base()
        {
            this.PhysicsManager = pm;
        }

        public override void OnEvent(Event e)
        {
            HeroEvent he = e as HeroEvent;
            
            if (he != null) {                 
                if (he.GameObject == GameObject)
                {
                    if (he.GameObject is GameObject)
                    {
                        if (he.EventType == HeroEvent.Type.TryShooting)
                            Shoot();
                        if (he.EventType == HeroEvent.Type.powerUp)
                            PowerUp();
                    }
                }
            }

            MovingEvent me = e as MovingEvent;
            if (me == null)
                return;
            if (me.GameObject == GameObject)
            {
                if (GameObject.State != null) 
                { 
                    Next = GameObject.State.HandleInput(me);

                    if (Next != GameObject.State)
                    {
                        GameObject.State = Next;
                        GameObject.State.Enter(GameObject);
                    }
                }
            }
        }



        public virtual void Move(float deltaT)
        {
            if(GameObject.State != null)
                GameObject.State.Update(deltaT);

            GameObject.PosX += GameObject.CurrentVelX * deltaT;
            GameObject.PosY += GameObject.CurrentVelY * deltaT;

            //Gravity
            if (GameObject.CurrentVelY < 500)
            {
                GameObject.CurrentVelY += Globals.Gravity * deltaT;
            }


        }

        public void CheckBorders()
        {
            int mapWidth = Program.Game.Maps.currentMap.MapWidth;
            int mapHeight = Program.Game.Maps.currentMap.MapHeight;

            if (GameObject.PosX < 0)
            {
                GameObject.PosX = 0;

            } else if(GameObject.PosX > mapWidth - GameObject.Width)
            {
                GameObject.PosX = mapWidth - GameObject.Width;
            }
            if (GameObject.PosY < 0)
            {
                GameObject.PosY = 0;
            }
            else if (GameObject.PosY > mapHeight - GameObject.Height)
            {
                GameObject.PosY = mapHeight - GameObject.Height;
            }


        }
        public void StopMoving()
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

        public void JumpAble()
        {
            GameObject.JumpPossibility = 2;
        }

        public void Shoot()
        {
            if (GameObject.CanShoot)
            {
                GameObject.Shoot = true;
            }
        }

        public void PowerUp()
        {
            GameObject.ShootingSpeed += Globals.BulletPowerUp;
        }

        public void Hurt()
        {

            //GameObject.jumpPossibility = 0;
            GameObject.CurrentVelY = -Globals.HurtChangePosY;

            if (GameObject.direction == "right" && GameObject.HurtAmount < Globals.NormalHurtAmount)
            {
                GameObject.PosX -= Globals.HurtChangePosX;
                //GameObject.PosY -= Globals.HurtChangePosY;
                GameObject.HurtAmount++;
            }
            else if (GameObject.direction == "left" && GameObject.HurtAmount < Globals.NormalHurtAmount)
            {
                GameObject.PosX += Globals.HurtChangePosX;
                //GameObject.PosY -= Globals.HurtChangePosY;
                GameObject.HurtAmount++;
            }
            else
            {
                GameObject.Hurt = false;
                GameObject.HurtAmount = 0;
            }
        }

    }
}




