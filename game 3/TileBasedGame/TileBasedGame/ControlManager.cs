using SDL2;
using System.Collections.Generic;

namespace TileBasedGame
{
    class ControlManager
    {

        private List<ControlComponent> _controlComponents = new List<ControlComponent>();

        internal Component CreateComponent()
        {
            ControlComponent cc = new ControlComponent(this);
            _controlComponents.Add(cc);
            return cc;
        }

        public void Control()
        {
            SDL.SDL_Event e;
            // Handle events on queue
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    Game.Quit = true;
                    
                }
                foreach (var component in _controlComponents)
                {
                    component.HandleEvent(e);
                }
            }
        }
        public void clearObjects()
        {

            for (int i = 0; i < _controlComponents.Count; i++)
            {
                _controlComponents[i] = null;
            }
            _controlComponents.Clear();
        }
    }
}
