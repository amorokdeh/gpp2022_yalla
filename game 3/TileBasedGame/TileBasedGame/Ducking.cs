using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Ducking : State
    {
        GameObject GameObject;
        public String Direction;
        int Frame;
        bool Flipped;
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
            }
            else if (GameObject.CurrentVelX > 0)
            {
                GameObject.direction = "right";
                Flipped = false;
                Frame = GameObject.CharData.Duck.Animate(timeStep);
            }
            else if (GameObject.CurrentVelX < 0)
            {
                GameObject.direction = "left";
                Flipped = true;
                Frame = GameObject.CharData.Duck.Animate(timeStep);
            }      
            MessageBus.PostEvent(new AnimationEvent(AnimationEvent.Type.Animation, this.GameObject, Frame, Flipped));
        }
        public void Enter(GameObject gameObject)
        {
            GameObject = gameObject;
            GameObject.Height = GameObject.GeneralHeight/2;

            GameObject.CharData.Duck.Setup();
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
                State state = new Running();
                state.SetDirection(Direction);
                state.SetFlipped(Flipped);
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.TryStanding, GameObject, GameObject.PosX, GameObject.PosY));
                GameObject.PosY -= GameObject.GeneralHeight / 2 +2;               
                return state;
            }
            else if (me.EventType == MovingEvent.Type.GoDown)
            {
                return this;
            }
            return this;
        }
    }
}
