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

        State next;
        public PlayerPhysicsComponent(PhysicsManager pm) : base(pm)
        {
            
        }



        public override void OnEvent(Event e)
        {

            HeroEvent he = e as HeroEvent;
            if (he == null)
                return;
            if (he.EventType == HeroEvent.Type.TryShooting)
                shoot();
            if (he.EventType == HeroEvent.Type.powerUp)
                powerUp();
           /* if (he.EventType == HeroEvent.Type.JumpAble)
                if (he.GameObject == this.GameObject)
                {
                    jumpAble();
                }*/
            next = GameObject.State.HandleInput(he);

            //Console.WriteLine(next);
            if (next != GameObject.State)
            {
                GameObject.State = next;
                GameObject.State.Enter(GameObject);
            }

            /*   if (he == null)
                   return;
               if (he.GameObject == this.GameObject)
               {

                   if (he.EventType == HeroEvent.Type.FlyLeft)
                   {
                       ImgChange = Globals.MediumImageSize * 3;
                       flipped = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
                   }
                   else if (he.EventType == HeroEvent.Type.FlyRight)
                   {
                       ImgChange = Globals.MediumImageSize * 3;
                       flipped = SDL.SDL_RendererFlip.SDL_FLIP_NONE;
                   }
                   else if (he.EventType == HeroEvent.Type.FlyStraight)
                   {

                       if (GameObject is Power || GameObject is Coin)
                       {
                           ImgChange = Globals.MediumImageSize * 1;
                       }
                   }

                   else if (he.EventType == HeroEvent.Type.FlyUp)
                       ImgChange = Globals.MediumImageSize * 2;
                   else if (he.EventType == HeroEvent.Type.EnemyDead)
                   {
                       rotateAngle = 90;
                       GameObject.PosY += 32;
                   }
                   else if (he.EventType == HeroEvent.Type.ChangeImage)
                   {
                       ImgStep++;
                       if (ImgStep < 5)
                       {
                           ImgChange = Globals.MediumImageSize * ImgStep;
                       }
                       else
                       {
                           ImgChange = Globals.Reset;
                       }

                   }

               }*/
        }

        public override void Move(float deltaT)
        {            
            GameObject.State.Update(deltaT);
            base.Move(deltaT);
            //Console.WriteLine(GameObject.State.GetDirection());
        }


        public void jumpAble() {
            GameObject.JumpPossibility = 2;
        }

        public void shoot() {
            if (GameObject.CanShoot) {
                GameObject.Shoot = true;
            }
        }

        public void powerUp() {
            GameObject.ShootingSpeed += Globals.BulletPowerUp;
        }

    }
}
