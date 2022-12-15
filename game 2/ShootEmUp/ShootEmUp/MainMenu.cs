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
        public int winner;
        public int level;
        
        


        public bool running = true;
        public bool quit = false;
        public Text txt = new Text();
        public int selected = 1;
        public string text;
        public IntPtr surfaceMessage;
        public SDL.SDL_Color color;
        public int textSize = 30;
        public String menuSelected = "main menu";


        private LevelManager _levelManager;
        public MainMenu(LevelManager lvlM)
        {
            this._levelManager = lvlM;
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
            if (menuSelected.Equals("option"))
            {
                switch (selected)
                {
                    case 1:
                        menuSelected = "window";
                        selected = 1;
                        return;
                    case 2:
                        menuSelected = "main menu";
                        selected = 1;
                        return;
                }
            }
            else if (menuSelected.Equals("window"))
            {
                switch (selected)
                {
                    case 1:
                        Program.window.changeScreenMode();
                        selected = 1;
                        return;
                    case 2:
                        Program.window.changeFPSLimit();
                        return;
                    case 3:
                        Program.window.changeWindowSize();
                        return;
                    case 4:
                        menuSelected = "option";
                        selected = 1;
                        return;
                }
            }
            else
            {
                switch (selected)
                {
                    case 1:
                        level = 1;
                        startLevel1();
                        return;
                    case 2:
                        startLevel2();
                        level = 2;
                        return;
                    case 3:
                        startLevel3();
                        level = 3;
                        return;
                    case 4:
                        menuSelected = "option";
                        selected = 1;
                        return;
                    case 5:
                        endGame();
                        return;
                }
            }
        }
        public void goUp() {
            if (selected > 1) {
                selected--;
            }
        }
        public void goDown() {
            
            if ((menuSelected.Equals("option")) && (selected < 2))
            {
                selected++;
                return;
            }
            else if ((menuSelected.Equals("window")) && (selected < 4)) {
                selected++;
                return;
            }
            else if ((menuSelected.Equals("main menu")) && selected < 5)
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
                checkSelected(1);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                txt.addText(Program.window.renderer, surfaceMessage, Program.window.heigh / 2, Program.window.heigh / 2 - 50, Program.window.width - 600, textSize);

                text = "Back";
                checkSelected(2);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                txt.addText(Program.window.renderer, surfaceMessage, Program.window.heigh / 2 + 50, Program.window.heigh / 2, Program.window.width - 700, textSize);

               
            }
            else if (menuSelected.Equals("window")) {
                text = "Screen: " + Program.window.screenMode;
                checkSelected(1);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                txt.addText(Program.window.renderer, surfaceMessage, Program.window.heigh / 2, Program.window.heigh / 2 - 50, Program.window.width - 600, textSize);

                text = "FPS limit: " + Program.window.limitedFPS;
                checkSelected(2);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                txt.addText(Program.window.renderer, surfaceMessage, Program.window.heigh / 2, Program.window.heigh / 2, Program.window.width - 600, textSize);

                text = "Screen size: " + Program.window.heigh + " x " + Program.window.width;
                checkSelected(3);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                txt.addText(Program.window.renderer, surfaceMessage, Program.window.heigh / 2, Program.window.heigh / 2 + 50, Program.window.width - 600, textSize);

                text = "Back";
                checkSelected(4);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                txt.addText(Program.window.renderer, surfaceMessage, Program.window.heigh / 2, Program.window.heigh / 2 + 100, Program.window.width - 600, textSize);
            }
            else
            {
                text = "Start Level 1";
                checkSelected(1);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                txt.addText(Program.window.renderer, surfaceMessage, Program.window.heigh / 2, Program.window.heigh / 2 - 50, Program.window.width - 600, textSize);

                text = "Start Level 2";
                checkSelected(2);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                txt.addText(Program.window.renderer, surfaceMessage, Program.window.heigh / 2, Program.window.heigh / 2, Program.window.width - 600, textSize);

                text = "Start Level 3";
                checkSelected(3);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                txt.addText(Program.window.renderer, surfaceMessage, Program.window.heigh / 2, Program.window.heigh / 2 + 50, Program.window.width - 600, textSize);


                text = "Options";
                checkSelected(4);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                txt.addText(Program.window.renderer, surfaceMessage, Program.window.heigh / 2, Program.window.heigh / 2 + 100, Program.window.width - 600, textSize);

                text = "Quit";
                checkSelected(5);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                txt.addText(Program.window.renderer, surfaceMessage, Program.window.heigh / 2 + 50, Program.window.heigh / 2 + 150, Program.window.width - 700, textSize);
            }
            SDL.SDL_RenderPresent(Program.window.renderer);
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

        public void closeAndGoTo(LevelManager.GameState gs)
        {
            _levelManager.display = gs;
            //clear renderer
            //SDL.SDL_RenderClear(renderer);
            //SDL.SDL_DestroyRenderer(renderer);
            //go to ...
            //Program.game.display = displayNum;
           
        }
    }
}

