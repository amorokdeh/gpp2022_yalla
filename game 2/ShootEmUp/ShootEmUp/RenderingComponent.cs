using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class RenderingComponent : Component
    {

        RenderingManager RenderingManager;
        public RenderingComponent(RenderingManager rm)
        {
            this.RenderingManager = rm;
        }

        public void Render()
        {

        }
    }
}
