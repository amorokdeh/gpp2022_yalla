using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Running : State
    {
        GameObject GameObject;
        public String Direction;
        int Frame;
        bool Flipped;
        float TimeToIdle = 0;
        float IdleTime = 0;
        public void SetDirection(String direction)
        {
            Direction = direction;
        }
        public void SetFlipped(bool flipped)
        {
            Flipped = flipped;
        }
        public String GetDirection()
        {
            return Direction;
        }
        public void Update(float timeStep) 
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
            GameObject.Height = GameObject.GeneralHeight;


            GameObject.CharData.Run.Setup();
            GameObject.CharData.Stand.Setup();
            GameObject.CharData.Idle.Setup();

        }

        public State HandleInput(MovingEvent me)
        {
            if (me.EventType == MovingEvent.Type.GoLeft)
            {
                Direction = "left";
                return this;
            }
            else if (me.EventType == MovingEvent.Type.GoRight)
            {
                Direction = "right";
                return this;
            }
            else if (me.EventType == MovingEvent.Type.GoUp)
            {
                State state = new Jumping();
                state.SetDirection(Direction);
                state.SetFlipped(Flipped);
                return state;
            }
            else if (me.EventType == MovingEvent.Type.GoDown)
            {
                State state = new Ducking();
                state.SetDirection(Direction);
                state.SetFlipped(Flipped);
                return state;
            }

            return this;
        }
    }
}
