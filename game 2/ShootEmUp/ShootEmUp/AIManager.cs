using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class AIManager
    {
        List<AIComponent> _aiComponents = new List<AIComponent>();

        internal Component CreateComponent()
        {
            AIComponent rc = new AIComponent(this);
            _aiComponents.Add(rc);
            return rc;
        }

        public void Control()
        {

            foreach (var component in _aiComponents)
            {
                component.Control();
            }

        }
    }
}
