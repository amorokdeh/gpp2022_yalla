using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class RenderingComponent : Component
    {
        public RenderingManager RenderingManager;
        public virtual void Render()
        {
        }
    }
}
