using SDL2;
using System;

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
                            Level level = Program.Game.Levels.CurrentLevel;
                            if (level is Level1)
                            {
                                CloseAndGoTo(LevelManager.GameState.Level1);
                                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.Level1));
                            }
                            else if (level is Level2)
                            {
                                CloseAndGoTo(LevelManager.GameState.Level2);
                                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.Level2));
                            }
                            else if (level is Level3)
                            {
                                CloseAndGoTo(LevelManager.GameState.Level3);
                                MessageBus.PostEvent(new HeroEvent(HeroEvent.Type.Level3));
                            }

                            break;
                        case SDL.SDL_Keycode.SDLK_m: 
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

            text = "DRUECKE M, UM MAIN MENU ZU ZEIGEN";
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
