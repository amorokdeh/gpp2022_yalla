using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class Event { };
    interface Observer
    {
        void OnEvent(Event e);
    }
    class Observable
    {

        List<Observer> _observers = new List<Observer>();
        public void AddObserver(Observer o)
        {
            _observers.Add(o);

            //Console.WriteLine(_observers.Count());
        }
        public void Notify(Event e)
        {
            foreach (Observer l in _observers)
                if(l != null)
                    l.OnEvent(e);
        }

        public void Clean()
        {
            _observers.Clear();
            _observers = null;

            _observers = new List<Observer>();
            Program.Game.RegisterInBus();
        }


    }
}
