﻿using SDL2;
using System;
using System.Collections.Generic;
using System.IO;
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
        public string text;
        public int nextText = 0;
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
            showFPS,
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
            if (menuSelected.Equals("main menu"))
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
            } else if (menuSelected.Equals("option"))
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
            } else if (menuSelected.Equals("window"))
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
                    case Choices.showFPS:
                        Program.window.showHideFPS();
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
        public int nextTextPos() {
            nextText += 50;
            return nextText;
        }

        public void textPrinter(String textIndex, Choices menu) {
            text = textIndex;
            checkSelected(menu);
            surfaceMessage = SDL_ttf.TTF_RenderText_Solid(txt.Font, text, color);
            txt.addText(Program.window.renderer, surfaceMessage, Program.window.width / 2 - text.Length * 10, nextTextPos(), text.Length * 20, textSize);
        }
        public void render()
        {
            //Clear screen
            SDL.SDL_SetRenderDrawColor(Program.window.renderer, 5, 5, 5, 255);
            SDL.SDL_RenderClear(Program.window.renderer);

            //calculate FPS
            Program.window.fpsCalculate();

            
            if (menuSelected.Equals("option"))
            {
                nextText = Program.window.heigh / 3;

                textPrinter("Window", Choices.Window);
                textPrinter("Back", Choices.BackMainMenu);

            }
            else if (menuSelected.Equals("window")) 
            {
                nextText = Program.window.heigh / 3;

                textPrinter("Screen: " + Program.window.screenMode, Choices.Screen);
                textPrinter("FPS limit: " + Program.window.limitedFPS, Choices.FpsLimit);
                textPrinter("Show FPS on display: " + Program.window.showFPSRunning, Choices.showFPS);
                textPrinter("Screen size: " + Program.window.width + " x " + Program.window.heigh, Choices.ScreenSize);
                textPrinter("Back", Choices.BackOption);
            }
            else
            {
                nextText = Program.window.heigh / 3;

                textPrinter("Start Level 1", Choices.Level1);
                textPrinter("Start Level 2", Choices.Level2);
                textPrinter("Start Level 3", Choices.Level3);
                textPrinter("Options", Choices.Options);
                textPrinter("Quit", Choices.Quit);
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

