
namespace TileBasedGame
{
    class PhysicsComponent : Component
    {
        State State;
        AnimationState Next;
        State Prev;
        PhysicsManager PhysicsManager;
        private bool _isShooting = false;

        private State _running = new Running();
        private State _jumping = new Jumping();
        private State _ducking = new Ducking();

        public enum AnimationState
        {
            Run,
            Jump,
            Duck
        }
        public PhysicsComponent(PhysicsManager pm):base()
        {
            MessageBus.Register(this);
            this.PhysicsManager = pm;
        }

        public override void SetGameObject(GameObject gameObject)
        {
            GameObject = gameObject;

            if(GameObject.CharData.Run != null)
            {
                _running.Enter(GameObject);
                _jumping.Enter(GameObject);
                _ducking.Enter(GameObject);

                State = _running;
                State.SetValues("stand", false);
            }           
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
                            _isShooting = true;
                        else if (he.EventType == HeroEvent.Type.StopShooting)
                            _isShooting = false;
                        else if (he.EventType == HeroEvent.Type.PowerUp)
                            PowerUp();
                    }
                }
            }

            MovingEvent me = e as MovingEvent;
            if (me == null)
                return;
            if (me.GameObject == GameObject)
            {
                if (State != null) 
                {
                    Next = State.HandleInput(me);
                    Prev = State;

                    switch (Next)
                    {
                        case AnimationState.Run:
                            State = _running;
                            break;
                        case AnimationState.Duck:
                            State = _ducking;
                            break;
                        case AnimationState.Jump:
                            State = _jumping;
                            break;
                    }

                    State.SetValues(Prev.GetDirection(), Prev.GetFlipped());
                }
            }
        }

        public virtual void Move(float deltaT)
        {
            if(State != null && !GameObject.Died)
                State.Update(deltaT);

            GameObject.PosX += GameObject.CurrentVelX * deltaT;
            GameObject.PosY += GameObject.CurrentVelY * deltaT;

            //Gravity
            if (GameObject.CurrentVelY < 500)
            {
                GameObject.CurrentVelY += Globals.Gravity * deltaT;
            }

            if (_isShooting)
                Shoot();
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




