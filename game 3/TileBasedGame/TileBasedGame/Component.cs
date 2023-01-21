using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Component : Observable, Observer
    {
        public GameObject GameObject;

        public Component()
        {
            MessageBus.Register(this);
        }

        public virtual void OnEvent(Event e)
        {
        }

    }
}
