using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Jumping : State
    {
        GameObject GameObject;
        public String Direction;
        int JumpPossibility = 2;
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

        private void Jump()
        {
            if (JumpPossibility > 0)
            {
                GameObject.CurrentVelY = Globals.JUMP_VELOCITY;
                JumpPossibility--;
            }
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
            }
            else if (GameObject.CurrentVelX > 0)
            {
                GameObject.direction = "right";
                Flipped = false;
            }
            else if (GameObject.CurrentVelX < 0)
            {
                GameObject.direction = "left";
                Flipped = true;
            }

            Frame = GameObject.CharData.Jump.Animate(timeStep);
            MessageBus.PostEvent(new AnimationEvent(AnimationEvent.Type.Animation, this.GameObject, Frame, Flipped));
        }
        public void Enter(GameObject gameObject) 
        {
            GameObject = gameObject;
            GameObject.Height = GameObject.GeneralHeight;
            Jump();

            GameObject.CharData.Jump.Setup();
        }
        public State HandleInput(HeroEvent he)
        {

            if (he.EventType == HeroEvent.Type.GoLeft)
            {
                Direction = "left";
                return this;
            }
            else if (he.EventType == HeroEvent.Type.GoRight)
            {
                Direction = "right";
                return this;
            }
            else if (he.EventType == HeroEvent.Type.GoUp)
            {
                Jump();
                return this;
            }
            else if (he.EventType == HeroEvent.Type.GoDown)
            {
                State state = new Running();
                state.SetDirection(Direction);
                state.SetFlipped(Flipped);
                return state;
            }
            else if (he.EventType == HeroEvent.Type.JumpAble)
            {
                if (GameObject == he.GameObject)
                {
                    State state = new Running();
                    state.SetDirection(Direction);
                    state.SetFlipped(Flipped);
                    return state;
                }
                return this;
            }
                return this;
        }
    }
}
