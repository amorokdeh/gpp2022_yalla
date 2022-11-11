using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Pong
{
    class MainMenu
    {
        public bool running = true;
        public bool quit = false;
        public IntPtr renderer;
        public string winnerText = "hallo";
        public Text txt = new Text();
        public int selected = 1;
        public string text;
        public IntPtr surfaceMessage;
        public SDL.SDL_Color color;
        public int textSize = 30;
        public MainMenu()
        {
            setup();
        }

        public void setup()
        {
            renderer = SDL.SDL_CreateRenderer(
                Program.window.show,
                -1,
                SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            if (renderer == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the renderer. {SDL.SDL_GetError()}");
            }
            //text
            txt.setUp();
            txt.loadText(1);
            color = txt.White;

            SDL.SDL_SetRenderDrawColor(renderer, 0, 60, 20, 255);
            winnerText = "Der rechte Spieler hat gewonnen!";

        }
        public void run()
        {
            while (running)
            {
                render();
                controll();

            }

            if (quit) { closeAndGoTo(0); } //close the game
        }

        public void controll()
        {
            //Key
            SDL.SDL_Event e;
            // Handle events on queue
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                //User requests quit
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    running = false;
                    quit = true;
                }
                else if (e.type == SDL.SDL_EventType.SDL_KEYDOWN)
                {

                    switch (e.key.keysym.sym)
                    {
                        case SDL.SDL_Keycode.SDLK_RETURN: goTo(); break;
                        case SDL.SDL_Keycode.SDLK_UP: goUp(); break;
                        case SDL.SDL_Keycode.SDLK_DOWN: goDown(); break;
                    }
                }

            }
        }
        public void goTo() {
            switch (selected) {
                case 1: startGame(); return;
                case 3: endGame(); return;
            }
        }
        public void goUp() {
            if (selected > 1) {
                selected--;
            }
        }
        public void goDown() {
            if (selected < 3)
            {
                selected++;
            }
        }

        public void endGame() {
            running = false; 
            closeAndGoTo(0);

        }

        public void startGame()
        {
            running = false;
            closeAndGoTo(2);

        }
        public void render()
        {
            //Clear screen
            SDL.SDL_SetRenderDrawColor(renderer, 5, 5, 5, 255);
            SDL.SDL_RenderClear(renderer);

            text = "Start game";
            checkSelected(1);
            surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
            txt.addText(renderer, surfaceMessage, Program.window.heigh / 2, Program.window.heigh / 2 , Program.window.width - 600, textSize);

            text = "Options";
            checkSelected(2);
            surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
            txt.addText(renderer, surfaceMessage, Program.window.heigh / 2, Program.window.heigh / 2 + 50, Program.window.width - 600, textSize);

            text = "Quit";
            checkSelected(3);
            surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
            txt.addText(renderer, surfaceMessage, Program.window.heigh / 2 + 50, Program.window.heigh / 2 + 100, Program.window.width - 700, textSize);

            SDL.SDL_RenderPresent(renderer);
        }

        public void checkSelected(int num) {
            if (num == selected)
            {
                color = txt.Red;
                textSize = 40;
            } else
            {
                color = txt.White;
                textSize = 30;
            }
        }

        public void closeAndGoTo(int displayNum)
        {

            //clear renderer
            SDL.SDL_RenderClear(renderer);
            SDL.SDL_DestroyRenderer(renderer);
            //go to ...
            Program.game.display = displayNum;
        }
    }
}

