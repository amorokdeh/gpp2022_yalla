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
        public static Sound sound = new Sound();
        public int winner;
        public int level;
        
        


        public bool running = true;
        public bool quit = false;
        public static IntPtr renderer;
        public string winnerText;
        public Text txt = new Text();
        public int selected = 1;
        public string text;
        public IntPtr surfaceMessage;
        public SDL.SDL_Color color;
        public int textSize = 30;
        public bool optionSelected = false;
        public MainMenu()
        {
            setup();
        }

        public void setup()
        {
            renderer = Program.window.renderer;
            //text
            txt.setUp();
            txt.loadText(1);
            color = txt.White;

            SDL.SDL_SetRenderDrawColor(renderer, 0, 60, 20, 255);
            winnerText = "Der rechte Spieler hat gewonnen!";

            // SOUND AND MUSIC
            sound.setup();

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
            if (optionSelected)
            {
                switch (selected)
                {
                    case 1: //full screen
                        Program.window.changeScreenMode();
                        return;
                    case 2:
                        optionSelected= false;
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
                        option();
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
            if ((!optionSelected) && (selected < 5))
            {
                selected++;
            }
            if ((optionSelected) && (selected < 2))
            {
                selected++;
            }
        }

        public void endGame() {
            running = false; 
            closeAndGoTo(0);

        }

        public void startLevel1()
        {
            running = false;
            closeAndGoTo(2);

        }
        public void startLevel2()
        {
            running = false;
            closeAndGoTo(3);

        }
        public void startLevel3()
        {
            running = false;
            closeAndGoTo(4);

        }
        public void option()
        {
            selected = 1;
            optionSelected = true;

        }
        public void render()
        {
            //Clear screen
            SDL.SDL_SetRenderDrawColor(renderer, 5, 5, 5, 255);
            SDL.SDL_RenderClear(renderer);

            if (!optionSelected)
            {
                text = "Start Level 1";
                checkSelected(1);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                txt.addText(renderer, surfaceMessage, Program.window.heigh / 2, Program.window.heigh / 2 - 50, Program.window.width - 600, textSize);

                text = "Start Level 2";
                checkSelected(2);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                txt.addText(renderer, surfaceMessage, Program.window.heigh / 2, Program.window.heigh / 2, Program.window.width - 600, textSize);

                text = "Start Level 3";
                checkSelected(3);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                txt.addText(renderer, surfaceMessage, Program.window.heigh / 2, Program.window.heigh / 2 + 50, Program.window.width - 600, textSize);


                text = "Options";
                checkSelected(4);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                txt.addText(renderer, surfaceMessage, Program.window.heigh / 2, Program.window.heigh / 2 + 100, Program.window.width - 600, textSize);

                text = "Quit";
                checkSelected(5);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                txt.addText(renderer, surfaceMessage, Program.window.heigh / 2 + 50, Program.window.heigh / 2 + 150, Program.window.width - 700, textSize);
            }
            else {
                text = "Screen: " + Program.window.screenMode;
                checkSelected(1);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                txt.addText(renderer, surfaceMessage, Program.window.heigh / 2, Program.window.heigh / 2 - 50, Program.window.width - 600, textSize);

                text = "Back";
                checkSelected(2);
                surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
                txt.addText(renderer, surfaceMessage, Program.window.heigh / 2 + 50, Program.window.heigh / 2, Program.window.width - 700, textSize);
            }
            SDL.SDL_RenderPresent(renderer);
        }

        public void checkSelected(int num) {
            if ((num == selected) && (!optionSelected))
            {
                color = txt.Red;
                textSize = 40;
            } else
            {
                color = txt.White;
                textSize = 30;
            }
            if ((num == selected) && (optionSelected))
            {
                color = txt.Red;
                textSize = 40;
            }
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

