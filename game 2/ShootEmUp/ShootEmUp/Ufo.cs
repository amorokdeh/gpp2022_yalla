using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Ufo:Enemy
    {
        public Ufo(string name) : base(name)
        {
            PosX = 500;
            PosY = 200;
            Width = 50;
            Height = 50;
        }
    }
}
