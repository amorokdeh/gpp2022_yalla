using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Shooting : State
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

        }
        public void Enter(GameObject gameObject)
        {
            GameObject = gameObject;
        }

        public State HandleInput(MovingEvent me)
        {
            return this;
        }
    }
}

