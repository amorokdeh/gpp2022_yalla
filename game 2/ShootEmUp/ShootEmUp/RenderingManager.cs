using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class RenderingManager
    {
        List<RenderingComponent> _renderingComponents;

        internal Component CreateComponent()
        {
            RenderingComponent rc = new RenderingComponent(this);
            _renderingComponents.Add(rc);
            return rc;
        }

        public void Render()
        {
            foreach(var rc in SubsetThatNeedsRendering())
            {
                rc.Render();
            }
        }

        public IEnumerable<RenderingComponent> SubsetThatNeedsRendering()
        {
            yield break;
        }
    }
}
