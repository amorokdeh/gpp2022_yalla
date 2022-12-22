using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ShootEmUp
{
    class MainMenu
    {
        //public static Sound sound = new Sound();



        public bool running = true;
        public bool quit = false;
        public Text txt = new Text();
        //public int selected = 1;
        //private LevelManager.GameState selected = LevelManager.GameState.MainMenu;
        public string text;
        public int textXPosition;
        public int textEndPosition;
        public IntPtr surfaceMessage;
        public SDL.SDL_Color color;
        public int textSize = 30;
        public String menuSelected = "main menu";
        

        public enum Choices
        {
            Level1,
            Level2,
            Level3,
            Options,
            Quit,
            Window,
            BackMainMenu,
            Screen,
            FpsLimit,
            ScreenSize,
            BackOption

        }
        private Choices selected = Choices.Level1;

        public MainMenu()
        {
            setup();
        }

        public void setup()
        {
            //text
            txt.setUp();
            txt.loadText(1);
            color = txt.White;

            SDL.SDL_SetRenderDrawColor(Program.window.renderer, 0, 60, 20, 255);

            // SOUND AND MUSIC
            //sound.setup();

        }
        public void run()
        {
            while (running)
            {
                Program.window.calculateFPS(); //frame limit start calculating here
                render();
                controll();
                Program.window.deltaFPS(); //frame limit end calculating here

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
            
            if (menuSelected.Equals("option"))
            {
                switch (selected)
                {
                    case Choices.Window:
                        menuSelected = "window";
                        selected = Choices.Screen;
                        return;
                    case Choices.BackMainMenu:
                        menuSelected = "main menu";
                        selected = Choices.Level1;
                        return;
                }
            }
            else if (menuSelected.Equals("window"))
            {
                switch (selected)
                {
                    case Choices.Screen:
                        Program.window.changeScreenMode();
                        selected = Choices.Screen;
                        return;
                    case Choices.FpsLimit:
                        Program.window.changeFPSLimit();
                        return;
                    case Choices.ScreenSize:
                        Program.window.changeWindowSize();
                        return;
                    case Choices.BackOption:
                        menuSelected = "option";
                        selected = Choices.Window;
                        return;
                }
            }
            else
            {
                switch (selected)
                {
                    case Choices.Level1:
                        startLevel1();
                        return;
                    case Choices.Level2:
                        startLevel2();
                        return;
                    case Choices.Level3:
                        startLevel3();
                        return;
                    case Choices.Options:
                        menuSelected = "option";
                        selected = Choices.Window;
                        return;
                    case Choices.Quit:
                        endGame();
                        return;
                }
            }
        }
        public void goUp() {
            if ((menuSelected.Equals("option")) && (selected > Choices.Window))
            {
                selected--;
                return;
            }
            else if ((menuSelected.Equals("window")) && (selected > Choices.Screen))
            {
                selected--;
                return;
            }
            else if ((menuSelected.Equals("main menu")) && selected > Choices.Level1) {
                selected--;
                return;
            }

        }
        public void goDown() {
            
            if ((menuSelected.Equals("option")) && (selected < Choices.BackMainMenu))
            {
                selected++;
                return;
            }
            else if ((menuSelected.Equals("window")) && (selected < Choices.BackOption)) {
                selected++;
                return;
            }
            else if ((menuSelected.Equals("main menu")) && selected < Choices.Quit)
            {
                selected++;
                return;
            }
        }

        public void endGame() {
            running = false; 
            closeAndGoTo(LevelManager.GameState.Quit);

        }

        public void startLevel1()
        {
            running = false;
            closeAndGoTo(LevelManager.GameState.Level1);

        }
        public void startLevel2()
        {
            running = false;
            closeAndGoTo(LevelManager.GameState.Level2);

        }
        public void startLevel3()
        {
            running = false;
            closeAndGoTo(LevelManager.GameState.Level3);

        }
        public void render()
        {
            //Clear screen
            SDL.SDL_SetRenderDrawColor(Program.window.renderer, 5, 5, 5, 255);
            SDL.SDL_RenderClear(Program.window.renderer);

            //show FPS
            Program.window.fpsCalculate();

            
            if (menuSelected.Equals("option"))
            {
                text = "Window";
                checkSelected(Choices.Window);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                textXPosition = (Program.window.width / 2) - (text.Length * 17);
                textEndPosition = (Program.window.width / 2) - (text.Length * 50);
                txt.addText(Program.window.renderer, surfaceMessage, textXPosition, Program.window.heigh / 2 - 50, textEndPosition, textSize);

                text = "Back";
                checkSelected(Choices.BackMainMenu);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                textXPosition = (Program.window.width / 2) - (text.Length * 17);
                textEndPosition = (Program.window.width / 2) - (text.Length * 90);
                txt.addText(Program.window.renderer, surfaceMessage, textXPosition, Program.window.heigh / 2, textEndPosition, textSize);

               
            }
            else if (menuSelected.Equals("window")) {
                text = "Screen: " + Program.window.screenMode;
                checkSelected(Choices.Screen);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                textXPosition = (Program.window.width / 2) - (text.Length * 10);
                textEndPosition = (Program.window.width / 2) - (text.Length * 12);
                txt.addText(Program.window.renderer, surfaceMessage, textXPosition, Program.window.heigh / 2 - 50, textEndPosition, textSize);

                text = "FPS limit: " + Program.window.limitedFPS;
                checkSelected(Choices.FpsLimit);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                textXPosition = (Program.window.width / 2) - (text.Length * 17);
                textEndPosition = (Program.window.width / 2) - (text.Length * 12);
                txt.addText(Program.window.renderer, surfaceMessage, textXPosition, Program.window.heigh / 2, textEndPosition, textSize);

                text = "Screen size: " + Program.window.width + " x " + Program.window.heigh;
                checkSelected(Choices.ScreenSize);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                textXPosition = (Program.window.width / 2) - (text.Length * 11);
                textEndPosition = (Program.window.width / 2) - (text.Length * 4);
                txt.addText(Program.window.renderer, surfaceMessage, textXPosition, Program.window.heigh / 2 + 50, textEndPosition, textSize);

                text = "Back";
                checkSelected(Choices.BackOption);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                textXPosition = (Program.window.width / 2) - (text.Length * 40);
                textEndPosition = (Program.window.width / 2) - (text.Length * 70);
                txt.addText(Program.window.renderer, surfaceMessage, textXPosition, Program.window.heigh / 2 + 100, textEndPosition, textSize);
            }
            else
            {
                text = "Start Level 1";
                checkSelected(Choices.Level1);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                textXPosition = (Program.window.width / 2) - (text.Length * 15);
                textEndPosition = (Program.window.width / 2) - (text.Length * 12);
                txt.addText(Program.window.renderer, surfaceMessage, textXPosition, Program.window.heigh / 2 - 50 , textEndPosition, textSize);

                text = "Start Level 2";
                checkSelected(Choices.Level2);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                textXPosition = (Program.window.width / 2) - (text.Length * 15);
                textEndPosition = (Program.window.width / 2) - (text.Length * 12);
                txt.addText(Program.window.renderer, surfaceMessage, textXPosition, Program.window.heigh / 2, textEndPosition, textSize);

                text = "Start Level 3";
                checkSelected(Choices.Level3);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                textXPosition = (Program.window.width / 2) - (text.Length * 15);
                textEndPosition = (Program.window.width / 2) - (text.Length * 12);
                txt.addText(Program.window.renderer, surfaceMessage, textXPosition, Program.window.heigh / 2 + 50, textEndPosition, textSize);


                text = "Options";
                checkSelected(Choices.Options);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                textXPosition = (Program.window.width / 2) - (text.Length * 20);
                textEndPosition = (Program.window.width / 2) - (text.Length * 35);
                txt.addText(Program.window.renderer, surfaceMessage, textXPosition, Program.window.heigh / 2 + 100, textEndPosition, textSize);

                text = "Quit";
                checkSelected(Choices.Quit);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                textXPosition = (Program.window.width / 2) - (text.Length * 20);
                textEndPosition = (Program.window.width / 2) - (text.Length * 90);
                txt.addText(Program.window.renderer, surfaceMessage, textXPosition, Program.window.heigh / 2 + 150, textEndPosition, textSize);
            }
            SDL.SDL_RenderPresent(Program.window.renderer);
        }

        public void checkSelected(Choices choice) {
            if (choice == selected)
            {
                color = txt.Red;
                textSize = 40;
            } else
            {
                color = txt.White;
                textSize = 30;
            }
        }

        public void closeAndGoTo(LevelManager.GameState gs)
        {
            LevelManager.display = gs;
           
        }
    }
}

