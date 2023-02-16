using System;

namespace TileBasedGame
{
    class Jumping : State
    {
        GameObject GameObject;
        public String Direction;
        int JumpPossibility = 2;
        int Frame;
        bool Flipped;

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
            if(JumpPossibility == 2)
                Jump();
            SetVelocity();
        }

        private void Jump()
        {
            if (JumpPossibility > 0)
            {
                GameObject.CurrentVelY = Globals.JUMP_VELOCITY;
                JumpPossibility--;
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.Jump, this.GameObject));
            }
        }
        public void Update(float timeStep) 
        {

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
            GameObject.CharData.Jump.Setup();
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
                return PhysicsComponent.AnimationState.Jump;
            }
            else if (me.EventType == MovingEvent.Type.GoRight)
            {
                Direction = "right";
                return PhysicsComponent.AnimationState.Jump;
            }
            else if (me.EventType == MovingEvent.Type.GoUp)
            {
                Jump();
                return PhysicsComponent.AnimationState.Jump;
            }
            else if (me.EventType == MovingEvent.Type.GoDown)
            {
                return PhysicsComponent.AnimationState.Jump;
            }
            else if (me.EventType == MovingEvent.Type.JumpAble)
            {
                JumpPossibility = 2;
                return PhysicsComponent.AnimationState.Run;
            }
            return PhysicsComponent.AnimationState.Jump;
        }
    }
}
