using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    class GameOver
    {   
        public bool Running = true;
        public bool Quit = false;
        public IntPtr Renderer;
        public string WinnerText = "hallo";
        public Text Txt = new Text();
        public GameOver() {
            Setup();
        }
        public void Setup()
        {
            Renderer = Program.Window.Renderer;


            if (Renderer == IntPtr.Zero)
            {
                Console.WriteLine($"There was an issue creating the renderer. {SDL.SDL_GetError()}");
            }
            //text
            Txt.SetUp();
            Txt.LoadText(1);
            //winner

                SDL.SDL_SetRenderDrawColor(Renderer, 0, 60, 20, 255);
                WinnerText = "GAME OVER";

        }
        public void Run()
        {
            while (Running)
            {
                Render();
                Control();
            }
            if (Quit) { CloseAndGoTo(LevelManager.GameState.Quit); } //close the game
        }

        public void Control()
        {
            //Key
            SDL.SDL_Event e;
            // Handle events on queue
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                //User requests quit
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    Running = false;
                    Quit = true;
                }
                else if (e.type == SDL.SDL_EventType.SDL_KEYUP)
                {

                    switch (e.key.keysym.sym)
                    {
                        case SDL.SDL_Keycode.SDLK_RETURN: 
                            Running = false;
                          if(LevelManager.CurrentLevel == 1)
                            {
                                CloseAndGoTo(LevelManager.GameState.Level1); 
                            }
                            else if (LevelManager.CurrentLevel == 2)
                            {
                                CloseAndGoTo(LevelManager.GameState.Level2);
                            }
                            else if (LevelManager.CurrentLevel == 3)
                            {
                                CloseAndGoTo(LevelManager.GameState.Level3);
                            }

                            break;
                        case SDL.SDL_Keycode.SDLK_SPACE: 
                            Running = false; 
                            CloseAndGoTo(LevelManager.GameState.MainMenu); 
                            break;
                    }
                }

            }
        }

        public void Render() {
            //Clear screen
            SDL.SDL_SetRenderDrawColor(Renderer, 5, 5, 5, 255);
            SDL.SDL_RenderClear(Renderer);
            int nextPos = Program.Window.Height / 4;
            IntPtr surfaceMessage = SDL_ttf.TTF_RenderText_Solid(Txt.Font, WinnerText, Txt.White);
            Txt.AddText(Program.Window.Renderer, surfaceMessage, Program.Window.Width / 2 - WinnerText.Length * 10, nextPos, WinnerText.Length * 20, 100);

            string text = "Score: " + Program.Game.Player.Score;
            surfaceMessage = SDL_ttf.TTF_RenderText_Solid(Txt.Font, text, Txt.Green);
            Txt.AddText(Program.Window.Renderer, surfaceMessage, Program.Window.Width / 2 - text.Length * 10, nextPos + 150, text.Length * 20, 30);


            text = "DRUECKE ENTER, UM NOCHMAL ZU SPIELEN";
            surfaceMessage = SDL_ttf.TTF_RenderText_Solid(Txt.Font, text, Txt.LightGray);
            Txt.AddText(Program.Window.Renderer, surfaceMessage, Program.Window.Width / 2 - text.Length * 10, nextPos + 300, text.Length * 20, 30);

            text = "DRUECKE SPACE, UM MAIN MENU ZU ZEIGEN";
            surfaceMessage = SDL_ttf.TTF_RenderText_Solid(Txt.Font, text, Txt.LightGray);
            Txt.AddText(Program.Window.Renderer, surfaceMessage, Program.Window.Width / 2 - text.Length * 10, nextPos + 350, text.Length * 20, 30);

            SDL.SDL_RenderPresent(Renderer);
        }

        public void CloseAndGoTo(LevelManager.GameState gs)
        {
            
            //clear renderer
            //SDL.SDL_RenderClear(renderer);
            //SDL.SDL_DestroyRenderer(renderer);
            //go to ...
            LevelManager.display = gs;
        }

    }
}
