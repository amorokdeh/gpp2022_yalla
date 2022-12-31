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


            if (renderer == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the renderer. {SDL.SDL_GetError()}");
            }
            //text
            txt.setUp();
            txt.loadText(1);
            //winner

                SDL.SDL_SetRenderDrawColor(renderer, 0, 60, 20, 255);
                winnerText = "GAME OVER";

        }
        public void run()
        {
            while (running)
            {
                render();
                controll();

            }

            if (quit) { closeAndGoTo(LevelManager.GameState.Quit); } //close the game
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
                          if(LevelManager.CurrentLevel == 1)
                            {
                                closeAndGoTo(LevelManager.GameState.Level1); 
                            }
                            else if (LevelManager.CurrentLevel == 2)
                            {
                                closeAndGoTo(LevelManager.GameState.Level2);
                            }
                            else if (LevelManager.CurrentLevel == 3)
                            {
                                closeAndGoTo(LevelManager.GameState.Level3);
                            }

                            break;
                        case SDL.SDL_Keycode.SDLK_SPACE: 
                            running = false; 
                            closeAndGoTo(LevelManager.GameState.MainMenu); 
                            break;
                    }
                }

            }
        }

        public void render() {
            //Clear screen
            SDL.SDL_SetRenderDrawColor(renderer, 5, 5, 5, 255);
            SDL.SDL_RenderClear(renderer);
            int nextPos = Program.window.heigh / 3;
            IntPtr surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, winnerText, txt.White);
            txt.addText(Program.window.renderer, surfaceMessage, Program.window.width / 2 - winnerText.Length * 10, nextPos, winnerText.Length * 20, 100);

            string text = "DRUECKE ENTER, UM NOCHMAL ZU SPIELEN";
            surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, txt.LightGray);
            txt.addText(Program.window.renderer, surfaceMessage, Program.window.width / 2 - text.Length * 10, nextPos + 200, text.Length * 20, 30);

            text = "DRUECKE SPACE, UM MAIN MENU ZU ZEIGEN";
            surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, txt.LightGray);
            txt.addText(Program.window.renderer, surfaceMessage, Program.window.width / 2 - text.Length * 10, nextPos + 250, text.Length * 20, 30);

            SDL.SDL_RenderPresent(renderer);
        }

        public void closeAndGoTo(LevelManager.GameState gs)
        {
            
            //clear renderer
            //SDL.SDL_RenderClear(renderer);
            //SDL.SDL_DestroyRenderer(renderer);
            //go to ...
            LevelManager.display = gs;
        }

    }
}
