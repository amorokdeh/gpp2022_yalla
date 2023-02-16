using System.Collections.Generic;

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
