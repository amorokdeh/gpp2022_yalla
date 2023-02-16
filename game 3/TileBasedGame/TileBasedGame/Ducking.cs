using System;

namespace TileBasedGame
{
    class Ducking : State
    {
        GameObject GameObject;
        public string Direction;
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
            GameObject.Height = GameObject.GeneralHeight / 2;
            SetVelocity();
        }

        public void Update(float timeStep)
        {

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
            GameObject.CharData.Duck.Setup();
            GameObject.CharData.Stand.Setup();
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
                return PhysicsComponent.AnimationState.Duck;
            }
            else if (me.EventType == MovingEvent.Type.GoRight)
            {
                Direction = "right";
                return PhysicsComponent.AnimationState.Duck;
            }
            else if (me.EventType == MovingEvent.Type.GoUp)
            {
                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.TryStanding, GameObject, GameObject.PosX, GameObject.PosY));
                GameObject.PosY -= GameObject.GeneralHeight / 2 + 2;
                return PhysicsComponent.AnimationState.Run;
            }
            else if (me.EventType == MovingEvent.Type.GoDown)
            {
                return PhysicsComponent.AnimationState.Duck;
            }
            return PhysicsComponent.AnimationState.Duck;
        }
    }
}
