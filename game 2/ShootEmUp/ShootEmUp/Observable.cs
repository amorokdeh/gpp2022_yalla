using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Event { };
    interface Observer
    {
        void OnEvent(Event e);
    }
    class Observable
    {
        /*
        public event EventHandler<EventArgs> EventOccured;
        public void Notify()
        {
            if (EventOccured != null)
                EventOccured(this, new EventArgs());
        }
        */
        List<Observer> _observers = new List<Observer>();
        public void AddObserver(Observer o)
        {
            _observers.Add(o);
        }
        public void Notify(Event e)
        {
            foreach (Observer l in _observers)
                l.OnEvent(e);
        }


    }
}
