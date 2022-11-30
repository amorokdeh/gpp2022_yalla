using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class GameOver
    {
        public bool running = true;
        public bool quit = false;
        public IntPtr renderer;
        public string winnerText = "hallo";
        public Text txt = new Text();
        public GameOver() {
            setup();
        }
        public void setup()
        {
            renderer = Program.window.renderer;
            /*renderer = SDL.SDL_CreateRenderer(
                Program.window.show,
                -1,
                SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);*/

            if (renderer == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the renderer. {SDL.SDL_GetError()}");
            }
            //text
            txt.setUp();
            txt.loadText(1);
            //winner
            if (Program.game.mainMenu.winner == 2)
            {
                SDL.SDL_SetRenderDrawColor(renderer, 0, 60, 20, 255);
                winnerText = "Der rechte Spieler hat gewonnen!";
            }
            else
            {
                SDL.SDL_SetRenderDrawColor(renderer, 110, 0, 0, 255);
                winnerText = "Der linke Spieler hat gewonnen!";
            }
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
                else if (e.type == SDL.SDL_EventType.SDL_KEYUP)
                {

                    switch (e.key.keysym.sym)
                    {
                        case SDL.SDL_Keycode.SDLK_RETURN: 
                            running = false;
                            if(Program.game.mainMenu.level == 1)
                            {
                                closeAndGoTo(2); 
                            }
                            else if (Program.game.mainMenu.level == 2)
                            {
                                closeAndGoTo(3);
                            }
                            else if (Program.game.mainMenu.level == 3)
                            {
                                closeAndGoTo(4);
                            }

                            break;
                        case SDL.SDL_Keycode.SDLK_SPACE: 
                            running = false; 
                            closeAndGoTo(1); 
                            break;
                    }
                }

            }
        }

        public void render() {
            //Clear screen
            SDL.SDL_SetRenderDrawColor(renderer, 5, 5, 5, 255);
            SDL.SDL_RenderClear(renderer);
            IntPtr surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, winnerText, txt.White);
            txt.addText(renderer, surfaceMessage, 40, Program.window.heigh / 2 - 100, Program.window.width - 80, 100);

            string text = "DRUECKE ENTER, UM NOCHMAL ZU SPIELEN";
            surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, txt.LightGray);
            txt.addText(renderer, surfaceMessage, 100, Program.window.heigh / 2 + 120, Program.window.width - 200, 30);

            text = "DRUECKE SPACE, UM MAIN MENU ZU ZEIGEN";
            surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, txt.LightGray);
            txt.addText(renderer, surfaceMessage, 100, Program.window.heigh / 2 + 170, Program.window.width - 200, 30);

            SDL.SDL_RenderPresent(renderer);
        }

        public void closeAndGoTo(int displayNum)
        {
            
            //clear renderer
            //SDL.SDL_RenderClear(renderer);
            //SDL.SDL_DestroyRenderer(renderer);
            //go to ...
            Program.game.display = displayNum;
        }
    }
}
