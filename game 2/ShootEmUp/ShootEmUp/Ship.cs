using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Ship:Enemy
    {
        public Ship(string name) : base(name)
        {
            PosX = 600;
            PosY = 200;
            Width = 50;
            Height = 50;
        }
    }
}
