using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    interface State
    {
        State HandleInput(HeroEvent he);
        void Update(float timeStep);
        void Enter(GameObject gameObject);
        void SetDirection(String direction);
        void SetFlipped(bool flipped);
        String GetDirection();
    }
}
