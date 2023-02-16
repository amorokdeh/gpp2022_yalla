using System;

namespace TileBasedGame
{
    class Running : State
    {
        GameObject GameObject;
        public string Direction;
        int Frame;
        bool Flipped;
        float TimeToIdle = 0;
        float IdleTime = 0;

        public string GetDirection()
        {
            return Direction;
        }
        public bool GetFlipped()
        {
            return Flipped;
        }
        public void SetValues(string direction, bool flipped)
        {
            Direction = direction;
            Flipped = flipped;
            GameObject.Height = GameObject.GeneralHeight;
            SetVelocity();
        }

        public void Update(float timeStep) 
        {
            if (GameObject.CurrentVelX == 0)
            {
                Direction = "stand";
                Frame = GameObject.CharData.Stand.Animate(timeStep);
                
                TimeToIdle += timeStep;
                if (TimeToIdle > 4)
                {
                    Frame = GameObject.CharData.Idle.Animate(timeStep);
                    IdleTime += timeStep;
                    if (IdleTime > 1)
                    {
                        TimeToIdle = Globals.Reset;
                        IdleTime = Globals.Reset;
                    }
                }
            }
            else if (GameObject.CurrentVelX > 0)
            {
                GameObject.direction = "right";
                Flipped = false;
                Frame = GameObject.CharData.Run.Animate(timeStep);
                TimeToIdle = Globals.Reset;
                IdleTime = Globals.Reset;
            }
            else if (GameObject.CurrentVelX < 0)
            {
                GameObject.direction = "left";
                Flipped = true;
                Frame = GameObject.CharData.Run.Animate(timeStep);
                TimeToIdle = Globals.Reset;
                IdleTime = Globals.Reset;
            }

            MessageBus.PostEvent(new AnimationEvent(AnimationEvent.Type.Animation, this.GameObject, Frame, Flipped));

        }
        public void Enter(GameObject gameObject) 
        {
            GameObject = gameObject;
            GameObject.CharData.Run.Setup();
            GameObject.CharData.Stand.Setup();
            GameObject.CharData.Idle.Setup();
        }

        public void SetVelocity()
        {
            if (Direction == "left" && GameObject.CurrentVelX >= 0)
            {
                GameObject.CurrentVelX -= GameObject.VelX;
            }
            else if (Direction == "right" && GameObject.CurrentVelX <= 0)
            {
                GameObject.CurrentVelX += GameObject.VelX;
            }
            else if (Direction == "stand")
            {
                GameObject.CurrentVelX = 0;
            }
        }

        public PhysicsComponent.AnimationState HandleInput(MovingEvent me)
        {
            if (me.EventType == MovingEvent.Type.GoLeft)
            {
                Direction = "left";
                return PhysicsComponent.AnimationState.Run;
            }
            else if (me.EventType == MovingEvent.Type.GoRight)
            {
                Direction = "right";
                return PhysicsComponent.AnimationState.Run;
            }
            else if (me.EventType == MovingEvent.Type.GoUp)
            {
                TimeToIdle = Globals.Reset;
                IdleTime = Globals.Reset;
                return PhysicsComponent.AnimationState.Jump;
            }
            else if (me.EventType == MovingEvent.Type.GoDown)
            {
                TimeToIdle = Globals.Reset;
                IdleTime = Globals.Reset;
                return PhysicsComponent.AnimationState.Duck;
            }

            return PhysicsComponent.AnimationState.Run;
        }
    }
}
