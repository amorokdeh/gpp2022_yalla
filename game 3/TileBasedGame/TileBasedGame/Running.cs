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
        public void SetDirection(String direction)
        {
            Direction = direction;
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
            } else if (Direction == "right" && GameObject.CurrentVelX <= 0)
            {
                GameObject.CurrentVelX += GameObject.VelX;
            } else if (Direction == "stand")
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
            }
            else if (GameObject.CurrentVelX < 0)
            {
                GameObject.direction = "left";
            }

            GameObject.ImgChange = GameObject.CharData.Run.Animate(timeStep);
            
        }
        public void Enter(GameObject gameObject) 
        {
            GameObject = gameObject;
            GameObject.Height = GameObject.GeneralHeight;


            GameObject.CharData.Run.Setup();
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
                State state = new Jumping();
                state.SetDirection(Direction);
                return state;
            }
            else if (he.EventType == HeroEvent.Type.GoDown)
            {
                State state = new Ducking();
                state.SetDirection(Direction);
                return state;
            }

            return this;
        }
    }
}
