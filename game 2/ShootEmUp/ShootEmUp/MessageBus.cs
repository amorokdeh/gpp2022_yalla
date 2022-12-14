using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    static class MessageBus
    {
        static Observable dispatcher = new Observable();

        static public void Register(Observer o)
        {
            dispatcher.AddObserver(o);
        }
        static public void PostEvent(Event evt)
        {
            dispatcher.Notify(evt);
        }


    }
}
